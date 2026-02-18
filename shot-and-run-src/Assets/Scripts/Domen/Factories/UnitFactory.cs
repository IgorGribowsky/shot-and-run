using Assets.Scripts.Zenject.Pools;
using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Domen.Factories
{
    public class UnitFactory : IUnitFactory
    {
        public int UnitsCount => _unitsList.Count;

        //For searching
        private Dictionary<Entity, UnitAuthoring> _unitsMap;
        //For fast deleting last element
        private List<UnitAuthoring> _unitsList;

        private UnitPool _pool;

        public UnitFactory(UnitPool pool)
        {
            _unitsMap = new Dictionary<Entity, UnitAuthoring>();
            _unitsList = new List<UnitAuthoring>();

            _pool = pool;
        }

        public UnitAuthoring CreateUnit(Vector3 position, Transform parentTransform)
        {
            var createdUnit = _pool.Spawn(position, parentTransform);

            _unitsMap.Add(createdUnit.Entity, createdUnit);
            _unitsList.Add(createdUnit);

            return createdUnit;
        }

        public void DeleteLastUnit()
        {
            var lastIndex = _unitsList.Count - 1;
            var lastUnit = _unitsList[lastIndex];

            _unitsMap.Remove(lastUnit.Entity);
            _unitsList.RemoveAt(lastIndex);

            _pool.Despawn(lastUnit);
        }

        public bool TryDeleteSpecificUnit(Entity entity)
        {
            if (!_unitsMap.TryGetValue(entity, out var unit))
            {
                return false;
            }

            _unitsMap.Remove(entity);

            //To not shift all elements after deleting
            int index = _unitsList.IndexOf(unit);
            if (index >= 0)
            {
                int lastIndex = _unitsList.Count - 1;
                _unitsList[index] = _unitsList[lastIndex];
                _unitsList.RemoveAt(lastIndex);
            }

            _pool.Despawn(unit);
            return true;
        }
    }
}
