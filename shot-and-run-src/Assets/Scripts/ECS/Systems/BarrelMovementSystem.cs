using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class BarrelMovementSystem : TrackObjectMovementSystem<Barrel>
    {
        protected override void ProcessAdditionalMovement(Entity entity, ref View view, float vz)
        {
            var r = view.Transform.localScale.y / 2f;
            var wDeg = (vz / r) * Mathf.Rad2Deg;

            view.Transform.Rotate(0, 0, -wDeg);
        }
    }
}
