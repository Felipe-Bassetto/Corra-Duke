using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Definição de lista
    public List<AudioClip> listSoundEffects = new List<AudioClip>();

    // Definição de variaveis audio
    private AudioClip sound;
    public AudioSource audioSource;
    public AudioMixer audioMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SoundPlay(int index)
    {
        sound = listSoundEffects[index];
        audioMixer.GetFloat("SoundVolume", out float effectsVolume);
        Debug.Log(effectsVolume);
        audioSource.PlayOneShot(sound, 0.5f); 
    }
}
