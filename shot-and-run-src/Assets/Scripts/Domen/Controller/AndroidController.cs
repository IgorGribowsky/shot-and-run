using UnityEngine;
using Zenject;

namespace Assets.Scripts.Domen.Controller
{
    public class AndroidController : IController
    {
        [Inject] private Joystick _joystick;

        //TODO MOVE TO CONSTS
        private const float Sensitivity = 0.3f;

        public Vector2 GetDirection()
        {
            float x = _joystick.Horizontal;
            float y = _joystick.Vertical;

            var moveVector = new Vector2(x, y) * Sensitivity;
            return moveVector;
        }
    }
}
