using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider health;

    public void SetMaxHealth (int healthVal) {
        health.maxValue = healthVal;
        health.value = healthVal;
    }

    public void SetHealth (int healthVal) {
        health.value = healthVal;
    }
}
