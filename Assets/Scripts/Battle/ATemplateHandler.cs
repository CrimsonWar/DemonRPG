using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ATemplateHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text AName;
    [SerializeField] private TMP_Text ADescription;
    [SerializeField] private GameObject aBehaviour;
    [SerializeField] private Button button;

    public void setupTemplate (AttackBase behaviour) {
        aBehaviour = behaviour.gameObject;
        AName.text = aBehaviour.GetComponent<AttackBase>().attackName;
        ADescription.text = aBehaviour.GetComponent<AttackBase>().description;
    }

    public void setupListener() {
        button.onClick.AddListener(delegate { doOnClick(); });
    }

    void doOnClick() {
        aBehaviour.GetComponent<iAttack>().attackSelected();
    }
}
