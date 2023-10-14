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

   
    public void UpdateScale ()
    {    
        for ( int j = 0; j < displayScales.Length; j++ )
        {            
            for ( int i = 0; i < displayScales[j].noteTexts.Length; i++ )
            {                
                int key = i + 1;
                if( key > 7 )
                {
                    key = 1;
                }
                displayScales[j].noteTexts[i].text = fBoard.CurrentMusicScale.NotesInScale[key.ToString()].ToString();               
            }            
            AddChordQualities( displayScales[j] );
            UpdateScaleDegreeSymbols( displayScales[j] );
        }        
    }

    private void AddChordQualities( DisplayScale scale )
    {
        List<string> qualities = GetChordQualitiesBasedOnScale( scaleDrop.value );        
        int lastIndex = scale.noteTexts.Length - 1;

        for( int i = 0; i < qualities.Count; i++ )
        {
            scale.noteTexts[i].text += qualities[i];
        }
        scale.noteTexts[lastIndex].text += qualities[0];
    }

    private List<string> GetChordQualitiesBasedOnScale( int scaleDropValue )
    {
        List<string> qualities = new List<string>();
        int scaleIndex = CalculateScaleModePosition( scaleDropValue );

        for( int i = 0; i < chordQualityList.Count; i++ )
        {
            qualities.Add( chordQualityList[scaleIndex] );
            scaleIndex++;

            if( scaleIndex >= chordQualityList.Count )
            {
                scaleIndex = scaleIndex - 7;
            }
        }
        return qualities;
    }

    //*NOTE* since the scales are out of order in the dropdown menu. We need a special function to find the correct starting point
    private int CalculateScaleModePosition ( int dropValue )
    {
        int modePosition = 0;
        switch( dropValue )
        {
            case 0:     //MAJOR
                modePosition = 0;
                break;
            case 1:     //MINOR
                modePosition = 5;
                break;
            case 2:     //DORIAN
                modePosition = 1;
                break;
            case 3:     //PHRYGIAN
                modePosition = 2;
                break;
            case 4:     //LYDIAN
                modePosition = 3;
                break;
            case 5:     //MIXOLYDIAN
                modePosition = 4;
                break;
            case 6:     //LOCRIAN
                modePosition = 6;
                break;
        }
        return modePosition;
    }

    private void UpdateScaleDegreeSymbols( DisplayScale scale )
    {
        for ( int i = 0; i < scale.intervalTexts.Length; i++ )
        {            
            scale.intervalTexts[i].text = ParseChordQuality( scale.noteTexts[i].text, i );
        }
    }

    private string ParseChordQuality( string chord, int scaleDegree )
    {
        string scaleDegreeSymbol = CalculateScaleDegreeSymbol( scaleDegree );

        if ( chord.Contains("m") || chord.Contains("dim") )
        {            
            scaleDegreeSymbol = scaleDegreeSymbol.ToLower();
        }
        else if ( chord.Contains("dim") )
        {
            //'\u2205' is for half-diminished symbol
            //unicode symbols found here https://www.fileformat.info/info/unicode/font/arial_unicode_ms/list.htm
            scaleDegreeSymbol += '\u2205';
        }

        return scaleDegreeSymbol;
    }

    private string CalculateScaleDegreeSymbol ( int degree )
    {
        string degreeSymbol = "";

        switch( degree )
        {
            case 0:
            case 7:
                degreeSymbol = "I";
                break;
            case 1:
                degreeSymbol = "II";
                break;
            case 2:
                degreeSymbol = "III";
                break;
            case 3:
                degreeSymbol = "IV";
                break;
            case 4:
                degreeSymbol = "V";
                break;
            case 5:
                degreeSymbol = "VI";
                break;
            case 6:
                degreeSymbol = "VII";
                break;
        }

        return degreeSymbol;
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

            displayScales[i].intervalTexts[1].color = c;
            displayScales[i].intervalTexts[3].color = c;
            displayScales[i].intervalTexts[5].color = c;
        }
    }
}
