using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay;

public enum overworldState
{
    Play,
    Pause
}

public class overworldManager : MonoBehaviour
{
    public static overworldManager inst { get; private set; }

    public overworldState state;

    private PartyManager partyManager;
    private encounterCache Cache;
    private BattleCache defeatedCache;
    private GameObject prefab;
    public GameObject PartyObj;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject Menu;

    void Awake()
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

    private void Start()
    {
        state = overworldState.Play;
        partyManager = PartyManager.inst;
        Cache = encounterCache.Cache;
        defeatedCache = BattleCache.Cache;
        prefab = partyManager.getOverworldParty();
        Vector3 SpawnPosition = respawnPoint.position;
        checkEncounterReturn();
        SpawnPosition.x = partyManager.getOverworldPos().x;
        SpawnPosition.y = partyManager.getOverworldPos().y;
        destroyDefeated();

        PartyObj = Instantiate(prefab, SpawnPosition, Quaternion.identity);
    }

    void Update()
    {
        if(InputManager.ExitPressed) {
            switch (state)
            {
                case overworldState.Play:
                    Time.timeScale = 0;
                    state = overworldState.Pause;
                    Menu.SetActive(true);
                break;
                case overworldState.Pause:
                    Menu.SetActive(false);
                    Time.timeScale = 1;
                    state = overworldState.Play;
                break;
            }
        }
    }

    private void checkEncounterReturn () {
        switch (Cache.getResult())
        {
            case BattleState.Won:
                defeatedCache.addDefeated(Cache.getEncounter());
                Cache.resetCache();
                break;
            case BattleState.Lost:
                PartyManager.inst.setOverworldPos(respawnPoint.position);
                Cache.resetCache();
                break;
            default:
                PartyManager.inst.setOverworldPos(respawnPoint.position);
                break;
        }
    }

    private void destroyDefeated () {
        List<string> defeated = defeatedCache.getDefeated();
        foreach (string enemyName in defeated)
        {
            if(GameObject.Find(enemyName) != null) {
                Destroy(GameObject.Find(enemyName));
            }
        }
    }
    
}
