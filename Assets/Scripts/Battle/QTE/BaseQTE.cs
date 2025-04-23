using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QTEButton
{
    E,
    Q,
    Space
}
public abstract class BaseQTE : MonoBehaviour
{
    public QTEButton button;
    public int QTESeconds = 1;
    public int pointsGathered = 0;
    public int MaxPoints = 0;
}
