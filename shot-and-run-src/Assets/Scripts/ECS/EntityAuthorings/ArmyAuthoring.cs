using Assets.Scripts.Domen.Helpers;
using Scellecs.Morpeh;
using System;
using TMPro;
using UnityEngine;
using Zenject;
using static UnityEngine.EventSystems.EventTrigger;

public class ArmyAuthoring : MonoBehaviour, IEntityAuthoring
{
    public int UntisCount = 1;

    //ArmyStats
    public float Damage = 1f;
    public float AttackSpeed = 2.5f;
    public float BulletSpeed = 10f;
    public float BulletRange = 10f;

    private Entity _entity;

    private Stash<Army> _armyStash;
    private Stash<View> _viewStash;
    private Stash<ArmyStats> _armyStatsStash;
    private Stash<UnitsCount> _unitsCountStash;
    private Stash<ArmyCountCanvas> _armyCountCanvasStash;

    public Canvas ArmyCountCanvas;

    private Canvas _armyCountCanvasObj;
    private GameObject _armyCountCanvasInstance;

    [Inject] private World _world;
    [Inject] private ICanvasBindingService _canvasBindingService;

    public Entity Entity => _entity;

    private void Awake()
    {
        _armyStash = _world.GetStash<Army>();
        _viewStash = _world.GetStash<View>();
        _armyStatsStash = _world.GetStash<ArmyStats>();
        _unitsCountStash = _world.GetStash<UnitsCount>();
        _armyCountCanvasStash = _world.GetStash<ArmyCountCanvas>();

        _armyCountCanvasInstance = Instantiate(ArmyCountCanvas.gameObject);
        _armyCountCanvasObj = _armyCountCanvasInstance.GetComponent<Canvas>();

        ConfigureComponents();

        _canvasBindingService.BindCanvasToEntity(_entity, _armyCountCanvasStash, _armyCountCanvasObj);
    }

    public void ConfigureComponents()
    {
        _entity = _world.CreateEntity();

        _armyStash.Add(_entity);

        ref var unitsCount = ref _unitsCountStash.Add(_entity);
        unitsCount.Value = UntisCount;

        ref var view = ref _viewStash.Add(_entity);
        view.Transform = transform;

        ref var armyStats = ref _armyStatsStash.Add(_entity);
        armyStats.Damage = Damage;
        armyStats.AttackSpeed = AttackSpeed;
        armyStats.BulletSpeed = BulletSpeed;
        armyStats.BulletRange = BulletRange;
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(_entity))
        {
            _world.RemoveEntity(_entity);
        }

        Destroy(_armyCountCanvasInstance);
    }
}
