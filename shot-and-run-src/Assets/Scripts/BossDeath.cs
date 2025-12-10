using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject Menu;
    private HealthPoints _healthPoints;
    private MenuScript _menuScript;
    private UnitController _unitController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthPoints = GetComponent<HealthPoints>();
        if (Menu != null)
        {
            _menuScript = Menu.GetComponent<MenuScript>();
        }
        else
        {
            _menuScript = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuScript>();
        }

        _unitController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<UnitController>();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        if (_healthPoints.HP <= 0)
        {
            _menuScript.SetField("score", _unitController.Units.Count.ToString());
            _menuScript.Show();
        }
    }
}
