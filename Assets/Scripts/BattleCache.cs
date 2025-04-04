using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCache : MonoBehaviour
{
    public static BattleCache Cache { get; private set; }
    [SerializeField] private List<string> defeatedEnemies;
    
    private void Awake() {

        if (Cache != null && Cache != this) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            Cache = this; 
        }

        DontDestroyOnLoad(this.gameObject);

    }

    public void addDefeated (string enemy) {
        defeatedEnemies.Add(enemy);
    }
    public List<string> getDefeated () {
        return defeatedEnemies;
    }

}    
