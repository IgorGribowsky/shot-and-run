using Assets.Scripts.Domen.Enums;
using Assets.Scripts.ECS.Systems.Abstract;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class BonusCanvasDisplayingSystem : CanvasDisplayingSystem<Bonus, BonusCanvas>
    {
        private Stash<Barrel> _barrelsStash;

        public override void OnAwake()
        {
            base.OnAwake();
            _barrelsStash = World.GetStash<Barrel>();
        }

        protected override Vector3 GetOffset(Entity entity)
        {
            //TODO Move magical vectors to const
            return _barrelsStash.Has(entity)
                ? new Vector3(1.5f, 0.5f, -1.25f)
                : new Vector3(0, 0.5f, -0.75f);
        }

        protected override void UpdateText(ref Bonus bonus, ref BonusCanvas canvas)
        {
            string sign = bonus.BonusSign switch
            {
                BonusSign.Plus => "+",
                BonusSign.Minus => "-",
                BonusSign.Divide => "/",
                BonusSign.Multiple => "x",
                _ => ""
            };
            canvas.Text.text = $"{sign}{bonus.Value}";
        }
    }
}
