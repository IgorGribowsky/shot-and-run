using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject.Pools
{
    public class UnitPool : MonoPoolableMemoryPool<Vector3, Transform, UnitAuthoring>
    {
    }
}
