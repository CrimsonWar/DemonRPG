using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE_Fill : BaseQTE
{
    public TMP_Text ButtonText;
    public GameObject Rating;
    public Slider slider;
    public int maxPresses;
    private bool buttonPressed;
    private Animator _animator;
    private const string trigger = "ButtonPressed";
    [SerializeField] private float valPerPress;
    [SerializeField] private float decayPercentage;
 
    private void Awake() {
        switch (button)
        {
            case QTEButton.E:
                ButtonText.SetText("[E]");
                break;
            case QTEButton.Q:
                ButtonText.SetText("[Q]");
                break;
            case QTEButton.Space:
                ButtonText.SetText("[Space]");
                break;
        }
        _animator = GetComponent<Animator>();
        float pressesPerSec = maxPresses / QTESeconds;
        valPerPress = (slider.maxValue / QTESeconds) / pressesPerSec;
    }
    private void Update() {
        slider.value -= decayPercentage;
        buttonPressed = buttonListener();
        if (buttonPressed)
        {
            slider.value += valPerPress;
            _animator.SetTrigger(trigger);
        }
        float pointPercentage = slider.value / slider.maxValue;

        if(pointPercentage < 0.2f) {
            Rating.GetComponent<TMP_Text>().SetText("WEAK");
            decayPercentage = 0f;
        } else if(pointPercentage < 0.4f) {
            Rating.GetComponent<TMP_Text>().SetText("GOOD");
            decayPercentage = 0.005f;
        } else if(pointPercentage < 0.6f) {
            Rating.GetComponent<TMP_Text>().SetText("MEATY");
            decayPercentage = 0.01f;
        } else if(pointPercentage < 0.7f) {
            Rating.GetComponent<TMP_Text>().SetText("GNARLY!");
            decayPercentage = 0.02f;
        } else if(pointPercentage < 0.8f) {
            Rating.GetComponent<TMP_Text>().SetText("KILLER!");
            decayPercentage = 0.03f;
        } else if(pointPercentage < 0.9f) {
            Rating.GetComponent<TMP_Text>().SetText("SADISTIC!!");
            decayPercentage = 0.05f;
        } else {
            Rating.GetComponent<TMP_Text>().SetText("HELLISH!!!");
            decayPercentage = 0.08f;
        }

        int pointsToAdd = (int)Mathf.Round(MaxPoints * pointPercentage);
        pointsGathered = pointsToAdd;
    }

    public bool buttonListener ()
    {
        bool retVal = false;
        switch (button)
        {
            case QTEButton.E:
                if (InputManager.EPressed)
                {
                    retVal = true;
                }
                break;
            case QTEButton.Q:
                if (InputManager.QPressed)
                {
                    retVal = true;
                }
                break;
            case QTEButton.Space:
                if (InputManager.SpacePressed)
                {
                    retVal = true;
                }
                break;
        }
        return retVal;
    }

    private void CalcRating (float pointPercentage) {
        if(pointPercentage < 0.2f) {
            Rating.GetComponent<TMP_Text>().SetText("WEAK");
            decayPercentage = 0f;
        } else if(pointPercentage < 0.4f) {
            Rating.GetComponent<TMP_Text>().SetText("GOOD");
            decayPercentage = 0.05f * valPerPress;
        } else if(pointPercentage < 0.6f) {
            Rating.GetComponent<TMP_Text>().SetText("MEATY");
            decayPercentage = 0.1f * valPerPress;
        } else if(pointPercentage < 0.7f) {
            Rating.GetComponent<TMP_Text>().SetText("GNARLY!");
            decayPercentage = 0.2f * valPerPress;
        } else if(pointPercentage < 0.8f) {
            Rating.GetComponent<TMP_Text>().SetText("KILLER!");
            decayPercentage = 0.3f * valPerPress;
        } else if(pointPercentage < 0.9f) {
            Rating.GetComponent<TMP_Text>().SetText("SADISTIC!!");
            decayPercentage = 0.4f * valPerPress;
        } else {
            Rating.GetComponent<TMP_Text>().SetText("HELLISH!!!");
            decayPercentage = 0.5f * valPerPress;
        }
    }

    private void CalcPoints (float pointPercentage) {
        
    }

}
