using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.Domen.Helpers
{
    public interface ICanvasBindingService
    {
        public void BindCanvasToEntity<T>(Entity entity, Stash<T> stash, Canvas canvasObj)
            where T : struct, IComponent, ICanvas;
    }
}