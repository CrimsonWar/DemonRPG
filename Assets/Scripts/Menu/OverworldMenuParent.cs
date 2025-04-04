using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMenuParent : MonoBehaviour
{
    public static OverworldMenuParent MenuBase{ get; private set; }
    [SerializeField] private PartyOverview partyOverview;
    [SerializeField] private OverworldMenuHandler menuHandler;

    void Awake()
    {
        if (MenuBase != null && MenuBase != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            MenuBase = this; 
        } 
    }

    void OnEnable()
    {
        returnToMain();
    }

    private void returnToMain () {
        partyOverview.updateParty();
    }

}
