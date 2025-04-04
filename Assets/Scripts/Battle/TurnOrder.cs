using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrder : MonoBehaviour
{
    [SerializeField] private List<GameObject> UnitOrder;
    [SerializeField] private List<GameObject> initiativeObjects;
    private int CurrentTurn;

    public void setupTurnOrder (List<GameObject> list) {
        GameObject RankTemplate = transform.GetChild(0).gameObject;
        GameObject Rank;
        UnitOrder = list;
        CurrentTurn = 0;
        RankTemplate.SetActive(true);
        foreach (GameObject Unit in UnitOrder)
        {
            Rank = Instantiate(RankTemplate, transform);
            Rank.GetComponent<Image>().sprite = Unit.GetComponent<UnitAbstract>().menuIcon;
            initiativeObjects.Add(Rank);
        }
        RankTemplate.SetActive(false);
    }

    public int getStartTurn ()
    {
        initiativeObjects[CurrentTurn].GetComponent<RankFocus>().setFocus();
        return CurrentTurn;

    }

    public int getNextTurn () {
        CurrentTurn++;
        if (CurrentTurn > initiativeObjects.Count - 1)
        {
            initiativeObjects[initiativeObjects.Count - 1].GetComponent<RankFocus>().unsetFocus();
            CurrentTurn = 0;
        }
        if(CurrentTurn > 0) {
            initiativeObjects[CurrentTurn - 1].GetComponent<RankFocus>().unsetFocus();
        }
        int nextTurn = CurrentTurn;
        initiativeObjects[CurrentTurn].GetComponent<RankFocus>().setFocus();
        return nextTurn;
    }

}
