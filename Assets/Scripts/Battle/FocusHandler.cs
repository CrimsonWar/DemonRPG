using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHandler : MonoBehaviour
{
    public void moveTo (Vector3 position) {
        transform.position = position;
    }

}
