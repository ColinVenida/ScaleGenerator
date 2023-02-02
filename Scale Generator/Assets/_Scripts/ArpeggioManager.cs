using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArpeggioManager : MonoBehaviour
{
    public Fretboard fBoard;   
    public Dropdown scaleDrop;       

    private int lastID = -1; //int to identify the previous button pushed
    private Button lastBtn;

    public void ProcessArpeggio( int arpID, Button selfBtn )
    {        
        
        if ( lastID != arpID )
        {              
            ApplyArpeggio( arpID );            
            DarkenBtnColor( selfBtn );
            if ( lastBtn != null )
                ResetBtnColor( lastBtn );
        }
        else
        {            
            CancelArpeggio();
            ResetBtnColor( selfBtn );
        }
        lastBtn = selfBtn;
    }

    private void ApplyArpeggio( int id )
    {
        scaleDrop.value = id;
        lastID = id;        
        fBoard.textForm.EnableArpeggioColor();
        fBoard.SetArpeggio();
    }

    private void DarkenBtnColor( Button btn )
    {
        btn.image.color = new Color( 0.784f, 0.784f, 0.784f );
    }

    private void CancelArpeggio()
    {
        lastID = -1;       //reset the lastBtn so we can activate the button again
        
        fBoard.textForm.DisableArpeggioColor();
        fBoard.ResetArpeggio();
    }

    private void ResetBtnColor( Button btn )
    {
        btn.image.color = new Color( 1.0f, 1.0f, 1.0f );
    }
}
