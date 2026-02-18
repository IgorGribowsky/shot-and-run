using Assets.Scripts.Domen.Enums;
using Assets.Scripts.ECS.Systems.Abstract;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class ArmyCountCanvasDisplayingSystem : CanvasDisplayingSystem<UnitsCount, ArmyCountCanvas>
    {
        protected override Vector3 GetOffset(Entity entity)
        {
            //TODO Move magical vectors to const
            return new Vector3(0, 2.5f, 0);
        }

        protected override void UpdateText(ref UnitsCount unitsCount, ref ArmyCountCanvas canvas)
        {
            canvas.Text.text = unitsCount.Value.ToString();
        }
    }
}
