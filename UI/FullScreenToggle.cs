using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    public Toggle fullScreenToggle;

    void Start()
    {
        fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });
        LoadFullScreenSetting();
    }

    void LoadFullScreenSetting()
    {
        bool isFullScreen = PlayerPrefs.GetInt("isFullScreen", 1) == 1;
        fullScreenToggle.isOn = isFullScreen;
    }

    void SetFullScreen()
    {
        bool isFullScreen = fullScreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("isFullScreen", isFullScreen ? 1 : 0);
    }
}
