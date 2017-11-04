using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPosController : MonoBehaviour
{

    [SerializeField]
    GameObject checkObj;

    Transform childTransform;

    int pointMax = 100;

    float distMax = 5;


    public int point = 0;

    // Use this for initialization
    void Start()
    {
        childTransform = transform.FindChild("answerPos");
    }

    // Update is called once per frame
    void Update()
    {
        point = calcPoint();
    }

    int calcPoint()
    {
        float dist = (Vector3.Distance(checkObj.transform.position, childTransform.position));

        //距離が最大より遠いとき
        if (distMax - dist <= 0)
        {
            return 0;
        }

        if (dist == 0)
        {
            return pointMax;
        }

        return (int)(((distMax - dist) / distMax) * pointMax);


    }


}
