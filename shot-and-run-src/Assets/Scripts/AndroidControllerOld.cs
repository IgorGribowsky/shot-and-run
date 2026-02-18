using UnityEngine;

public class AndroidControllerOld : MonoBehaviour
{
    public Joystick joystick;
    public float sensitivity = 0.15f;
    private UnitController _heroController;

    // Start is called before the first frame update
    void Start()
    {
        _heroController = gameObject.GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        var moveVector = new Vector2(x, y) * sensitivity;
        if (x != 0 || y != 0)
        {
            _heroController.Move(moveVector);
        }
    }
}
