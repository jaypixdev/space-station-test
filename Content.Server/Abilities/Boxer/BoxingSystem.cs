using Content.Shared.Damage.Events;
using Content.Shared.Weapons.Melee;
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Containers;

namespace Content.Server.Abilities.Boxer
{
    public sealed class BoxingSystem : EntitiesSystem
    {
        // Dependency injection for SharedContainerSystem
        [Dependency] private readonly SharedContainerSystem _containerSystem = default!;

        public override void Initialize()
        {
            base.Initialize();
            // Subscribe to the local event for when the BoxerComponent is initialized
            SubscribeLocalEvent<BoxerComponent, ComponentInit>(OnInit);
            // Subscribe to the local event for when the BoxerComponent is hit in melee combat
            SubscribeLocalEvent<BoxerComponent, MeleeHitEvent>(OnMeleeHit);
            // Subscribe to the local event for when the BoxingGlovesComponent is hit in melee combat
            SubscribeLocalEvent<BoxingGlovesComponent, StaminaMeleeHitEvent>(OnStamHit);
        }

        private void OnInit(EntityUid uid, BoxerComponent component, ComponentInit args)
        {
            // Check if the entity has a MeleeWeaponComponent and if so, increase its range by the range bonus of the BoxerComponent
            if (TryComp<MeleeWeaponComponent>(uid, out var meleeComp))
                meleeComp.Range *= component.RangeBonus;
        }
        private void OnMeleeHit(EntityUid uid, BoxerComponent component, MeleeHitEvent args)
        {
            // Add the unarmed modifiers of the BoxerComponent to the hit's modifiers list
            args.ModifiersList.Add(component.UnarmedModifiers);
        }

        private void OnStamHit(EntityUid uid, BoxingGlovesComponent component, StaminaMeleeHitEvent args)
        {
            // Try to get the container the entity is in
            if (!_containerSystem.TryGetContainingContainer(uid, out var equipee))
                return;

            // Check if the container's owner has a BoxerComponent and if so, multiply the hit's damage by the boxing gloves modifier
            if (TryComp<BoxerComponent>(equipee.Owner, out var boxer))
                args.Multiplier *= boxer.BoxingGlovesModifier;
        }
    }
}

                args.Multiplier *= boxer.BoxingGlovesModifier;
        }
    }
}
