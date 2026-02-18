using Assets.Scripts.Domen.Enums;
using Scellecs.Morpeh;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class BonusAuthoring : MonoBehaviour
{
    public Canvas BonusCanvas;

    private Canvas _bonusCanvasObj;
    private GameObject _bonusCanvasInstance;

    private Stash<Bonus> _bonusStash;
    private Stash<BonusCanvas> _bonusCanvasStash;

    [Inject] private World _world;

    private void Awake()
    {
        _bonusStash = _world.GetStash<Bonus>();
        _bonusCanvasStash = _world.GetStash<BonusCanvas>();

        _bonusCanvasInstance = Instantiate(BonusCanvas.gameObject);
        _bonusCanvasObj = _bonusCanvasInstance.GetComponent<Canvas>();
    }

    public void SetBonus(Entity entity, BonusSign bonusSign, int value)
    {
        if (!_bonusStash.Has(entity))
        {
            _bonusStash.Add(entity);
        }

        ref var bonus = ref _bonusStash.Get(entity);
        bonus.BonusSign = bonusSign;
        bonus.Value = value;

        BindBonusCanvas(entity);
    }

    private void BindBonusCanvas(Entity entity)
    {
        _bonusCanvasObj.transform.position = gameObject.transform.position;
        _bonusCanvasObj.transform.Rotate(new Vector3(0, 90, 0));

        //TODO MOVE TO HELPER
        TMP_Text text = null;

        foreach (Transform child in _bonusCanvasObj.transform)
        {
            if (child.CompareTag("CanvasText"))
            {
                text = child.GetComponent<TMP_Text>();
            }
        }

        if (text == null)
        {
            throw new ArgumentNullException("BonusCanvas.Text");
        }

        ref var bonusCanvas = ref _bonusCanvasStash.Add(entity);
        bonusCanvas.Text = text;
        bonusCanvas.CanvasTransform = _bonusCanvasObj.transform;
        bonusCanvas.IsTextUpdated = true;
    }

    private void OnDestroy()
    {
        Destroy(_bonusCanvasInstance);
    }
}
