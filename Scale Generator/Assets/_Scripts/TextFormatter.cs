using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;


//methods copied from ScaleScene.cs
public class TextFormatter : MonoBehaviour
{
    public Fretboard fBoard;   
    public DisplayScale[] displayScales;
    public Toggle useArpeggio;    
    public Dropdown scaleDrop;
    public Dropdown rootDrop;

    private List<string> chordQualityList = new List<string> { "", "m", "m", "", "", "m", "dim" };    
    private Color lightGray = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
    private Color darkGray = new Color( 0.196f, 0.196f, 0.196f );

    public void UpdateDisplayScalesOnFretboard()
    {
        MusicScale scale = fBoard.CurrentMusicScale;
        int dropValue = fBoard.scaleDrop.value;

        for ( int i = 0; i < displayScales.Length; i++ )
        {
            displayScales[i].UpdateNotes( scale, dropValue );
            displayScales[i].UpdateScaleDegreeSymbols();
        }
    }      
    
    public void EnableArpeggioColor()
    {
        UpdateDisplayArpeggioColor( lightGray );
    }

    public void DisableArpeggioColor()
    {
        UpdateDisplayArpeggioColor( darkGray );
    }

    //apply color to the 2, 4, and 6 scale degrees   
    private void UpdateDisplayArpeggioColor( Color c )
    {
        for ( int i = 0; i < displayScales.Length; i++ )
        {
            displayScales[i].noteTexts[1].color = c;
            displayScales[i].noteTexts[3].color = c;
            displayScales[i].noteTexts[5].color = c;

            displayScales[i].scaleDegreeTexts[1].color = c;
            displayScales[i].scaleDegreeTexts[3].color = c;
            displayScales[i].scaleDegreeTexts[5].color = c;
        }
    }
}
