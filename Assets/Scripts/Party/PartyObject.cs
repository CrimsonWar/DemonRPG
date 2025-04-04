
using UnityEngine;

[CreateAssetMenu(fileName = "PartyObject", menuName = "Demon RPG Demo/PartyObject", order = 0)]
public class PartyObject : ScriptableObject {
    
    public GameObject PartyOverworld;
    public GameObject PartySlot1;
    public int Slot1Health;
    public GameObject PartySlot2;
    public int Slot2Health;
    public GameObject PartySlot3;
    public int Slot3Health;
    public Vector2 OverworldPos;

}