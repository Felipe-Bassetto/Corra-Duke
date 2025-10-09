using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundManager : MonoBehaviour
{
    // Definição de lista
    public List<AudioClip> listSoundEffects = new List<AudioClip>();

    // Definição de variaveis audio
    private AudioClip sound;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //sound = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundPlay(int index)
    {
        sound = listSoundEffects[index];
        audioSource.PlayOneShot(sound, 0.5f);
    }
}
