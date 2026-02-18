using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class ArchAuthoring : MonoBehaviour, IEntityAuthoring
{
    private Stash<Arch> _archStash;
    private Stash<View> _viewStash;
    private Stash<WaveIdentity> _waveIdentityStash;

    [Inject] private World _world;

    public Entity Entity { get; set; }

    private void Awake()
    {
        _archStash = _world.GetStash<Arch>();
        _viewStash = _world.GetStash<View>();
        _waveIdentityStash = _world.GetStash<WaveIdentity>();

        ConfigureComponents();
    }

    public void ConfigureComponents()
    {
        if (_world.IsDisposed(Entity))
        {
            Entity = _world.CreateEntity();
        }

        _archStash.Add(Entity);

        ref var view = ref _viewStash.Add(Entity);
        view.Transform = transform;

        ref var waveId = ref _waveIdentityStash.Add(Entity);
        waveId.Value = default;
    }

    public void SetWaveId(int id)
    {
        ref var waveId = ref _waveIdentityStash.Get(Entity);
        waveId.Value = id;
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(Entity))
        {
            _world.RemoveEntity(Entity);
        }
    }
}
