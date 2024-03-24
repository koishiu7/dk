using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // varible
    [Header("==== keys seeting ====")]
    public string keyUp = "w";
    public string keyDwon = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

     public string keyA;
     public string keyB;
     public string keyC;
    public string keyD;

    [Header("==== output signals ====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    //press signal
    public bool run;
    //2 trigger once signal
    public bool jump;
    private bool lastJump;
    //3 double trigger

    [Header("==== others ====")]
    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDwon)?1.0f:0);
        targetDright = (Input.GetKey (keyRight)? 1.0f:0) - (Input.GetKey(keyLeft)?1.0f:0);

        if (inputEnabled == false) {
            targetDup = 0;
            targetDright = 0;
        }   

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SqareToCicle (new Vector2 (Dup, Dright));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        //bool tempJump = Input.GetKey(keyB);
        bool newJump = Input.GetKey(keyB);
        //jump = tempJump;
        // triger ����
        if ( newJump != lastJump && newJump == true )
        {
            jump = true;
        }
        else { 
        jump = false;
        }
        lastJump = newJump;

        
    }

    private Vector2 SqareToCicle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1-(input.y * input.y)/2.0f) ;
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

}
