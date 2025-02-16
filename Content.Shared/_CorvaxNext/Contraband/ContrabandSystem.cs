using Content.Shared._CorvaxNext.Skills;
using Content.Shared.Examine;

namespace Content.Shared._CorvaxNext.Contraband;

public sealed class ContrabandSystem : EntitySystem
{
    [Dependency] private readonly ILocalizationManager _localization = default!;
    [Dependency] private readonly SharedSkillsSystem _skills = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<ContrabandComponent, ExaminedEvent>(OnExamined);
    }

    private void OnExamined(Entity<ContrabandComponent> entity, ref ExaminedEvent e)
    {
        var str = entity.Comp.Level switch
        {
            ContrabandLevel.Unusual => "contraband-unusual",
            ContrabandLevel.Suspicious => "contraband-suspicious",
            ContrabandLevel.Danger => "contraband-danger",
            _ => null
        };

        if (str is null)
            return;

        e.PushMarkup(_localization.GetString(str));
    }
}
