using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MemberOverview : MonoBehaviour
{
    [SerializeField] private Image MemberIcon;
    [SerializeField] private TMP_Text MemberName;
    [SerializeField] private TMP_Text MemberHealth;

    private const string HealthText = "HP: (current) / (max)";
    private const string currentPlaceholder = "(current)";
    private const string maxPlaceholder = "(max)";

    public void updateMember (Sprite Image, string Name, string maxHealth, string currentHealth) {
        MemberIcon.sprite = Image;
        MemberName.text = Name;
        string healthOutput = HealthText;
        healthOutput = healthOutput.Replace(currentPlaceholder, currentHealth);
        healthOutput = healthOutput.Replace(maxPlaceholder, maxHealth);
        MemberHealth.text = healthOutput;
    }

}
