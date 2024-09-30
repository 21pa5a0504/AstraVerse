using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public AudioSource buttonClickSound;

    public Slider volumeSlider;
    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;

    private Resolution[] resolutions;
    private float savedVolume = 1f;
    private int savedResolutionIndex;
    private bool isFullScreen = true;

    void Start()
    {
        LoadSettings();
        InitializeResolutions();
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(); });
        fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });
    }

    void LoadSettings()
    {
        savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        savedResolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        isFullScreen = PlayerPrefs.GetInt("isFullScreen", 1) == 1;

        volumeSlider.value = savedVolume;
        fullScreenToggle.isOn = isFullScreen;
    }

    public void PlayGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        PlayButtonSound();
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        PlayButtonSound();
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
    }

    void PlayButtonSound()
    {
        buttonClickSound.Play();
    }

    void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new Dropdown.OptionData(option));
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void SetVolume()
    {
        savedVolume = volumeSlider.value;
        AudioListener.volume = savedVolume;
        PlayerPrefs.SetFloat("volume", savedVolume);
    }

    void SetResolution()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionDropdown.value);
    }

    void SetFullScreen()
    {
        isFullScreen = fullScreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("isFullScreen", isFullScreen ? 1 : 0);
    }
}
