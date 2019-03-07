using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FretButton : MonoBehaviour
{
    public Fretboard fBoard;

    private Text childText;

    public void HighlightColor()
    {
        //Debug.Log( "Fret has been pressed.  HighlightColor() invoked" );
        //Debug.Log( "childText = " + childText.text );        
        if ( childText.text != "" )
        {
            fBoard.HighlightFrets( childText.text );
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        childText = GetComponentInChildren<Text>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
