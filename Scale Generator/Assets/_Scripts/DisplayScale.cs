using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScale : MonoBehaviour
{
    public Text[] scaleDegreeTexts;    //scaleDegrees
    public Text[] noteTexts;  //note text

    private List<string> chordQualityList = new List<string> { "", "m", "m", "", "", "m", "dim" };

    public void UpdateNotes( MusicScale currentScale, int scaleDropValue )
    {        
        UpdateNoteTexts( currentScale );
        AddChordQualities( scaleDropValue );        
        UpdateScaleDegreeSymbols();
    }

    public void UpdateNotes_NoScaleDegreeSymbols( MusicScale currentScale, int scaleDropValue )
    {
        UpdateNoteTexts( currentScale );
        AddChordQualities( scaleDropValue );
    }

    private void UpdateNoteTexts( MusicScale currentScale )
    {
        for ( int i = 0; i < noteTexts.Length; i++ )
        {
            int key = i + 1;
            if ( key > 7 )
            {
                key = 1;
            }
            noteTexts[i].text = currentScale.NotesInScale[key.ToString()].ToString();
        }
    }

    private void AddChordQualities( int scaleDropValue )
    {
        List<string> qualities = GetChordQualitiesBasedOnScale( scaleDropValue );
        int lastIndex = noteTexts.Length - 1;

        for ( int i = 0; i < qualities.Count; i++ )
        {
            noteTexts[i].text += qualities[i];
        }
        noteTexts[lastIndex].text += qualities[0];
    }

    private List<string> GetChordQualitiesBasedOnScale( int scaleDropValue )
    {
        List<string> qualities = new List<string>();
        int scaleIndex = CalculateScaleModePosition( scaleDropValue );

        for ( int i = 0; i < chordQualityList.Count; i++ )
        {
            qualities.Add( chordQualityList[scaleIndex] );
            scaleIndex++;

            if ( scaleIndex >= chordQualityList.Count )
            {
                scaleIndex = scaleIndex - 7;
            }
        }
        return qualities;
    }

    //*NOTE* since the scales are out of order in the dropdown menu. We need a special function to find the correct starting point
    private int CalculateScaleModePosition( int dropValue )
    {
        int modePosition = 0;
        switch ( dropValue )
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

    public void UpdateScaleDegreeSymbols()
    {
        for ( int i = 0; i < scaleDegreeTexts.Length; i++ )
        {
            scaleDegreeTexts[i].text = ParseChordQuality( noteTexts[i].text, i );
        }
    }

    private string ParseChordQuality( string chord, int scaleDegree )
    {
        string scaleDegreeSymbol = CalculateScaleDegreeSymbol( scaleDegree );

        if ( chord.Contains( "m" ) || chord.Contains( "dim" ) )
        {
            scaleDegreeSymbol = scaleDegreeSymbol.ToLower();
        }
        if ( chord.Contains( "dim" ) )
        {
            //'\u2205' is for half-diminished symbol
            //unicode symbols found here https://www.fileformat.info/info/unicode/font/arial_unicode_ms/list.htm
            scaleDegreeSymbol += '\u2205';
        }

        return scaleDegreeSymbol;
    }

    private string CalculateScaleDegreeSymbol( int degree )
    {
        string degreeSymbol = "";

        switch ( degree )
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
}
