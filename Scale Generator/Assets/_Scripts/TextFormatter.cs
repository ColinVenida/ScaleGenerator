using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using UnityEngine.UIElements;


//methods copied from ScaleScene.cs
public class TextFormatter : MonoBehaviour
{
    public Fretboard fBoard;
    public List<Text> TitleAndFretNumbersText;
    public DisplayScale[] displayScales;
    public DisplayScale fretboardDisplayScale;
    public DisplayScale menuDisplayScale;
    public UnityEngine.UI.Toggle useArpeggio;    
    public Dropdown scaleDrop;
    public Dropdown rootDrop;

    private List<string> chordQualityList = new List<string> { "", "m", "m", "", "", "m", "dim" };
    private Color BLACK = new Color( 0.0f, 0.0f, 0.0f );
    private Color WHITE = new Color( 1.0f, 1.0f, 1.0f );
    private Color LIGHT_GRAY = new Color( 0.2f, 0.2f, 0.2f, 0.2f );
    private Color DARK_GRAY = new Color( 0.196f, 0.196f, 0.196f );

    private const int SECOND_DEGREE_INDEX = 1;
    private const int FOURTH_DEGREE_INDEX = 3;
    private const int SIXTH_DEGREE_INDEX = 5;

    public void ChangeTextColorBasedOnMode( int dropValue )
    {
        switch ( dropValue )
        {
            case 0:     //Major/Ionian, black            
            case 4:     //Lydian, black
                ChangeTextColor( BLACK );
                break;
            default:    //the rest look better with white text
                ChangeTextColor( WHITE );
                break;
        }
    }

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
    
    private void ChangeTextColor( Color color )
    {
        for ( int i = 0; i < TitleAndFretNumbersText.Count; i++ )
        {
            TitleAndFretNumbersText[i].color = color;
        }
    }

    public void UpdateArpeggioTextColor()
    {
        ToggleFretboardDisplayScaleArpeggio();
        ToggleMenuDisplayScaleArpeggio();
    }

    private void ToggleFretboardDisplayScaleArpeggio()
    {
        Color c;
        if ( fBoard.UseArpeggio() )
        {
            c = LIGHT_GRAY; 
        }
        else
        {
            c = DetermineTextColor_BasedOnMode();
        }

        UpdateDisplayScaleArpeggioText( fretboardDisplayScale, c );
    }

    private Color DetermineTextColor_BasedOnMode()
    {
        Color color;
        switch ( scaleDrop.value )
        {
            case 0:     //Major/Ionian, black            
            case 4:     //Lydian, black
                color = BLACK;
                break;
            default:    //the rest look better with white text
                color = WHITE;
                break;
        }
        return color;
    }

    private void ToggleMenuDisplayScaleArpeggio()
    {
        Color c;

        if( fBoard.UseArpeggio() )
        {
            c = LIGHT_GRAY;
        }
        else
        {
            c = BLACK;
        }
        UpdateDisplayScaleArpeggioText( menuDisplayScale, c );
    }

    private void UpdateDisplayScaleArpeggioText( DisplayScale scale, Color c )
    {
        scale.noteTexts[SECOND_DEGREE_INDEX].color = c;
        scale.noteTexts[FOURTH_DEGREE_INDEX].color = c;
        scale.noteTexts[SIXTH_DEGREE_INDEX].color = c;

        scale.scaleDegreeTexts[SECOND_DEGREE_INDEX].color = c;
        scale.scaleDegreeTexts[FOURTH_DEGREE_INDEX].color = c;
        scale.scaleDegreeTexts[SIXTH_DEGREE_INDEX].color = c;
    }

}
