using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//methods copied from ScaleScene.cs
public class TextFormatter : MonoBehaviour
{
    public Fretboard fBoard;   
    public DisplayScale[] displayScales;
    public Toggle useArpeggio;    
    public Dropdown scaleDrop;
    public Dropdown rootDrop;

    public void UpdateScale ()
    {    

        for (int j = 0; j < displayScales.Length; j++ )
        {
            //**find the starting point of the scale here***
            int familyIndex = fBoard.scaleGen.FindFamilyIndex( rootDrop.value );
            for (int i = 0; i < 8; i++)
            {
                displayScales[j].noteTexts[i].text = fBoard.noteArray.noteArray[familyIndex].GetNote();
                familyIndex++;
                if (familyIndex > 6)
                {
                    familyIndex = 0;
                }
            }
            AddChordQuality( j );
        }                  
    }

    private void AddChordQuality( int displayIndex )
    {        
        //add minor/diminished
        switch ( scaleDrop.value )
        {
            case 0: //ionian
            case 4: //lydian
            case 5: //mixolydian
                displayScales[displayIndex].noteTexts[1].text += "m";
                displayScales[displayIndex].noteTexts[2].text += "m";
                displayScales[displayIndex].noteTexts[5].text += "m";
                displayScales[displayIndex].noteTexts[6].text += "dim";
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                displayScales[displayIndex].noteTexts[0].text += "m";
                displayScales[displayIndex].noteTexts[1].text += "dim";
                displayScales[displayIndex].noteTexts[3].text += "m";
                displayScales[displayIndex].noteTexts[4].text += "m";
                displayScales[displayIndex].noteTexts[7].text += "m";
                break;
        }
        UpdatePositions( displayIndex, scaleDrop );
    }

    private void UpdatePositions( int display, Dropdown scale )
    {
        switch ( scale.value )
        {
            //'\u2205' is for half-diminished symbol
            //unicode symbols found here https://www.fileformat.info/info/unicode/font/arial_unicode_ms/list.htm
            case 0: //ionian
            case 4: //lydian
            case 5: //mixolydian
                displayScales[display].intervalTexts[0].text = "I";
                displayScales[display].intervalTexts[1].text = "ii";
                displayScales[display].intervalTexts[2].text = "iii";
                displayScales[display].intervalTexts[3].text = "IV";
                displayScales[display].intervalTexts[4].text = "V";
                displayScales[display].intervalTexts[5].text = "vi";
                displayScales[display].intervalTexts[6].text = "vii" + '\u2205';
                displayScales[display].intervalTexts[7].text = "I";
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                displayScales[display].intervalTexts[0].text = "i";
                displayScales[display].intervalTexts[1].text = "ii" + '\u2205';
                displayScales[display].intervalTexts[2].text = "III";
                displayScales[display].intervalTexts[3].text = "iv";
                displayScales[display].intervalTexts[4].text = "v";
                displayScales[display].intervalTexts[5].text = "VI";
                displayScales[display].intervalTexts[6].text = "VII";
                displayScales[display].intervalTexts[7].text = "i";
                break;
        }
    }
    
    //gray out the 2, 4, and 6 positions
    public void ShowArpeggio( bool show )
    {        
        if ( show )
        {
            for ( int i = 0; i < displayScales.Length; i++ )
            {
                displayScales[i].noteTexts[1].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScales[i].noteTexts[3].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScales[i].noteTexts[5].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );                

                displayScales[i].intervalTexts[1].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScales[i].intervalTexts[3].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScales[i].intervalTexts[5].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
            }
        }
        else
        {
            for (int i = 0; i < displayScales.Length; i++)
            {
                displayScales[i].noteTexts[1].color = new Color( 0.196f, 0.196f, 0.196f );                
                displayScales[i].noteTexts[3].color = new Color( 0.196f, 0.196f, 0.196f );
                displayScales[i].noteTexts[5].color = new Color( 0.196f, 0.196f, 0.196f );

                displayScales[i].intervalTexts[1].color = new Color( 0.196f, 0.196f, 0.196f );
                displayScales[i].intervalTexts[3].color = new Color( 0.196f, 0.196f, 0.196f );
                displayScales[i].intervalTexts[5].color = new Color( 0.196f, 0.196f, 0.196f );
            }
        }
    }  
}
