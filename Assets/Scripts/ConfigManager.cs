using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;

    void Start()
    {
        float volume;
        if (audioMixer.GetFloat("MusicVolume", out volume))
        {
            musicSlider.value = Mathf.Pow(10, volume / 20f);
        }
    }

    public void SetMusicVolume(float volume)
   {
        if (volume <= 0.0001f)
        {
        audioMixer.SetFloat("MusicVolume", -80f); // Mudo
        }
        else
        {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        }
    }

}

