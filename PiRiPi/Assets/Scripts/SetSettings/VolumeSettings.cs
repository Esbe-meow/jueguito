using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_SFX = "sfxVolume";
    public const string MIXER_MUSIC = "MusicVolume";


    private void Awake() 
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void Start() 
    {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
    }

    void OnDisable() 
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);    
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    void SetSfxVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }
}
