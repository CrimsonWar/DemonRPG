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
        valPerPress = 6f;
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
        int pointsToAdd = 0;
        if(pointPercentage < 0.2f) {
            Rating.GetComponent<TMP_Text>().SetText("WEAK");
            decayPercentage = 0f;
            pointsToAdd = 0;
        } else if(pointPercentage < 0.4f) {
            Rating.GetComponent<TMP_Text>().SetText("GOOD");
            decayPercentage = 0.01f;
            pointsToAdd = (int)Mathf.Round(MaxPoints * 0.2f);
        } else if(pointPercentage < 0.6f) {
            Rating.GetComponent<TMP_Text>().SetText("MEATY");
            decayPercentage = 0.02f;
            pointsToAdd = (int)Mathf.Round(MaxPoints * 0.4f);
        } else if(pointPercentage < 0.7f) {
            Rating.GetComponent<TMP_Text>().SetText("GNARLY!");
            decayPercentage = 0.04f;
            pointsToAdd = (int)Mathf.Round(MaxPoints * 0.6f);
        } else if(pointPercentage < 0.8f) {
            Rating.GetComponent<TMP_Text>().SetText("KILLER!");
            decayPercentage = 0.06f;
            pointsToAdd = (int)Mathf.Round(MaxPoints * 0.7f);
        } else if(pointPercentage < 0.9f) {
            Rating.GetComponent<TMP_Text>().SetText("SADISTIC!!");
            decayPercentage = 0.07f;
            pointsToAdd = (int)Mathf.Round(MaxPoints * 0.8f);
        } else {
            Rating.GetComponent<TMP_Text>().SetText("HELLISH!!!");
            decayPercentage = 0.08f;
            pointsToAdd = MaxPoints;
        }

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

}
