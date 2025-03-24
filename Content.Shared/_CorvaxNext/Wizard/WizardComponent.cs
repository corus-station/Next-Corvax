using Content.Shared.StatusIcon;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CorvaxNext.Wizard;

[RegisterComponent, NetworkedComponent]
public sealed partial class CorvaxWizardComponent : Component
{
    [DataField]
    public ProtoId<FactionIconPrototype> StatusIcon = "CorvaxWizardFaction";
}