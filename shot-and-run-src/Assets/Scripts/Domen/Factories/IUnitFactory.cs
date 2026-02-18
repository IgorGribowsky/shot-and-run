
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.Domen.Factories
{
    public interface IUnitFactory
    {
        public int UnitsCount { get; }

        public UnitAuthoring CreateUnit(Vector3 position, Transform parentTransform);

        public void DeleteLastUnit();

        public bool TryDeleteSpecificUnit(Entity entity);
    }
}
