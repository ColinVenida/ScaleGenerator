using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//methods copied from ScaleScene.cs
public class TextFormatter : MonoBehaviour
{
    public Fretboard fBoard;   
    public DisplayScale[] displayScale;
    
    public Dropdown scaleDrop;
    public Dropdown rootDrop;    

    public void UpdateScale ()
    {    

        for (int j = 0; j < displayScale.Length; j++ )
        {
            //**find the starting point of the scale here***
            int familyIndex = fBoard.scaleGen.FindFamilyIndex( rootDrop.value );
            for (int i = 0; i < 8; i++)
            {
                displayScale[j].displayText[i].text = fBoard.noteArray.noteArray[familyIndex].GetNote();
                familyIndex++;
                if (familyIndex > 6)
                {
                    familyIndex = 0;
                }
            }
            UpdateDisplay( j );
        }            
        //SavePlayerPrefs();
    }

    public void UpdateDisplay( int display )
    {
        if ( !fBoard.GetIsInitialized() )
        {
            return;
        }

        //add minor/diminished
        switch ( scaleDrop.value )
        {
            case 0: //ionian
            case 4: //lydian
            case 5: //mixolydian
                displayScale[display].displayText[1].text += "m";
                displayScale[display].displayText[2].text += "m";
                displayScale[display].displayText[5].text += "m";
                displayScale[display].displayText[6].text += "dim";
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                displayScale[display].displayText[0].text += "m";
                displayScale[display].displayText[1].text += "dim";
                displayScale[display].displayText[3].text += "m";
                displayScale[display].displayText[4].text += "m";
                displayScale[display].displayText[7].text += "m";
                break;
        }
        UpdatePositions( display, scaleDrop );
    }

    public void UpdatePositions( int display, Dropdown scale )
    {
        switch ( scale.value )
        {
            //'\u2205' is for half-diminished symbol
            //unicode symbols found here https://www.fileformat.info/info/unicode/font/arial_unicode_ms/list.htm
            case 0: //ionian
            case 4: //lydian
            case 5: //mixolydian
                displayScale[display].notePositions[0].text = "I";
                displayScale[display].notePositions[1].text = "ii";
                displayScale[display].notePositions[2].text = "iii";
                displayScale[display].notePositions[3].text = "IV";
                displayScale[display].notePositions[4].text = "V";
                displayScale[display].notePositions[5].text = "vi";
                displayScale[display].notePositions[6].text = "vii" + '\u2205';
                displayScale[display].notePositions[7].text = "I";
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                displayScale[display].notePositions[0].text = "i";
                displayScale[display].notePositions[1].text = "ii" + '\u2205';
                displayScale[display].notePositions[2].text = "III";
                displayScale[display].notePositions[3].text = "iv";
                displayScale[display].notePositions[4].text = "v";
                displayScale[display].notePositions[5].text = "VI";
                displayScale[display].notePositions[6].text = "VII";
                displayScale[display].notePositions[7].text = "i";
                break;
        }
    }
    
    //gray out the 2, 4, and 6 positions
    public void ShowArpeggio( bool show )
    {
        if ( show )
        {
            for ( int i = 0; i < displayScale.Length; i++ )
            {
                displayScale[i].displayText[1].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScale[i].displayText[3].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScale[i].displayText[5].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );

                displayScale[i].notePositions[1].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScale[i].notePositions[3].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
                displayScale[i].notePositions[5].color = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
            }
        }
        else
        {
            for (int i = 0; i < displayScale.Length; i++)
            {
                displayScale[i].displayText[1].color = new Color( 0.196f, 0.196f, 0.196f );                
                displayScale[i].displayText[3].color = new Color( 0.196f, 0.196f, 0.196f );
                displayScale[i].displayText[5].color = new Color( 0.196f, 0.196f, 0.196f );

                displayScale[i].notePositions[1].color = new Color( 0.196f, 0.196f, 0.196f );
                displayScale[i].notePositions[3].color = new Color( 0.196f, 0.196f, 0.196f );
                displayScale[i].notePositions[5].color = new Color( 0.196f, 0.196f, 0.196f );
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
