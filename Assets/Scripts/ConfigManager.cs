using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.STP;

public class ConfigManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider soundSlider;
    public Toggle fullScreen;

    public GameDb dB;
    private float volumeMusicaAtual;
    private float volumeEfeitosAtual;
    public Configuracoes configs;

    void Start()
    {
        configs = dB.CarregarConfiguracoes();

        volumeMusicaAtual = configs.VolumeMusica;
        volumeEfeitosAtual = configs.VolumeEfeitos;

        musicSlider.value = volumeMusicaAtual;
        soundSlider.value = volumeEfeitosAtual;

        SetMusicVolume(volumeMusicaAtual);
        SetSoundVolume(volumeEfeitosAtual);
        
    }

    public void SetMusicVolume(float volume)
    {
        volumeMusicaAtual = volume;
        if (volume <= 0.0001f)
        {
        audioMixer.SetFloat("MusicVolume", -80f); // Mudo
        }
        else
        {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        }
    }

    public void SetSoundVolume(float volume)
    {
        volumeEfeitosAtual = volume;
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("SoundVolume", -80f); // Mudo
        }
        else
        {
            audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20f);
        }
    }

    public void SaveChanges()
    {
        if (fullScreen.isOn)
        {
            Screen.fullScreen = true;
        }

        float volumeMusica = musicSlider.value;
        float volumeEfeitos = soundSlider.value;
        string resolucao = configs.Resolucao;
        bool telaCheia = fullScreen.isOn;

        dB.AtualizarConfiguracoes(configs.Id, volumeMusica, volumeEfeitos, resolucao, telaCheia);
    }

}

