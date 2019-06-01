using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FretButton : MonoBehaviour
{
    public Fretboard fBoard;
    public bool onDisplay; //bool to see if the button is on the DisplayScale objects (as opposed to on the fretboard)

    private Text childText;

    public void HighlightColor()
    {
        //Debug.Log( "Fret has been pressed.  HighlightColor() invoked" );
        //Debug.Log( "childText = " + childText.text );
        int remove = 0;
        string str = childText.text;

        if ( childText.text != "" )
        {
            if (onDisplay)
            {
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

            
            //Debug.Log( "str = " + str );

            //fBoard.HighlightFrets( childText.text );
            fBoard.HighlightFrets( str );
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
