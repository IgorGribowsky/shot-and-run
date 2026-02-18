using Assets.Scripts.Domen.Constants;
using Assets.Scripts.ECS.Systems.Abstract;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class ArmyCountCanvasDisplayingSystem : CanvasDisplayingSystem<UnitsCount, ArmyCountCanvas>
    {
        protected override Vector3 GetOffset(Entity entity)
        {
            return CanvasConstants.ArmyCountCanvasOffset;
        }

        protected override void UpdateText(ref UnitsCount unitsCount, ref ArmyCountCanvas canvas)
        {
            canvas.Text.text = unitsCount.Value.ToString();
        }
    }
}
