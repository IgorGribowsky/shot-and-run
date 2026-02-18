using UnityEngine;

public class DeviceController : MonoBehaviour
{
    private AndroidControllerOld androidController;
    private WindowsControllerOld windowsController;

    public bool useAndroidController = false; 

    // Start is called before the first frame update
    void Start()
    {
        if (useAndroidController)
        {
            UseAndroid();
            return;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            UseAndroid();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            UseWindows();
        }
    }

    private void UseAndroid()
    {
        androidController = GetComponent<AndroidControllerOld>();
        androidController.joystick.gameObject.SetActive(true);
        androidController.enabled = true;
    }

    private void UseWindows()
    {
        windowsController = GetComponent<WindowsControllerOld>();
        windowsController.enabled = true;
    }
}
