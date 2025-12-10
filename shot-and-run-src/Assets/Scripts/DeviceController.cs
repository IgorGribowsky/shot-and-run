using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    private AndroidController androidController;
    private WindowsController windowsController;

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
        androidController = GetComponent<AndroidController>();
        androidController.joystick.gameObject.SetActive(true);
        androidController.enabled = true;
    }

    private void UseWindows()
    {
        windowsController = GetComponent<WindowsController>();
        windowsController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
