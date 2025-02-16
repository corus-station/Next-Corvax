using Content.Shared.Examine;

namespace Content.Shared._CorvaxNext.Contraband;

public sealed class CNContrabandSystem : EntitySystem
{
    [Dependency] private readonly ILocalizationManager _localization = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<CNContrabandComponent, ExaminedEvent>(OnExamined);
    }

    private void OnExamined(Entity<CNContrabandComponent> entity, ref ExaminedEvent e)
    {
        var str = entity.Comp.Level switch
        {
            CNContrabandLevel.Unusual => "contraband-unusual",
            CNContrabandLevel.Suspicious => "contraband-suspicious",
            CNContrabandLevel.Danger => "contraband-danger",
            _ => null
        };

        if (str is null)
            return;

        e.PushMarkup(_localization.GetString(str));
    }
}
