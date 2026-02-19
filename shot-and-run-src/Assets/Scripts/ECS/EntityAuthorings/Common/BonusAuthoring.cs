using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Domen.Enums;
using Assets.Scripts.Domen.Helpers;
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
    [Inject] private ICanvasBindingService _canvasBindingService;

    private void Awake()
    {
        _bonusStash = _world.GetStash<Bonus>();
        _bonusCanvasStash = _world.GetStash<BonusCanvas>();

        _bonusCanvasInstance = Instantiate(BonusCanvas.gameObject, gameObject.transform.position, Quaternion.identity);
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

        _canvasBindingService.BindCanvasToEntity(entity, _bonusCanvasStash, _bonusCanvasObj);
    }

    private void OnDestroy()
    {
        Destroy(_bonusCanvasInstance);
    }
}
