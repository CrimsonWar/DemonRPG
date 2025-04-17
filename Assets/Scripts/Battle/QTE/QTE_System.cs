using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTE_System : MonoBehaviour
{
    public TMP_Text ButtonText;
    public GameObject Rating;
    private int QTESeconds = 1;

    public int startQTE (string ButtonToPress, int QTELengthInSec, int MaxPointsPerTest) {
        int pointsGathered = 0;
        QTESeconds = QTELengthInSec;
        return pointsGathered;
    }

}
