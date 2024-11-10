using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript Instance { get; private set; }
    
    
    [Header("--- Audio Source ---")]
    public  AudioSource srcMusic;
    public  AudioSource srcSfx;
    public AudioSource srcRun;
    public AudioSource dynamicSfx;

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
    public AudioClip totem1Shoot;
    public AudioClip dmgSound1;
    public AudioClip dmgSound2;
    public AudioClip dmgSound3;
    public AudioClip dmgSound4;

    [Header("--- Soundtrack ---")] 
    public AudioClip mainMenuTheme;
    public AudioClip leve1Theme;
    public AudioClip level2Theme;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //made for playing sounds with dynamic volumes
    // for example totem's shooting volume is based on distance
    public void PlayDynamicSfx(AudioClip clip, float volume = 1f)
    {
        dynamicSfx.PlayOneShot(clip,volume);
    }
    
    //normal sound effect
    public void PlaySfx(AudioClip clip)
    {
        srcSfx.PlayOneShot(clip);
    }
}
