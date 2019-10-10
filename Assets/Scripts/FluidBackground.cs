using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidBackground : MonoBehaviour
{

    [SerializeField]
    private Transform centerBG;
    [SerializeField]
    private Transform centerRightBarrier;
    [SerializeField]
    private Transform centerLeftBarrier;

    private float bgDiff; // = 16f;
    private float barrierDiffLeft;//= 0.04f;//-2.49f; // 2.3842e-07f;//0.04f;
    private float barrierDiffRight;

    private void Start()
    {
        bgDiff = (centerBG.GetChild(0).position - centerBG.position).y;
        barrierDiffLeft = (centerLeftBarrier.GetChild(0).position - centerLeftBarrier.position).y;
        barrierDiffRight = (centerRightBarrier.GetChild(0).position - centerRightBarrier.position).y;

    }


    // Update is called once per frame
    void Update()
    {
        //TODO Ta ut 16 från background uppers, y istället

        //background
        Reposition(centerBG, bgDiff);
        //leftSide
        Reposition(centerLeftBarrier, barrierDiffLeft);
        //rightside
        Reposition(centerRightBarrier, barrierDiffRight);  //TODO X ska ändras istället 2.49


    }

    private void Reposition(Transform image, float diff)
    {

        if (transform.position.y >= image.position.y + diff)
        {
            image.position = new Vector2(image.position.x, transform.position.y + diff);
        }
        else if (transform.position.y <= image.position.y - diff)
        {
            image.position = new Vector2(image.position.x, transform.position.y - diff);

        }
    }
}
