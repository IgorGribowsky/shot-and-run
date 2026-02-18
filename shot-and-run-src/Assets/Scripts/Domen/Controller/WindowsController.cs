using UnityEngine;

namespace Assets.Scripts.Domen.Controller
{
    public class WindowsController : IController
    {
        public Vector2 GetDirection()
        {
            if (Input.GetMouseButton(0))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                var moveVector = new Vector2(x, y);

                return moveVector;
            }
            else
            {
                return new Vector2(0, 0);
            }
        }
    }
}
