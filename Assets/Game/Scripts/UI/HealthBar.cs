using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;

    // Method to set full health (both max and current value)
    public void GiveFullHealth(float health)
    {
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
    }

    // Method to update current health
    public void SetHealth(float health)
    {
        healthBarSlider.value = health;
    }
}
