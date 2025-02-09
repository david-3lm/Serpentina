using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance{get; private set;}
    
    private AudioSource audioSource;
    
    /* 0 = MEOW; 1 = VEN; 2 = VICTORIA; 3 = DERROTA; 4 = MESA; 5 = SHINY*/
    [SerializeField]private List<AudioClip> audioClips;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el objeto en todas las escenas
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe una instancia
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int clipIndex)
    {
        audioSource.PlayOneShot(audioClips[clipIndex]);
    }
}
