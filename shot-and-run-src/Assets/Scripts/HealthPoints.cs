using TMPro;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    public float HP = 35;
    public Canvas HPCanvas;

    private PlayerValues _playerValues;
    private BulletPoolOld _bulletPool;
    private BonusGain _bonus;

    private Canvas _hpCanvas;
    private TMP_Text _hpText;

    // Start is called before the first frame update
    void Start()
    {
        _playerValues = GameObject.FindGameObjectWithTag("LevelController").GetComponent<PlayerValues>();
        _bulletPool = GameObject.FindGameObjectWithTag("LevelController").GetComponent<BulletPoolOld>();
        _bonus = GetComponent<BonusGain>();
        _hpCanvas = GameObject.Instantiate(HPCanvas);
        _hpCanvas.transform.SetParent(transform, false);
        foreach (Transform child in _hpCanvas.transform)
        {
            if (child.CompareTag("CanvasText"))
            {
                _hpText = child.GetComponent<TMP_Text>();
                _hpText.text = HP.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            if (_bonus != null)
            {
                _bonus.GetAward();
            }

            GameObject.Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        var isBoss = transform.tag == "Boss";

        var addVector = isBoss 
            ? new Vector3(0, 5, -6)
            : new Vector3(1.75f, 2, 0);

        _hpCanvas.transform.position = transform.position + addVector;
        _hpCanvas.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            _bulletPool.Return(other.gameObject);
            HP -= _playerValues.Damage;
            _hpText.text = HP.ToString();
        }
    }

    public void SetHP(float newHp)
    {
        HP = newHp;
    }
}
