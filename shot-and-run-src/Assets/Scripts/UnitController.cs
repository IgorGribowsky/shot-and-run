using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float Speed = 50f;

    public List<GameObject> Units = new List<GameObject>();
    public Canvas CountCanvas;
    public float CanvasY = 2.5f;

    private PlayerValues _playerValues;
    private LevelController _levelController;
    private LevelManager _levelManager;

    private Canvas _countCanvas;
    private TMP_Text _countText;

    // Start is called before the first frame update
    void Start()
    {
        _playerValues = gameObject.GetComponent<PlayerValues>();
        _levelController = gameObject.GetComponent<LevelController>();
        _levelManager = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<LevelManager>();

        _countCanvas = GameObject.Instantiate(CountCanvas, Vector3.one, Quaternion.identity);
        _countCanvas.transform.SetParent(transform, false);
        foreach (Transform child in _countCanvas.transform)
        {
            if (child.CompareTag("CanvasText"))
            {
                _countText = child.GetComponent<TMP_Text>();
                _countText.text = Units.Count.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Units.Count > 0)
        {
            _countCanvas.transform.position = Units.First().transform.position + new Vector3(0, CanvasY, 0);
        }
        else
        {
            _levelManager?.LoadCurrentLevel();
        }
    }

    public void Move(Vector2 moveVector)
    {
        if (Units.Count == 0)
        {
            return;
        }

        var unitPositions = Units.Select(u => u.transform.position).ToList();
        var leftUnitX = unitPositions.Min(p => p.x);
        var rightUnitX = unitPositions.Max(p => p.x);
        var downUnitZ = unitPositions.Min(p => p.z);
        var topUnitZ = unitPositions.Max(p => p.z);

        var moveX = moveVector.x * Time.deltaTime * Speed;
        var moveY = moveVector.y * Time.deltaTime * Speed;

        var x = CalculateMove(leftUnitX, rightUnitX, moveX, _levelController.MinX, _levelController.MaxX);
        var z = CalculateMove(downUnitZ, topUnitZ, moveY, _levelController.MinZ, _levelController.MaxZ);

        foreach (var unit in Units)
        {
            unit.transform.position += new Vector3(x, 0, z);
        }
    }

    private float CalculateMove(float minPos, float maxPos, float move, float minLimit, float maxLimit)
    {
        if (move < 0)
        {
            var newPos = minPos + move;
            return newPos < minLimit ? minLimit - minPos : move;
        }
        else
        {
            var newPos = maxPos + move;
            return newPos > maxLimit ? maxLimit - maxPos : move;
        }
    }

    public void AddUnits(int num)
    {
        var positionToAdd = Units.Count != 0 
            ? Units.First().transform.position 
            : new Vector3(0, -0.5f, 0);

        for (int i = 0; i < num; i++)
        {
            var randomVector2 = Random.insideUnitCircle;
            var createdUnit = GameObject.Instantiate(_playerValues.Unit, positionToAdd + new Vector3(randomVector2.x, 0, randomVector2.y) * 2.0f + Vector3.one * 0.1f, _playerValues.Unit.transform.rotation);

            Units.Add(createdUnit);

            UpdateCountCanvas();
        }
    }

    public void RemoveUnits(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (Units.Count <= 1)
            {
                return;
            }

            var lastUnit = Units.Last();

            Units.Remove(lastUnit);
            Destroy(lastUnit);

            UpdateCountCanvas();
        }
    }

    public void RemoveUnit(GameObject unit)
    {
        Units.Remove(unit);
        Destroy(unit);

        UpdateCountCanvas();
    }

    public void DevidUnits(int num)
    {
        var minus = (int)Mathf.Ceil(Units.Count - (Units.Count / (num * 1.0f)));
        RemoveUnits(minus);
    }

    public void Multiple(int num)
    {
        var plus = Units.Count * (num - 1);
        AddUnits(plus);
    }

    public void UpdateCountCanvas()
    {
        _countText.text = Units.Count.ToString();
    }
}
