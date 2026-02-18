using Assets.Scripts.Domen.Constants;
using Assets.Scripts.ECS.Systems.Abstract;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class HealthCanvasDisplayingSystem : CanvasDisplayingSystem<Health, HealthCanvas>
    {
        private Stash<Barrel> _barrelsStash;
        private Stash<Boss> _bossStash;

        public override void OnAwake()
        {
            base.OnAwake();
            _barrelsStash = World.GetStash<Barrel>();
            _bossStash = World.GetStash<Boss>();
        }

        protected override Vector3 GetOffset(Entity entity)
        {
            return entity switch
            {
                _ when _barrelsStash.Has(entity) => CanvasConstants.HealthCanvasOffsetForBarrel,
                _ when _bossStash.Has(entity) => CanvasConstants.HealthCanvasOffsetForBoss,
                _ => CanvasConstants.HealthCanvasOffsetForArch
            };
        }

        protected override void UpdateText(ref Health data, ref HealthCanvas canvas)
        {
            canvas.Text.text = Mathf.Ceil(data.Value).ToString();
        }
    }
}
