using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArpeggioManager : MonoBehaviour
{
    public Fretboard fBoard;   
    public Dropdown scaleDrop;    
   
    private int lastBtn = -1; //int to identify the previous button pushed

    public void ProcessArpeggio( int arpID, Button selfBtn )
    {        
        if ( lastBtn != arpID )
        {              
            ApplyArpeggio( arpID );
            DarkenBtnColor( selfBtn );
        }
        else
        {            
            CancelArpeggio();
            ResetBtnColor( selfBtn );
        }        
    }

    private void ApplyArpeggio( int id )
    {
        scaleDrop.value = id;
        lastBtn = id;
        fBoard.textForm.ShowArpeggio( true );
        fBoard.SetArpeggio();
    }

    private void DarkenBtnColor( Button btn )
    {
        btn.image.color = new Color( 0.784f, 0.784f, 0.784f );
    }

    private void CancelArpeggio()
    {
        lastBtn = -1;       //reset the lastBtn so we can activate the button again
        fBoard.textForm.ShowArpeggio( false );
        fBoard.ResetArpeggio();
    }

    private void ResetBtnColor( Button btn )
    {
        btn.image.color = new Color( 1.0f, 1.0f, 1.0f );
    }
}
