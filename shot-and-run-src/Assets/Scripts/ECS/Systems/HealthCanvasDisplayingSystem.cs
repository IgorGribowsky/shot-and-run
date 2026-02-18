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
            //TODO Move magical vectors to const
            return entity switch
            {
                _ when _barrelsStash.Has(entity) => new Vector3(1.5f, 2.25f, 0),
                _ when _bossStash.Has(entity) => new Vector3(0, 6, -6),
                _ => new Vector3(0, 2.25f, 0)
            };
        }

        protected override void UpdateText(ref Health data, ref HealthCanvas canvas)
        {
            canvas.Text.text = Mathf.Ceil(data.Value).ToString();
        }
    }
}
