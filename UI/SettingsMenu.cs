using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Text sensitivityValueText;
    public Slider brightnessSlider;
    public Text brightnessValueText;

    private float savedSensitivity = 1f;
    private float savedBrightness = 1f;

    void Start()
    {
        LoadSettings();
        sensitivitySlider.onValueChanged.AddListener(delegate { AdjustSensitivity(); });
        brightnessSlider.onValueChanged.AddListener(delegate { AdjustBrightness(); });
    }

    void LoadSettings()
    {
        savedSensitivity = PlayerPrefs.GetFloat("sensitivity", 1f);
        savedBrightness = PlayerPrefs.GetFloat("brightness", 1f);

        sensitivitySlider.value = savedSensitivity;
        brightnessSlider.value = savedBrightness;

        sensitivityValueText.text = savedSensitivity.ToString("F1");
        brightnessValueText.text = savedBrightness.ToString("F1");
    }

    void AdjustSensitivity()
    {
        savedSensitivity = sensitivitySlider.value;
        sensitivityValueText.text = savedSensitivity.ToString("F1");
        PlayerPrefs.SetFloat("sensitivity", savedSensitivity);
    }

    void AdjustBrightness()
    {
        savedBrightness = brightnessSlider.value;
        brightnessValueText.text = savedBrightness.ToString("F1");
        RenderSettings.ambientLight = Color.white * savedBrightness;
        PlayerPrefs.SetFloat("brightness", savedBrightness);
    }
}
