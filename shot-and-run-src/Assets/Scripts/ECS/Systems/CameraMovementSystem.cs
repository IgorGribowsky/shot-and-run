using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Domen.Enums;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class CameraMovementSystem : ISystem
    {
        public World World { get; set; }

        private Entity _cameraEntity;

        private Stash<View> _viewStash;
        private Stash<CameraEntity> _cameraStash;

        public void OnAwake()
        {
            _cameraEntity = World.Filter
                .With<CameraEntity>()
                .With<View>()
                .Build()
                .First();

            _viewStash = World.GetStash<View>();
            _cameraStash = World.GetStash<CameraEntity>();
        }

        public void OnUpdate(float deltaTime)
        {
            ref var camera = ref _cameraStash.Get(_cameraEntity);

            if (camera.Phase == CameraPhase.Rotating)
            {
                ref var cameraView = ref _viewStash.Get(_cameraEntity);
                RotateCamera(ref cameraView, ref camera, deltaTime);
            }
        }

        private void RotateCamera(ref View cameraView, ref CameraEntity camera, float deltaTime)
        {
            int completedRotationSteps = 0;
            var newY = cameraView.Transform.position.y + deltaTime;

            if (newY > GameConstants.BossFightCameraY)
            {
                newY = GameConstants.BossFightCameraY;
                completedRotationSteps += 1;
            }

            var newXAng = cameraView.Transform.eulerAngles.x - deltaTime * 10;

            if (newXAng < GameConstants.BossFightCameraXAng)
            {
                newXAng = GameConstants.BossFightCameraXAng;
                completedRotationSteps += 1;
            }

            cameraView.Transform.position = new Vector3(cameraView.Transform.position.x, newY, cameraView.Transform.position.z);
            cameraView.Transform.eulerAngles = new Vector3(newXAng, cameraView.Transform.eulerAngles.y, cameraView.Transform.eulerAngles.z);

            if (completedRotationSteps == 2)
            {
                camera.Phase = CameraPhase.PostRotation;
            }
        }

        public void Dispose() { }
    }
}
