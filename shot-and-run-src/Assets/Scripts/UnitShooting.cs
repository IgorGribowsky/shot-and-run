using UnityEngine;

public class UnitShooting : MonoBehaviour
{
    private bool shootingEnabled = true;

    private float currentCd = 0f;
    private float rate;

    private PlayerValues _playerValues;
    private BulletPoolOld _bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _playerValues = GameObject.FindGameObjectWithTag("LevelController").GetComponent<PlayerValues>();
        _bulletPool = GameObject.FindGameObjectWithTag("LevelController").GetComponent<BulletPoolOld>();

        if (_playerValues.AttackSpeed > 0)
        {
            shootingEnabled = true;
            rate = 1 / _playerValues.AttackSpeed;
        }
        else
        {
            shootingEnabled = false;
            rate = float.MaxValue;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!shootingEnabled)
        {
            return;
        }

        if (currentCd <= 0)
        {
            Shoot();
            currentCd = rate;
        }
        else
        {
            currentCd -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }
}
