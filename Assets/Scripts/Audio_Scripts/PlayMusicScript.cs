using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicScript : MonoBehaviour
{
    private AudioSource _as;

    private void Awake()
    {
        _as = this.GetComponent<AudioSource>();
    }

    void Start()
    {
       _as.Play(); 
    }
    
    void Update()
    {
        
    }
}
