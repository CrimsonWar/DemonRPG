using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankFocus : MonoBehaviour
{
   [SerializeField] private GameObject TurnFocus;

   public void setFocus () {
        TurnFocus.SetActive(true);
   }
   public void unsetFocus () {
        TurnFocus.SetActive(false);
   }
}
