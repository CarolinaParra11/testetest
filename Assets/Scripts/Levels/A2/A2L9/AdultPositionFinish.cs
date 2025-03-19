using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultPositionFinish : MonoBehaviour
{
    [SerializeField]
    private Transform finishPosition;

    [SerializeField]
    private Transform pivot;

    public void PlacePosition()
    {
        //pivot.position = new Vector3(finishPosition.position.x, finishPosition.position.y, finishPosition.position.z);
    }
}
