using Assets.Scripts.Domen.Constants;
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
            return _barrelsStash.Has(entity)
                ? CanvasConstants.BonusCanvasOffsetForBarrel
                : CanvasConstants.BonusCanvasOffsetArch;
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
