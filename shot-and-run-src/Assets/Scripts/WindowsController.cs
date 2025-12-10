using UnityEngine;

public class WindowsController : MonoBehaviour
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

            var moveVectore = new Vector2(x, y);

            if (x != 0 || y != 0)
            {
                Debug.Log(moveVectore);
                _heroController.Move(moveVectore);
            }
        }
    }
}
