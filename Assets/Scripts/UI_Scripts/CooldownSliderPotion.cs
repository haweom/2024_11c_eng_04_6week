using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSliderPotion : MonoBehaviour
{
    [SerializeField] private HealthPotion healthPotion;
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Color cooldownCharge;
    [SerializeField] private Color cooldownFinished;

    private void Start()
    {
        cooldownSlider.maxValue = healthPotion.Cooldown;
        cooldownSlider.value = 0;
    }

    private void Update()
    {
        if (healthPotion.CooldownTimer > 0)
        {
            cooldownSlider.value = healthPotion.Cooldown - healthPotion.CooldownTimer; // Fill slider based on time spent
            sliderFill.color = cooldownCharge;
        }
        else
        {
            cooldownSlider.value = healthPotion.Cooldown;
            sliderFill.color = cooldownFinished;
        }
    }
}
