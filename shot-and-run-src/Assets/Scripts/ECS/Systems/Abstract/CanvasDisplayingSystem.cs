using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Abstract
{
    public abstract class CanvasDisplayingSystem<TData, TCanvas> : ISystem
            where TData : struct, IComponent
            where TCanvas : struct, IComponent, ICanvas
    {
        public World World { get; set; }

        protected Stash<TData> DataStash;
        protected Stash<TCanvas> CanvasStash;
        protected Stash<View> ViewsStash;

        private Filter _filter;

        public virtual void OnAwake()
        {
            DataStash = World.GetStash<TData>();
            CanvasStash = World.GetStash<TCanvas>();
            ViewsStash = World.GetStash<View>();

            _filter = World.Filter
                .With<TData>()
                .With<TCanvas>()
                .With<View>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var data = ref DataStash.Get(entity);
                ref var canvas = ref CanvasStash.Get(entity);
                ref var view = ref ViewsStash.Get(entity);

                if (canvas.IsTextUpdated)
                {
                    UpdateText(ref data, ref canvas);
                    canvas.IsTextUpdated = false;
                }

                canvas.CanvasTransform.position = view.Transform.position + GetOffset(entity);
            }
        }

        protected abstract void UpdateText(ref TData data, ref TCanvas canvas);
        protected abstract Vector3 GetOffset(Entity entity);

        public void Dispose() { }
    }
}
