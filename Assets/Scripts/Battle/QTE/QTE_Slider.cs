using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE_Slider : BaseQTE
{
    public TMP_Text ButtonText;
    public GameObject Rating;
    public Slider slider;
    private bool buttonPressed;
    private Animator _animator;
    private const string trigger = "ButtonPressed";

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
    }
    private void Update() {
        buttonPressed = buttonListener();
        if (buttonPressed)
        {
            Rating.SetActive(true);
            float pointPercentage = slider.value / slider.maxValue;
            if(pointPercentage < 0.2f) {
                Rating.GetComponent<TMP_Text>().SetText("SHIT!");
            } else if(pointPercentage < 0.5f) {
                Rating.GetComponent<TMP_Text>().SetText("GOOD!");
            } else if(pointPercentage < 0.9f) {
                Rating.GetComponent<TMP_Text>().SetText("NICE!");
            } else {
                Rating.GetComponent<TMP_Text>().SetText("EXCELLENT!");
            }
            int pointsToAdd = (int)Mathf.Round(MaxPoints * pointPercentage);
            pointsGathered = pointsGathered + pointsToAdd;
            _animator.SetTrigger(trigger);
        }
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
