using Assets.Scripts.Domen.Constants;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Domen.Controller
{
    public class AndroidController : IController
    {
        [Inject] private Joystick _joystick;

        public Vector2 GetDirection()
        {
            float x = _joystick.Horizontal;
            float y = _joystick.Vertical;

            var moveVector = new Vector2(x, y) * InputConstants.AndroidSensivity;
            return moveVector;
        }
    }
}
