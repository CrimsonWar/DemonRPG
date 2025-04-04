using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text MenuName;
    [SerializeField] private Button AttackButton;
    [SerializeField] private Button AbilityButton;
    [SerializeField] private Button PassButton;
    [SerializeField] private Button MoveButton;
    [SerializeField] private GameObject AttackPopup;
    [SerializeField] private GameObject AbilityPopup;
    [SerializeField] private GameObject disableImage;

    private List<GameObject> attackButtons;
    private List<GameObject> abilityButtons;

    private PlayerUnit unit;

    public void SetupMenu (PlayerUnit playerUnit) {
        unit = playerUnit;
        image.sprite = unit.menuIcon;
        MenuName.text = unit.unitName;
        this.setupAttacks();
        this.setupAbilities();
    }

    public void disableMenu () {
        disableImage.SetActive(true);
    }

    private void setupAttacks () {
        GameObject attackTemplate = AttackPopup.transform.GetChild(0).gameObject;
        GameObject a;
        attackButtons = new List<GameObject>{};
        AttackBase[] attacks = unit.getAttacks();
        foreach (AttackBase attack in attacks)
        {
            a = Instantiate(attackTemplate, AttackPopup.transform);
            a.GetComponent<ATemplateHandler>().setupTemplate(attack);
            attackButtons.Add(a);
        }
        attackTemplate.SetActive(false);
    }
    
    private void setupAbilities () {
        GameObject abilityTemplate = AbilityPopup.transform.GetChild(0).gameObject;
        GameObject a;
        abilityButtons = new List<GameObject>{};
        AttackBase[] abilities = unit.getAbilities();
        foreach (AttackBase ability in abilities)
        {
            a = Instantiate(abilityTemplate, AbilityPopup.transform);
            a.GetComponent<ATemplateHandler>().setupTemplate(ability);
            abilityButtons.Add(a);
        }
        abilityTemplate.SetActive(false);
    }

    private void setupListeners () {
        foreach (GameObject attackButton in attackButtons)
        {
            attackButton.GetComponent<ATemplateHandler>().setupListener();
        }
        foreach (GameObject abilityButton in abilityButtons)
        {
            abilityButton.GetComponent<ATemplateHandler>().setupListener();
        }
    }

    public void openAttacks () {
        AbilityPopup.SetActive(false);
        AttackPopup.SetActive(true);
        this.setupListeners();
    }
    public void openAbilities () {
        AttackPopup.SetActive(false);
        AbilityPopup.SetActive(true);
        this.setupListeners();
    }

    public void startTurn () {
        AttackButton.interactable = true;
        AbilityButton.interactable = true;
        PassButton.interactable = true;
        MoveButton.interactable = true;
    }
    public void endTurn () {
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        PassButton.interactable = false;
        MoveButton.interactable = false;
        AbilityPopup.SetActive(false);
        AttackPopup.SetActive(false);
    }
}
