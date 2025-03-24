using Content.Shared.NPC.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server._CorvaxNext.Wizard;

[RegisterComponent, Access(typeof(CorvaxWizardRuleSystem))]
public sealed partial class CorvaxWizardRuleComponent : Component
{
    [DataField]
    public ProtoId<NpcFactionPrototype> Faction = "CorvaxWizard";

    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? TargetStation;
}