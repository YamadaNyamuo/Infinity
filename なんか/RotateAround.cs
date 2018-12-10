using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System;

public class RotateAround : MonoBehaviour
{

    public enum RotateAxis
    {
        XAxis,
        YAxis,
        ZAxis
    }

    //　回転軸をどこにするか
    public RotateAxis rotateAxis;
    //　回転スピード
    public float rotateSpeed;
    //　基準点
    public Transform basePosition;

    //
    public bool rotateFlag;

    //GameObject drillRange;

    //DrillIsVisible isVisible;

    void Start()
    {
        //drillRange = transform.Find("DrillRange").gameObject;
        //isVisible = drillRange.GetComponent<DrillIsVisible>();
        rotateFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = Vector3.zero;

        if (rotateAxis == RotateAxis.XAxis)
        {
            axis = Vector3.right;
        }
        else if (rotateAxis == RotateAxis.YAxis)
        {
            axis = Vector3.up;
        }
        else if (rotateAxis == RotateAxis.ZAxis)
        {
            axis = Vector3.forward;
        }

        //if (isVisible.visibleFlag == true)
        //{
        if (rotateFlag == true)
        {
            //　回転処理
            float rotation = Time.deltaTime * rotateSpeed;

            transform.RotateAround(basePosition.position, axis, rotation);
        }
        //}
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "MainCamera")
        {
            rotateFlag = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            rotateFlag = false;
        }
    }

    //void OnBecameInvisible()
    //{

    //}

    //void OnBecameVisible()
    //{

    //}
}
