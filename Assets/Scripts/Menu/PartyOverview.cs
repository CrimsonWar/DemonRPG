using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyOverview : MonoBehaviour
{
    [SerializeField] private MemberOverview PartySlot1;
    [SerializeField] private MemberOverview PartySlot2;
    [SerializeField] private MemberOverview PartySlot3;
    [SerializeField] private Sprite EmptySlot;
    [SerializeField] private Transform Loadpoint;

    public void updateParty () {
        GameObject Unit1 = Instantiate(PartyManager.inst.getSlot1Obj(), Loadpoint);
        GameObject Unit2 = null;
        GameObject Unit3 = null;
        if(PartyManager.inst.getSlot2Obj() != null) {
            Unit2 = Instantiate(PartyManager.inst.getSlot2Obj(), Loadpoint);
        }
        if(PartyManager.inst.getSlot3Obj() != null) {
            Unit3 = Instantiate(PartyManager.inst.getSlot3Obj(), Loadpoint);
        }

        updateSlot(1, Unit1, PartyManager.inst.getHealthSlot1());
        updateSlot(2, Unit2, PartyManager.inst.getHealthSlot2());
        updateSlot(3, Unit3, PartyManager.inst.getHealthSlot3());

        Destroy(Unit1);
        Destroy(Unit2);
        Destroy(Unit3);

    }

    private void updateSlot (int slot, GameObject unit, int currentHealth) {
        Sprite MenuIcon;
        string MenuName;
        string currentHP;
        string maxHP;
        if(unit != null) {
            MenuIcon = unit.GetComponent<UnitAbstract>().menuIcon;
            MenuName = unit.GetComponent<UnitAbstract>().unitName;
            currentHP = currentHealth.ToString();
            maxHP = unit.GetComponent<UnitAbstract>().maxHP.ToString();
        } else {
            MenuName = "-";
            currentHP = "-";
            maxHP = "-";
            MenuIcon =  EmptySlot;
        }

        switch (slot)
        {
            case 1:
                PartySlot1.updateMember(MenuIcon, MenuName, maxHP, currentHP);
            break;
            case 2:
                PartySlot2.updateMember(MenuIcon, MenuName, maxHP, currentHP);
            break;
            case 3:
                PartySlot3.updateMember(MenuIcon, MenuName, maxHP, currentHP);
            break;
        }
    }

}
