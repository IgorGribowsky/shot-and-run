using Assets.Scripts.Domen.Helpers;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class HealthAuthoring : MonoBehaviour
{
    public Canvas HealthCanvas;

    private Canvas _healthCanvasObj;
    private GameObject _healthCanvasInstance;

    private Stash<Health> _healthStash;
    private Stash<HealthCanvas> _healthCanvasStash;


    [Inject] private World _world;
    [Inject] private ICanvasBindingService _canvasBindingService;

    private void Awake()
    {
        _healthStash = _world.GetStash<Health>();
        _healthCanvasStash = _world.GetStash<HealthCanvas>();

        _healthCanvasInstance = Instantiate(HealthCanvas.gameObject);
        _healthCanvasObj = _healthCanvasInstance.GetComponent<Canvas>();
    }

    public void SetHealth(Entity entity, float value)
    {
        if (!_healthStash.Has(entity))
        {
            _healthStash.Add(entity);
        }

        ref var bonus = ref _healthStash.Get(entity);
        bonus.Value = value;

        _canvasBindingService.BindCanvasToEntity(entity, _healthCanvasStash, _healthCanvasObj);
    }

    private void OnDestroy()
    {
        Destroy(_healthCanvasInstance);
    }
}
