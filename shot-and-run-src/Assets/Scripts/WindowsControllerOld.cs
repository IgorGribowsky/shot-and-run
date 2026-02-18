using UnityEngine;

public class WindowsControllerOld : MonoBehaviour
{
    private UnitController _heroController;

    // Start is called before the first frame update
    void Start()
    {
        _heroController = gameObject.GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            var moveVector = new Vector2(x, y);

            if (x != 0 || y != 0)
            {
                _heroController.Move(moveVector);
            }
        }
    }
}
