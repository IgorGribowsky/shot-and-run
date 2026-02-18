using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class MenuController : MonoBehaviour
{
    public string LabelText = "some text";
    public string ButtonText = "some text";
    public bool InitialActiveState = false;

    private UIDocument _uiDocument;
    private ButtonActionBase _action;

    private Button _button;
    private Label _label;
    private VisualElement _root;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _action = gameObject.GetComponent<ButtonActionBase>();
        _uiDocument = GetComponent<UIDocument>();
        _root = _uiDocument.rootVisualElement;

        _label = _root.Q<Label>("titleLabel");
        _button = _root.Q<Button>("nextLevelButton");

        _button.clicked += _action.Act;
        _button.text = ButtonText;
        _label.text = LabelText;

        _root.style.display = InitialActiveState ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void SetField(string key, string value)
    {
        _label.text = _label.text.Replace("{" + key + "}", value);
        _button.text = _button.text.Replace("{" + key + "}", value);
    }

    public void Show()
    {
        _root.style.display = DisplayStyle.Flex;
    }
}
