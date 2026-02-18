using Assets.Scripts.Domen.Constants;
using Scellecs.Morpeh;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Domen.Helpers
{
    public class CanvasBindingService : ICanvasBindingService
    {
        public void BindCanvasToEntity<T>(Entity entity, Stash<T> stash, Canvas canvasObj)
            where T : struct, IComponent, ICanvas
        {
            if (canvasObj == null) throw new ArgumentNullException(nameof(canvasObj));

            var canvasTransform = canvasObj.transform;
            canvasTransform.Rotate(CanvasConstants.DefaultRotation);

            var text = FindTextInChild(canvasTransform);

            if (text == null)
            {
                throw new ArgumentException($"TMP_Text with tag '{CanvasConstants.Tag}' not found in {canvasObj.name}");
            }

            ref var component = ref stash.Add(entity);
            component.Text = text;
            component.CanvasTransform = canvasTransform;
            component.IsTextUpdated = true;
        }

        private TMP_Text FindTextInChild(Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child.CompareTag(CanvasConstants.Tag))
                {
                    return child.GetComponent<TMP_Text>();
                }
            }
            return null;
        }
    }
}
