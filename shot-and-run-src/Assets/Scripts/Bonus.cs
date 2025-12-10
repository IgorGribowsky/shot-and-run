using System;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public BonusSign bonusSign = BonusSign.Plus;
    public int Value = 2;

    public Canvas BonusCanvas;

    public bool BonusIsAvailable { get; set; } = true;

    private Canvas _bonusCanvas;
    private TMP_Text _text;
    private UnitController _unitController;
    private ConnectedArch _connectedArch;

    // Start is called before the first frame update
    void Start()
    {
        _connectedArch = GetComponent<ConnectedArch>();
        _unitController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<UnitController>();
        if (Value != 0)
        {
            CreateBonusCanvas();
        }
    }

    private void CreateBonusCanvas()
    {
        _bonusCanvas = GameObject.Instantiate(BonusCanvas);
        _bonusCanvas.transform.rotation = Quaternion.identity;
        _bonusCanvas.transform.Rotate(new Vector3(0, -90, -90));
        _bonusCanvas.transform.SetParent(transform, false);

        if (gameObject.tag == "Arch")
        {
            _bonusCanvas.transform.localScale = new Vector3(0.5f, 1, 1);
        }

        foreach (Transform child in _bonusCanvas.transform)
        {
            if (child.CompareTag("CanvasText"))
            {
                _text = child.GetComponent<TMP_Text>();
                string sign = "";
                switch (bonusSign)
                {
                    case BonusSign.Plus:
                        sign = "+";
                        break;

                    case BonusSign.Minus:
                        sign = "-";
                        break;

                    case BonusSign.Divide:
                        sign = "/";
                        break;

                    case BonusSign.Multiple:
                        sign = "x";
                        break;
                }

                _text.text = sign + Value.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (_bonusCanvas != null)
        {
            if (gameObject.tag == "Arch")
            {
                _bonusCanvas.transform.position = transform.position + new Vector3(0, 2, 0);
            }
            else if (gameObject.tag == "Barrel")
            {
                _bonusCanvas.transform.position = transform.position + new Vector3(1.5f, 0.75f, -1.25f);
                _bonusCanvas.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void GetAward()
    {
        if (BonusIsAvailable)
        {
            switch (bonusSign)
            {
                case BonusSign.Plus:
                    _unitController.AddUnits(Value);
                    break;

                case BonusSign.Minus:
                    _unitController.RemoveUnits(Value);
                    break;

                case BonusSign.Divide:
                    _unitController.DevidUnits(Value);
                    break;

                case BonusSign.Multiple:
                    _unitController.Multiple(Value);
                    break;
            }

            if (_connectedArch != null)
            {
                _connectedArch.InactiveAllConnectedBonuses();
            }
        }
    }
}

[Serializable]
public enum BonusSign
{
    Plus,
    Minus,
    Multiple,
    Divide,
}
