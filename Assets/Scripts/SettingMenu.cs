using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        AudioManager.instance.currentAudio.volume = volume;
    }
}
