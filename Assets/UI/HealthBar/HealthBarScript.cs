using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    
    [SerializeField] private float easeSpeed = 0.05f;

    private void Start()
    {
        healthSlider.value = healthSlider.maxValue;
    }

    private void Update()
    {
        if (easeHealthSlider.value != healthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, easeSpeed);
        }
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
    
}
