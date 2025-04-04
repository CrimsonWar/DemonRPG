using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class encounterCache : MonoBehaviour
{
    public static encounterCache Cache { get; private set; }
    [SerializeField] private string activeEncounter;
    [SerializeField] private GameObject encounterUnit1;
    [SerializeField] private GameObject encounterUnit2;
    [SerializeField] private GameObject encounterUnit3;
    [SerializeField] private iUnit selectedTarget;
    [SerializeField] private BattleState result;

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

    public string getEncounter() {
        return activeEncounter;
    }
    public GameObject getUnit1() {
        return encounterUnit1;
    }
    public GameObject getUnit2() {
        return encounterUnit2;
    }
    public GameObject getUnit3() {
        return encounterUnit3;
    }

    public void setResult (BattleState state) {
        result = state;
    }
    
    public BattleState getResult () {
        return result;
    }

    public void setSelectedTarget (iUnit target) {
        selectedTarget = target;
    }

    public iUnit getSelectedTarget() {
        iUnit target = selectedTarget;
        selectedTarget = null;
        return target;
    }

    public void setEncounter (GameObject encounter, GameObject Unit1, GameObject Unit2 = null, GameObject Unit3 = null) {
        activeEncounter = encounter.name;
        encounterUnit1 = Unit1;
        encounterUnit2 = Unit2;
        encounterUnit3 = Unit3;
    }

    public void resetCache () {
        activeEncounter = null;
        encounterUnit1 = null;
        encounterUnit2 = null;
        encounterUnit3 = null;
        result = BattleState.Setup;
    }

}
