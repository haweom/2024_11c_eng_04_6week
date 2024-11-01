using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSliderBomb : MonoBehaviour
{

    [SerializeField] private BombThrow bombThrow;
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Color cooldownCharge;
    [SerializeField] private Color cooldownFinished;

    private void Start()
    {
        cooldownSlider.maxValue = bombThrow.CooldownTime;
        cooldownSlider.value = 0;
    }

    private void Update()
    {
        if (Time.time < bombThrow.NextThrowTime)
        {
            float remainingTime = bombThrow.NextThrowTime - Time.time;
            cooldownSlider.value = bombThrow.CooldownTime - remainingTime;
            sliderFill.color = cooldownCharge;
        }
        else
        {
            cooldownSlider.value = bombThrow.CooldownTime;
            sliderFill.color = cooldownFinished;
        }
    }
}
