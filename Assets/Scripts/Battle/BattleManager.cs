using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Setup,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager inst { get; private set; }
    public sceneLoader sceneLoader;
    public Transform PlayerPartySlot1;
    public Transform PlayerPartySlot2;
    public Transform PlayerPartySlot3;
    public Transform EnemyPartySlot1;
    public Transform EnemyPartySlot2;
    public Transform EnemyPartySlot3;
    public BattleState state;
    public PlayerMenu playerMenu1;
    public PlayerMenu playerMenu2;
    public PlayerMenu playerMenu3;
    public TurnOrder turnOrder;
    public GameObject focusPrefab;
    public Transform UI;
    private FocusHandler UnitFocus;

    public List<GameObject> ObjectList;
    public PlayerUnit PlayerUnit1;
    public PlayerUnit PlayerUnit2;
    public PlayerUnit PlayerUnit3;
    public EnemyUnit EnemyUnit1;
    public EnemyUnit EnemyUnit2;
    public EnemyUnit EnemyUnit3;
    private int currentTurn;
    private bool nextTurn = false;

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
        state = BattleState.Setup;
        ObjectList = new List<GameObject>{};
        setupBattle();
    }

    private void Update() {
        if(nextTurn){
            nextTurn = false;
            checkWonOrLost();
            switch (state) {
            case BattleState.Won:
                saveParty();
                encounterCache.Cache.setResult(state);
                sceneLoader.loadOverworld();
                break;
            case BattleState.Lost:
                prepareRespawn();
                encounterCache.Cache.setResult(state);
                sceneLoader.loadOverworld();
                break;
            default:
                bool unitDead = true;
                while (unitDead)
                {
                    currentTurn = turnOrder.getNextTurn();
                    unitDead = this.checkDead(currentTurn);
                }
                this.getNextState();
                break;
            }
        }
    }

    private void setupBattle () 
    {
        //party setup
        GameObject PlayerGO1 = Instantiate(PartyManager.inst.getSlot1Obj(), PlayerPartySlot1);
        PlayerUnit1 = PlayerGO1.GetComponent<PlayerUnit>();
        ObjectList.Add(PlayerGO1);
        PlayerUnit1.setPartySlot(1);

        if (PartyManager.inst.getSlot2Obj() != null)
        {
            GameObject PlayerGO2 = Instantiate(PartyManager.inst.getSlot2Obj(), PlayerPartySlot2);
            PlayerUnit2 = PlayerGO2.GetComponent<PlayerUnit>();
            ObjectList.Add(PlayerGO2);
            PlayerUnit2.setPartySlot(2);
        }

        if (PartyManager.inst.getSlot3Obj() != null)
        {
            GameObject PlayerGO3 = Instantiate(PartyManager.inst.getSlot3Obj(), PlayerPartySlot3);
            PlayerUnit3 = PlayerGO3.GetComponent<PlayerUnit>();
            ObjectList.Add(PlayerGO3);
            PlayerUnit3.setPartySlot(3);
        }
        //enemy setup
        GameObject EnemyGO1 = Instantiate(encounterCache.Cache.getUnit1(), EnemyPartySlot1);
        EnemyUnit1 = EnemyGO1.GetComponent<EnemyUnit>();
        ObjectList.Add(EnemyGO1);
        EnemyUnit1.setPartySlot(4);

        if (encounterCache.Cache.getUnit2() != null)
        {
            GameObject EnemyGO2 = Instantiate(encounterCache.Cache.getUnit2(), EnemyPartySlot2);
            EnemyUnit2 = EnemyGO2.GetComponent<EnemyUnit>();
            ObjectList.Add(EnemyGO2);
            EnemyUnit2.setPartySlot(5); 
        }

        if (encounterCache.Cache.getUnit3() != null)
        {
            GameObject EnemyGO3 = Instantiate(encounterCache.Cache.getUnit3(), EnemyPartySlot3);
            EnemyUnit3 = EnemyGO3.GetComponent<EnemyUnit>();
            ObjectList.Add(EnemyGO3);
            EnemyUnit3.setPartySlot(6);
        }
        this.setupPlayerMenus();
        this.setupTurnOrder();
        currentTurn = turnOrder.getStartTurn();
        this.getNextState();
    }

    private void setupPlayerMenus () {
        playerMenu1.SetupMenu(PlayerUnit1);
        if(PlayerUnit2 != null) {
            playerMenu2.SetupMenu(PlayerUnit2);
        } else {
            playerMenu2.disableMenu();
        }
        if(PlayerUnit3 != null) {
            playerMenu3.SetupMenu(PlayerUnit3);
        } else {
            playerMenu3.disableMenu();
        }
    }

    private void setupTurnOrder () {
        ObjectList.Sort((x, y) => y.GetComponent<UnitAbstract>().speed.CompareTo(x.GetComponent<UnitAbstract>().speed));;
        turnOrder.setupTurnOrder(ObjectList);
        GameObject focusGO = Instantiate(focusPrefab, ObjectList[0].transform.position, Quaternion.identity);
        UnitFocus = focusGO.GetComponent<FocusHandler>();
    }

    private void getNextState () {
        this.resetPlayerMenus();
        if(state != BattleState.Won && state != BattleState.Lost) {
            int slot = ObjectList[currentTurn].GetComponent<UnitAbstract>().getSlot();
            switch (slot) {
                case 1:
                    state = BattleState.PlayerTurn;
                    PlayerUnit1.startTurn();
                    playerMenu1.startTurn();
                    UnitFocus.moveTo(PlayerPartySlot1.position);
                    break;
                case 2:
                    state = BattleState.PlayerTurn;
                    PlayerUnit2.startTurn();
                    playerMenu2.startTurn();
                    UnitFocus.moveTo(PlayerPartySlot2.position);
                    break;
                case 3:
                    state = BattleState.PlayerTurn;
                    PlayerUnit3.startTurn();
                    playerMenu3.startTurn();
                    UnitFocus.moveTo(PlayerPartySlot3.position);
                    break;
                case 4:
                    state = BattleState.EnemyTurn;
                    EnemyUnit1.startTurn();
                    UnitFocus.moveTo(EnemyPartySlot1.position);
                    break;
                case 5:
                    state = BattleState.EnemyTurn;
                    EnemyUnit2.startTurn();
                    UnitFocus.moveTo(EnemyPartySlot2.position);
                    break;
                case 6:
                    state = BattleState.EnemyTurn;
                    EnemyUnit3.startTurn();
                    UnitFocus.moveTo(EnemyPartySlot3.position);
                    break;
            }
        }
    }

    private void resetPlayerMenus () {
        playerMenu1.endTurn();
        playerMenu2.endTurn();
        playerMenu3.endTurn();
    }

    private bool checkDead(int nextTurn) {
        if (ObjectList[nextTurn].GetComponent<UnitAbstract>().isDead()) {
            return true;
        } else {
            return false;
        }
    }

    private void saveParty() {
        saveHP(PlayerUnit1);
        saveHP(PlayerUnit2);
        saveHP(PlayerUnit3);
    }

    private void saveHP (PlayerUnit unit) {
        int restoreHP = 10;

        if(unit != null) {
            if(unit.isDead()) {
                switch (unit.getSlot())
                {
                    case 1:
                        PartyManager.inst.setHealthSlot1(restoreHP);
                    break;
                    case 2:
                        PartyManager.inst.setHealthSlot2(restoreHP);
                    break;
                    case 3:
                        PartyManager.inst.setHealthSlot3(restoreHP);
                    break;
                }
            } else {
                switch (unit.getSlot())
                {
                    case 1:
                        PartyManager.inst.setHealthSlot1(unit.currentHP);
                    break;
                    case 2:
                        PartyManager.inst.setHealthSlot2(unit.currentHP);
                    break;
                    case 3:
                        PartyManager.inst.setHealthSlot3(unit.currentHP);
                    break;
                }
            }
        }
    }

    private void prepareRespawn () {
        PartyManager.inst.setHealthSlot1(PlayerUnit1.maxHP);
        if(PlayerUnit2 != null) {
            PartyManager.inst.setHealthSlot2(PlayerUnit2.maxHP);
        }
        if(PlayerUnit3 != null) {
            PartyManager.inst.setHealthSlot3(PlayerUnit3.maxHP);
        }
    }

    private void checkWonOrLost () {
        bool enemy1 = true;
        bool enemy2 = true;
        bool enemy3 = true;
        bool player1 = true;
        bool player2 = true;
        bool player3 = true;
        
        if (EnemyUnit1 != null)
        {
           enemy1 = EnemyUnit1.isDead();
        } 
        if (EnemyUnit2 != null)
        {
           enemy2 = EnemyUnit2.isDead();
        }
        if (EnemyUnit3 != null)
        {
           enemy3 = EnemyUnit3.isDead();
        }
        

        if (PlayerUnit1 != null)
        {
           player1 = PlayerUnit1.isDead();
        }
        if (PlayerUnit2 != null)
        {
           player2 = PlayerUnit2.isDead();
        }
        if (PlayerUnit3 != null)
        {
           player3 = PlayerUnit3.isDead();
        }

        //win
        if(enemy1 && enemy2 && enemy3) {
            state = BattleState.Won;
        }

        //lose
        if(player1 && player2 && player3) {
            state = BattleState.Lost;
        }
    }

    public void finishTurn () {
        ObjectList[currentTurn].GetComponent<UnitAbstract>().OnTurnEnd();
        nextTurn = true;
    }

}
