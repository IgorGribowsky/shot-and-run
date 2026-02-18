using Scellecs.Morpeh;
using System;
using TMPro;
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

        BindHealthCanvas(entity);
    }

    private void BindHealthCanvas(Entity entity)
    {
        _healthCanvasObj.transform.position = gameObject.transform.position;
        //TODO Move magical vector to const
        _healthCanvasObj.transform.Rotate(new Vector3(0, 90, 0));

        //TODO MOVE TO HELPER
        TMP_Text text = null;

        foreach (Transform child in _healthCanvasObj.transform)
        {
            if (child.CompareTag("CanvasText"))
            {
                text = child.GetComponent<TMP_Text>();
            }
        }

        if (text == null)
        {
            throw new ArgumentNullException("HealthCanvas.Text");
        }

        ref var healthCanvas = ref _healthCanvasStash.Add(entity);
        healthCanvas.Text = text;
        healthCanvas.CanvasTransform = _healthCanvasObj.transform;
        healthCanvas.IsTextUpdated = true;
    }


    private void OnDestroy()
    {
        Destroy(_healthCanvasInstance);
    }
}
