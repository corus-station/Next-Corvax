using Content.Shared._CorvaxNext.Wizard;
using Content.Shared.StatusIcon.Components;

namespace Content.Client._CorvaxNext.Wizard;

public sealed class SpellsSystem : SharedSpellsSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CorvaxWizardComponent, GetStatusIconsEvent>(GetWizardIcon);
    }

    private void GetWizardIcon(Entity<CorvaxWizardComponent> ent, ref GetStatusIconsEvent args)
    {
        if (ProtoMan.TryIndex(ent.Comp.StatusIcon, out var iconPrototype))
            args.StatusIcons.Add(iconPrototype);
    }
}