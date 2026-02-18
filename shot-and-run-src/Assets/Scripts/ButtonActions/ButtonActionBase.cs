using UnityEngine;
using Zenject;

public abstract class ButtonActionBase : MonoBehaviour
{
    protected LevelManager LevelManager;

    [Inject]
    private void Construct(LevelManager levelManager)
    {
        LevelManager = levelManager;
    }

    public abstract void Act();
}