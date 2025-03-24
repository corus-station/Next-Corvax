using Content.Shared.Actions;
using Content.Shared.Magic;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._CorvaxNext.Wizard;

public sealed partial class CluwneCurseEvent : EntityTargetActionEvent, ISpeakSpell
{
    [DataField]
    public string? Speech { get; private set; }

    [DataField]
    public TimeSpan ParalyzeDuration = TimeSpan.FromSeconds(5);

    [DataField]
    public TimeSpan JitterStutterDuration = TimeSpan.FromSeconds(30);
}

public sealed partial class BananaTouchEvent : EntityTargetActionEvent, ISpeakSpell
{
    [DataField]
    public string? Speech { get; private set; }

    [DataField]
    public ProtoId<StartingGearPrototype> Gear = "ClownGear";

    [DataField]
    public TimeSpan ParalyzeDuration = TimeSpan.FromSeconds(5);

    [DataField]
    public TimeSpan JitterStutterDuration = TimeSpan.FromSeconds(30);
}

public sealed partial class MimeMalaiseEvent : EntityTargetActionEvent, ISpeakSpell
{
    [DataField]
    public string? Speech { get; private set; }

    [DataField]
    public ProtoId<StartingGearPrototype> Gear = "MimeGear";

    [DataField]
    public TimeSpan WizardMuteDuration = TimeSpan.FromSeconds(15);

    [DataField]
    public TimeSpan ParalyzeDuration = TimeSpan.FromSeconds(5);
}