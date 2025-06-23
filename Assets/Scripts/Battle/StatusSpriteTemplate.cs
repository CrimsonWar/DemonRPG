using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusSpriteTemplate : MonoBehaviour
{
    
    [SerializeField] private Image StatusSprite;
    [SerializeField] private TMP_Text StatusRank;
    public GameObject status;

    public void setupStatus(GameObject statusBase) {
        status = Instantiate(statusBase, this.transform);
        StatusSprite.sprite = status.GetComponent<StatusBase>().sprite;
        if(status.GetComponent<StatusBase>().rank == 0) {
            StatusRank.text = "";
        } else {
           StatusRank.text = status.GetComponent<StatusBase>().rank.ToString(); 
        }
    }

    public void setSprite (Sprite newSprite) {
        StatusSprite.sprite = newSprite;
    }

    public void setRank (string newRank) {
        StatusRank.text = newRank;
    }

}
