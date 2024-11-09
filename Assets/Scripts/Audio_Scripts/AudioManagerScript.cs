using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    
    [Header("--- Audio Source ---")]
    public  AudioSource srcMusic;
    public  AudioSource srcSfx;
    public AudioSource srcRun;

    [Header("--- Audio Clip ---")] 
    public AudioClip jump;
    public AudioClip land;
    public AudioClip swordAttack1;
    public AudioClip swordAttack2;
    public AudioClip swordAttack3;
    public AudioClip potion;
    public AudioClip throwBomb;
    public AudioClip explodeBomb;
    public AudioClip hookHit;
    public AudioClip hookShoot;
    public AudioClip swordThrow;
    public AudioClip swordThrowHit;
    public AudioClip swordPickUp;
    public AudioClip coinPickUp;

    [Header("--- Soundtrack ---")] 
    public AudioClip mainMenuTheme;
    public void Start()
    {
    }

    public void Update()
    {
        
    }

    public void PlaySfx(AudioClip clip)
    {
        srcSfx.PlayOneShot(clip);
    }
    
}
