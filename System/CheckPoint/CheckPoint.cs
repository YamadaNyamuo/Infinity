using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 checkPointPos;

    public CheckPointData checkPointData;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            checkPointData.startPosition = checkPointPos;
        }
    }
}
