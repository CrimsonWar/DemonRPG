using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public PartyObject partyData;
    public static PartyManager inst { get; private set; }

    private void Awake() {
        
        if (inst != null && inst != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            inst = this; 
        } 

    }

    public GameObject getOverworldParty() {
        return partyData.PartyOverworld;
    }

    public void setOverworldParty(GameObject prefab) {
        partyData.PartyOverworld = prefab;
    }

    public GameObject getSlot1Obj () {
        return partyData.PartySlot1;
    }
    public void setSlot1Obj (GameObject prefab) {
        partyData.PartySlot1 = prefab;
    }

    public int getHealthSlot1 () {
        return partyData.Slot1Health;
    }

    public void setHealthSlot1 (int hp) {
        partyData.Slot1Health = hp;
    }

    public GameObject getSlot2Obj () {
        return partyData.PartySlot2;
    }
    public void setSlot2Obj (GameObject prefab) {
        partyData.PartySlot2 = prefab;
    }

    public int getHealthSlot2 () {
        return partyData.Slot2Health;
    }

    public void setHealthSlot2 (int hp) {
        partyData.Slot2Health = hp;
    }

    public GameObject getSlot3Obj () {
        return partyData.PartySlot3;
    }
    public void setSlot3Obj (GameObject prefab) {
        partyData.PartySlot3 = prefab;
    }

    public int getHealthSlot3 () {
        return partyData.Slot3Health;
    }

    public void setHealthSlot3 (int hp) {
        partyData.Slot3Health = hp;
    }

    public Vector2 getOverworldPos () {
        return partyData.OverworldPos;
    }
    public void setOverworldPos (Vector3 pos) {
        partyData.OverworldPos.x = pos.x;
        partyData.OverworldPos.y = pos.y;
    }

}
