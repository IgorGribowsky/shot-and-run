using UnityEngine;

public abstract class ButtonActionBase : MonoBehaviour
{
    protected LevelManager LevelManager;

    private void Start()
    {
        LevelManager = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<LevelManager>();
    }

    public abstract void Act();
}