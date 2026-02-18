using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject.Pools
{
    public class BulletPool : MonoPoolableMemoryPool<Vector3, float, BulletAuthoring>
    {
    }
}
