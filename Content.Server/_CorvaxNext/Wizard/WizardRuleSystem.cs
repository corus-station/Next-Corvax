using Content.Server.Antag;
using Content.Server.GameTicking.Rules;
using Content.Server.Mind;
using Content.Server.Roles;
using Content.Server.Station.Components;
using Content.Shared.GameTicking.Components;
using Content.Shared.NPC.Components;
using Content.Shared.NPC.Systems;
using Robust.Shared.Random;

namespace Content.Server._CorvaxNext.Wizard;

public sealed class CorvaxWizardRuleSystem : GameRuleSystem<CorvaxWizardRuleComponent>
{
    [Dependency] private readonly AntagSelectionSystem _antag = default!;
    [Dependency] private readonly NpcFactionSystem _faction = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CorvaxWizardRuleComponent, AfterAntagEntitySelectedEvent>(OnAfterAntagSelected);

        SubscribeLocalEvent<CorvaxWizardRoleComponent, GetBriefingEvent>(OnGetBriefing);
    }

    protected override void Started(EntityUid uid,
        CorvaxWizardRuleComponent component,
        GameRuleComponent gameRule,
        GameRuleStartedEvent args)
    {
        var eligible = new List<Entity<StationEventEligibleComponent, NpcFactionMemberComponent>>();
        var eligibleQuery = EntityQueryEnumerator<StationEventEligibleComponent, NpcFactionMemberComponent>();
        while (eligibleQuery.MoveNext(out var eligibleUid, out var eligibleComp, out var member))
        {
            if (!_faction.IsFactionHostile(component.Faction, (eligibleUid, member)))
                continue;

            eligible.Add((eligibleUid, eligibleComp, member));
        }

        if (eligible.Count == 0)
            return;

        component.TargetStation = RobustRandom.Pick(eligible);
    }

    private void OnGetBriefing(Entity<CorvaxWizardRoleComponent> ent, ref GetBriefingEvent args)
    {
        args.Append(Loc.GetString("corvax-wizard-role-briefing"));
    }

    private void OnAfterAntagSelected(Entity<CorvaxWizardRuleComponent> ent, ref AfterAntagEntitySelectedEvent args)
    {
        MakeWizard(args.EntityUid, ent.Comp);
    }

    public bool MakeWizard(EntityUid target, CorvaxWizardRuleComponent rule)
    {
        var station = (rule.TargetStation is not null) ? Name(rule.TargetStation.Value) : "the station";

        _antag.SendBriefing(target, Loc.GetString("corvax-wizard-role-greeting", ("station", station)), Color.LightBlue, null);

        return true;
    }
}