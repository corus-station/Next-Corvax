using Content.Server.Abilities.Mime;
using Content.Server.Administration.Commands;
using Content.Shared._CorvaxNext.Wizard;
using Content.Shared.Clothing.Components;
using Content.Shared.Interaction.Components;
using Content.Shared.Inventory;
using Content.Shared.Speech.Muting;
using Content.Shared.StatusEffect;

namespace Content.Server._CorvaxNext.Wizard;

public sealed class SpellsSystem : SharedSpellsSystem
{
    protected override void SetGear(EntityUid uid, string gear, SlotFlags unremoveableClothingFlags = SlotFlags.NONE)
    {
        base.SetGear(uid, gear, unremoveableClothingFlags);

        SetOutfitCommand.SetOutfit(uid, gear, EntityManager);

        if (unremoveableClothingFlags == SlotFlags.NONE)
            return;

        var enumerator = Inventory.GetSlotEnumerator(uid, unremoveableClothingFlags);
        while (enumerator.MoveNext(out var container))
        {
            if (HasComp<ClothingComponent>(container.ContainedEntity))
                EnsureComp<UnremoveableComponent>(container.ContainedEntity.Value);
        }
    }

    protected override void MakeMime(MimeMalaiseEvent ev, StatusEffectsComponent? status = null)
    {
        base.MakeMime(ev, status);

        var targetWizard = HasComp<WizardComponent>(ev.Target);

        SetGear(ev.Target,
            ev.Gear,
            targetWizard ? SlotFlags.NONE : SlotFlags.MASK | SlotFlags.INNERCLOTHING | SlotFlags.BELT);

        if (!targetWizard)
            EnsureComp<MimePowersComponent>(ev.Target).CanBreakVow = false;
        else
            StatusEffects.TryAddStatusEffect<MutedComponent>(ev.Target, "Muted", ev.WizardMuteDuration, true, status);
    }
}