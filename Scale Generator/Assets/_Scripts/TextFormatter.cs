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

    private Color lightGray = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
    private Color darkGray = new Color( 0.196f, 0.196f, 0.196f );

    //***BUG***
    //UpdateScale does not update the last note in DisplayScales (the octave)
    public void UpdateScale ()
    {    
        for ( int j = 0; j < displayScales.Length; j++ )
        {
            for( int i = 0; i < fBoard.CurrentMusicScale.NotesInScale.Count; i++ )
            {
                int key = i + 1;
                displayScales[j].noteTexts[i].text = fBoard.CurrentMusicScale.NotesInScale[key.ToString()].ToString();
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
                AddMajorScaleQualities( displayScales[displayIndex] );
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                AddMinorScaleQualities( displayScales[displayIndex] );
                break;
        }
        UpdatePositions( displayIndex, scaleDrop );
    }

    private void AddMajorScaleQualities( DisplayScale dScale )
    {
        dScale.noteTexts[1].text += "m";
        dScale.noteTexts[2].text += "m";
        dScale.noteTexts[5].text += "m";
        dScale.noteTexts[6].text += "dim";
    }

    private void AddMinorScaleQualities( DisplayScale dScale )
    {
        dScale.noteTexts[0].text += "m";
        dScale.noteTexts[1].text += "dim";
        dScale.noteTexts[3].text += "m";
        dScale.noteTexts[4].text += "m";
        dScale.noteTexts[7].text += "m";
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
                ChangeDisplayScaleToMajorIntervals( displayScales[display] );
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian                
                ChangeDisplayScaleToMinorIntervals( displayScales[display] );
                break;
        }
    }

    private void ChangeDisplayScaleToMajorIntervals( DisplayScale dScale )
    {
        dScale.intervalTexts[0].text = "I";
        dScale.intervalTexts[1].text = "ii";
        dScale.intervalTexts[2].text = "iii";
        dScale.intervalTexts[3].text = "IV";
        dScale.intervalTexts[4].text = "V";
        dScale.intervalTexts[5].text = "vi";
        dScale.intervalTexts[6].text = "vii" + '\u2205';
        dScale.intervalTexts[7].text = "I";
    }

    private void ChangeDisplayScaleToMinorIntervals( DisplayScale dScale )
    {
        dScale.intervalTexts[0].text = "i";
        dScale.intervalTexts[1].text = "ii" + '\u2205';
        dScale.intervalTexts[2].text = "III";
        dScale.intervalTexts[3].text = "iv";
        dScale.intervalTexts[4].text = "v";
        dScale.intervalTexts[5].text = "VI";
        dScale.intervalTexts[6].text = "VII";
        dScale.intervalTexts[7].text = "i";
    }    
    
    public void EnableArpeggioColor()
    {
        UpdateDisplayArpeggioColor( lightGray );
    }

    public void DisableArpeggioColor()
    {
        UpdateDisplayArpeggioColor( darkGray );
    }

    //apply color to the 2, 4, and 6 intervals   
    private void UpdateDisplayArpeggioColor( Color c )
    {
        for ( int i = 0; i < displayScales.Length; i++ )
        {
            displayScales[i].noteTexts[1].color = c;
            displayScales[i].noteTexts[3].color = c;
            displayScales[i].noteTexts[5].color = c;

            displayScales[i].intervalTexts[1].color = c;
            displayScales[i].intervalTexts[3].color = c;
            displayScales[i].intervalTexts[5].color = c;
        }
    }
}
