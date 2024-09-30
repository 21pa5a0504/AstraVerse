using UnityEngine;
using UnityEngine.UI;

public class GraphicsMenu : MonoBehaviour
{
    public Dropdown qualityDropdown;
    public Toggle vSyncToggle;

    void Start()
    {
        qualityDropdown.onValueChanged.AddListener(delegate { SetQuality(); });
        vSyncToggle.onValueChanged.AddListener(delegate { ToggleVSync(); });
        LoadGraphicsSettings();
    }

    void LoadGraphicsSettings()
    {
        int savedQuality = PlayerPrefs.GetInt("qualityLevel", 2);
        bool vSyncEnabled = PlayerPrefs.GetInt("vSyncEnabled", 0) == 1;

        qualityDropdown.value = savedQuality;
        vSyncToggle.isOn = vSyncEnabled;
    }

    void SetQuality()
    {
        int qualityIndex = qualityDropdown.value;
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityLevel", qualityIndex);
    }

    void ToggleVSync()
    {
        bool vSync = vSyncToggle.isOn;
        QualitySettings.vSyncCount = vSync ? 1 : 0;
        PlayerPrefs.SetInt("vSyncEnabled", vSync ? 1 : 0);
    }
}
