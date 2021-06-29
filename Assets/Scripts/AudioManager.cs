using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioSource source;
    public AudioClip[] clips;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(Sounds sound)
    {
        source.clip = clips[(int)sound];
        source.Play();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public enum Sounds
    {
        False,
        Lose,
        MoneyWon,
        PassengerWalksIn,
        True,
        Win
    }
}
