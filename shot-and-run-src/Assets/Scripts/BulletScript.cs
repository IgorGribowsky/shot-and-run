using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float deadTimer;

    private PlayerValues _playerValues;
    private BulletPool _bulletPool;

    // Start is called before the first frame update
    void Awake()
    {
        _bulletPool = GameObject.FindGameObjectWithTag("LevelController").GetComponent<BulletPool>();
        _playerValues = GameObject.FindGameObjectWithTag("LevelController").GetComponent<PlayerValues>();
    }

    private void OnEnable()
    {
        deadTimer = _playerValues.BulletRange / _playerValues.BulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        var dz = _playerValues.BulletSpeed * Time.deltaTime;
        transform.position += new Vector3(0, 0, dz);
        deadTimer -= Time.deltaTime;
        if (deadTimer < 0)
        {
            _bulletPool.Return(gameObject);
        }
    }
}
