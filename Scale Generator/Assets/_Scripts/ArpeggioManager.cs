using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArpeggioManager : MonoBehaviour
{
    public Fretboard fBoard;   
    public Dropdown scaleDrop;

    public List<Button> ArpButtonList;

    private int lastID = -1; //int to identify the previous button pushed
    private Button lastBtn;

    private Color WHITE = new Color( 1.0f, 1.0f, 1.0f );
    private Color DARK_GRAY = new Color( 0.784f, 0.784f, 0.784f );

    public void ProcessArpeggio( int arpID, Button selfBtn )
    {
        ResetAllBtnColors();
        if ( lastID != arpID )
        {              
            ApplyArpeggio( arpID );            
            DarkenBtnColor( selfBtn );            
        }
        else
        {            
            CancelArpeggio();            
        }
        lastBtn = selfBtn;
    }

    private void ApplyArpeggio( int id )
    {
        scaleDrop.value = id;
        lastID = id;                
        fBoard.SetArpeggio();
        fBoard.textForm.UpdateArpeggioTextColor();
    }

    private void DarkenBtnColor( Button btn )
    {
        btn.image.color = DARK_GRAY;
    }

    private void CancelArpeggio()
    {
        lastID = -1;       //reset the lastBtn so we can activate the button again        
        
        fBoard.ResetArpeggio();
        fBoard.textForm.UpdateArpeggioTextColor();
    }

    private void ResetBtnColor( Button btn )
    {
        btn.image.color = WHITE;
    }

    private void ResetAllBtnColors()
    {
        foreach ( Button btn in ArpButtonList )
        {
            btn.image.color = WHITE;
        }
    }
}
