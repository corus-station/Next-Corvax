using System.Numerics;
using Content.Shared._CorvaxNext.Wizard.TimeStop;
using Content.Shared.Interaction;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics.Systems;

namespace Content.Shared._CorvaxNext.Wizard.Projectiles;

public sealed class HomingProjectileSystem : EntitySystem
{
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly RotateToFaceSystem _rotate = default!;
    [Dependency] private readonly SharedPhysicsSystem _physics = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<HomingProjectileComponent, PhysicsComponent, TransformComponent, FixturesComponent>();

        var xformQuery = GetEntityQuery<TransformComponent>();
        var frozenQuery = GetEntityQuery<FrozenComponent>();
        while (query.MoveNext(out var uid, out var homing, out var physics, out var xform, out var fix))
        {
            if (frozenQuery.HasComp(uid))
                continue;

            if (!xformQuery.TryComp(homing.Target, out var targetXform))
                continue;

            var goalAngle = (_transform.GetMapCoordinates(targetXform).Position -
                             _transform.GetMapCoordinates(xform).Position).ToWorldAngle();

            var speed = float.MaxValue;
            if (homing.HomingSpeed != null)
                speed = MathHelper.DegreesToRadians(homing.HomingSpeed.Value);

            _rotate.TryRotateTo(uid, goalAngle, frameTime, homing.Tolerance, speed, xform);

            var projectileSpeed = physics.LinearVelocity.Length();
            var velocity = _transform.GetWorldRotation(xform).ToWorldVec() * projectileSpeed;
            _physics.SetLinearVelocity(uid, velocity, true, true, fix, physics);
        }
    }
}