using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float ZDeadLine = -10;
    public float Speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var Vz = Time.deltaTime * Speed;

        transform.position += new Vector3(0, 0, -1 * Vz);

        if (transform.position.z <= ZDeadLine)
        {
            Destroy(gameObject);
        }
    }
}
