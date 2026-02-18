using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class EntityTriggerHandler<TFilter> : MonoBehaviour where TFilter : struct, IComponent
{
    private Stash<CollisionEvent> _collisionsStash;
    private Stash<TFilter> _filterStash;
    private IEntityAuthoring _selfAuthoring;

    [Inject] private World _world;

    private void Awake()
    {
        _collisionsStash = _world.GetStash<CollisionEvent>();
        _filterStash = _world.GetStash<TFilter>();
        _selfAuthoring = gameObject.GetComponent<IEntityAuthoring>();

    }

    private void OnTriggerEnter(Collider other)
    {
        var otherAuthoring = other.GetComponent<IEntityAuthoring>();

        if (_selfAuthoring != null && otherAuthoring != null && _filterStash.Has(otherAuthoring.Entity)) 
        {
            ref var collisionEvent = ref _collisionsStash.Has(_selfAuthoring.Entity)
                ? ref _collisionsStash.Get(_selfAuthoring.Entity)
                : ref _collisionsStash.Add(_selfAuthoring.Entity);

            if (collisionEvent.Others == null)
            {
                collisionEvent.Others = new List<Entity>();
            }

            collisionEvent.Others.Add(otherAuthoring.Entity);
        }
    }
}