using Scellecs.Morpeh;
using TMPro;
using UnityEngine;

public struct HealthCanvas : IComponent, ICanvas
{
    public TMP_Text Text;

    public Transform CanvasTransform;

    public bool IsTextUpdated;

    bool ICanvas.IsTextUpdated
    {
        get => IsTextUpdated;
        set => IsTextUpdated = value;
    }

    Transform ICanvas.CanvasTransform => CanvasTransform;
    TMP_Text ICanvas.Text => Text;
}
