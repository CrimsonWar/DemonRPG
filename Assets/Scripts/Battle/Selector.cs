using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public static Selector inst { get; private set; } 
    [SerializeField] private Button SelectPlayer1;
    [SerializeField] private Button SelectPlayer2;
    [SerializeField] private Button SelectPlayer3;
    [SerializeField] private Button SelectEnemy1;
    [SerializeField] private Button SelectEnemy2;
    [SerializeField] private Button SelectEnemy3;

    void Start()
    {
        if (inst != null && inst != this) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            inst = this; 
        } 
    }

    public void activateSelector (List<iUnit> targetableUnits) {
        foreach(iUnit Unit in targetableUnits) {
            if(Unit != null) {
                switch (Unit.getSlot())
                {
                    case 1:
                        SelectPlayer1.interactable = true;
                        break;
                    case 2:
                        SelectPlayer2.interactable = true;
                        break;
                    case 3:
                        SelectPlayer3.interactable = true;
                        break;
                    case 4:
                        SelectEnemy1.interactable = true;
                        break;
                    case 5:
                        SelectEnemy2.interactable = true;
                        break;
                    case 6:
                        SelectEnemy3.interactable = true;
                        break;
                }
            }
        }
    }

    public void resetSelector () {
        SelectPlayer1.interactable = false;
        SelectPlayer2.interactable = false;
        SelectPlayer3.interactable = false;
        SelectEnemy1.interactable = false;
        SelectEnemy2.interactable = false;
        SelectEnemy3.interactable = false;
    }

    public void selectedPlayer1 () {
        encounterCache.Cache.setSelectedTarget(BattleManager.inst.PlayerUnit1);
    }

    public void selectedPlayer2 () {
        encounterCache.Cache.setSelectedTarget(BattleManager.inst.PlayerUnit2);
    }

    public void selectedPlayer3 () {
        encounterCache.Cache.setSelectedTarget(BattleManager.inst.PlayerUnit3);
    }

    public void selectedEnemy1 () {
        encounterCache.Cache.setSelectedTarget(BattleManager.inst.EnemyUnit1);
    }

    public void selectedEnemy2 () {
        encounterCache.Cache.setSelectedTarget(BattleManager.inst.EnemyUnit2);
    }

    public void selectedEnemy3 () {
        encounterCache.Cache.setSelectedTarget(BattleManager.inst.EnemyUnit3);
    }

}
