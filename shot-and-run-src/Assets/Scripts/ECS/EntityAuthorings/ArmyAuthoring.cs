using Scellecs.Morpeh;
using System;
using TMPro;
using UnityEngine;
using Zenject;

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

        BindArmyCountCanvas();
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

    private void BindArmyCountCanvas()
    {
        _armyCountCanvasObj.transform.position = gameObject.transform.position;
        _armyCountCanvasObj.transform.Rotate(new Vector3(0, 90, 0));

        //TODO MOVE TO HELPER
        TMP_Text text = null;

        foreach (Transform child in _armyCountCanvasObj.transform)
        {
            if (child.CompareTag("CanvasText"))
            {
                text = child.GetComponent<TMP_Text>();
            }
        }

        if (text == null)
        {
            throw new ArgumentNullException("ArmyCountCanvas.Text");
        }

        ref var armyCountCanvas = ref _armyCountCanvasStash.Add(Entity);
        armyCountCanvas.Text = text;
        armyCountCanvas.CanvasTransform = _armyCountCanvasObj.transform;
        armyCountCanvas.IsTextUpdated = true;
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
