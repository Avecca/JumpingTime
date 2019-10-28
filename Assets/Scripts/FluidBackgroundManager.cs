using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidBackgroundManager : MonoBehaviour
{
    //Make the background and sides of the game board be infinite
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
        //The diffrences between the center diffrent panels and the upper/lower panels which make up the BG
        bgDiff = (centerBG.GetChild(0).position - centerBG.position).y;
        barrierDiffLeft = (centerLeftBarrier.GetChild(0).position - centerLeftBarrier.position).y;
        barrierDiffRight = (centerRightBarrier.GetChild(0).position - centerRightBarrier.position).y;

    }

    void Update()
    {
        //background
        Reposition(centerBG, bgDiff);
        //leftSide
        Reposition(centerLeftBarrier, barrierDiffLeft);
        //rightside
        Reposition(centerRightBarrier, barrierDiffRight);  
    }

    private void Reposition(Transform image, float diff)
    {
        //moving up or down
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
