﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FretButton : MonoBehaviour
{
    public Fretboard fBoard;
    public bool onDisplay; //bool to see if the button is on the DisplayScale objects (as opposed to on the fretboard)

    private Text childText;

    void Start()
    {
        childText = GetComponentInChildren<Text>();
    }

    public void HighlightColor()
    {        
        int remove = 0;
        string str = childText.text;

        if ( childText.text != "" )
        {
            if (onDisplay)
            {
                //remove the "m" or "dim" from the string
                if (childText.text.Contains( "dim" ))
                {
                    remove = 3;
                    str = str.Remove( childText.text.Length - remove );
                }
                else if ( childText.text.Contains( "m" ) )
                {
                    remove = 1;
                    str = str.Remove( childText.text.Length - remove );
                }                
            }
            //fBoard.HighlightFrets( str );
            fBoard.ToggleHighlight( str );
        }
    }       
}
