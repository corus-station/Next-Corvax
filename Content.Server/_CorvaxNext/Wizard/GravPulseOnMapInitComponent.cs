namespace Content.Server._CorvaxNext.Wizard;

[RegisterComponent]
public sealed partial class GravPulseOnMapInitComponent : Component
{
    [DataField]
    public float MaxRange;

    [DataField]
    public float MinRange;

    [DataField]
    public float BaseRadialAcceleration;

    [DataField]
    public float BaseTangentialAcceleration;
}