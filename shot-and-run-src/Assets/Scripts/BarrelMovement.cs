using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    public float ZDeadLine = -10;
    public float Speed = 5;

    void Update()
    {
        var Vz = Time.deltaTime * Speed;
        var r = transform.localScale.y / 2;
        var Wrad = Vz / r;
        var W = Wrad * 180f / Mathf.PI;

        transform.position += new Vector3(0, 0, -1 * Vz);
        transform.Rotate(0, 0, -1 * W);

        if (transform.position.z <= ZDeadLine)
        {
            Destroy(gameObject);
        }
    }
}
