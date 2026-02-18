using TMPro;
using UnityEngine;

public interface ICanvas
{
    bool IsTextUpdated { get; set; }
    Transform CanvasTransform { get; set; }
    TMP_Text Text { get; set; }
}
