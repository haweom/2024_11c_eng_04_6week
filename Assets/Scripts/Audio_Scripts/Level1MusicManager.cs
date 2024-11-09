using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1MusicManager : MonoBehaviour
{
    private AudioManagerScript _ams;

    private void Awake()
    {
        _ams = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        _ams.srcMusic.loop = true;
        _ams.srcMusic.volume = 0.4f;
    }

    private void Start()
    {
        _ams.srcMusic.clip = _ams.leve1Theme;
        _ams.srcMusic.Play();
    }
}
