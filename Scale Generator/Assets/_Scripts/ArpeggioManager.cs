using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArpeggioManager : MonoBehaviour
{
    public Fretboard fBoard;   
    public Dropdown scaleDrop;
    public Button[] btnArray;

    private bool useArp = false;
    private int lastBtn = -1; //int to identify the previous button pushed

    public void FilterScale( int scale )
    {
        ChangeBtnColor( scale );
        //if the same button has been pushed twice, then turn off the filter
        if (lastBtn == scale)
        {            
            lastBtn = -1;       //reset the lastBtn so we can activate the button again
            fBoard.textForm.ShowArpeggio( false );
            fBoard.ResetArpeggio();
        }
        else
        {            
            scaleDrop.value = scale;            
            lastBtn = scale;
            fBoard.textForm.ShowArpeggio( true );
            fBoard.SetArpeggio();
        }        
    }


    private void ChangeBtnColor( int scale )
    {
        //reset all the button's colors        
        for (int i = 0; i < btnArray.Length; i++)
        {
            btnArray[i].image.color = new Color( 1.0f, 1.0f, 1.0f );
        }

        //if scale is different than lastBtn, then change the color of the scale's button
        if ( scale != lastBtn )
        {
            switch ( scale ) 
            {
                case 0:     //major7
                    btnArray[0].image.color = new Color( 0.784f, 0.784f, 0.784f );
                    break;
                case 1:     //minor7
                    btnArray[1].image.color = new Color( 0.784f, 0.784f, 0.784f );
                    break;
                case 5:     //dom7
                    btnArray[2].image.color = new Color( 0.784f, 0.784f, 0.784f );
                    break;
            }
        }
    }

}
