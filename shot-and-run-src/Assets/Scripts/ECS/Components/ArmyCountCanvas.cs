using Scellecs.Morpeh;
using TMPro;
using UnityEngine;

public struct ArmyCountCanvas : IComponent, ICanvas
{
    public TMP_Text Text;

    public Transform CanvasTransform;

    public bool IsTextUpdated;

    bool ICanvas.IsTextUpdated
    {
        get => IsTextUpdated;
        set => IsTextUpdated = value;
    }

    Transform ICanvas.CanvasTransform
    {
        get => CanvasTransform;
        set => CanvasTransform = value;
    }
    TMP_Text ICanvas.Text
    {
        get => Text;
        set => Text = value;
    }
}
