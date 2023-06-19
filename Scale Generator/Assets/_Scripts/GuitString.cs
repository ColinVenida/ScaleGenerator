using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuitString : MonoBehaviour
{
    public Fretboard fBoard;       
    public Dropdown noteSelect;    
    public Button[] buttonArray;
    public Text[] textArray; 
    public Dropdown presetDrop;
    public int stringNumber;

    public static bool changePreset = true;  //variable for determining if the Preset Dropdown should be reset or not

    private Note currentTuning;

    private int[] arpeggioFrets = { 0, 0, 0, 0 };
    private string[] arpeggioNotes = { "", "", "", "" };

    public void CalculateFrets_New( int dropValue )
    {
        //workaround for avoiding the OnValueChanged event during Fretboard.Awake()
        if ( !fBoard.AreGuitStringsInitialized )
        {           
            return;
        }
        ResetUI();        
        SetCurrentTuning( dropValue );
        SaveTuning( dropValue );

        MusicScale currentScale = fBoard.CurrentMusicScale;
        int tuningScaleDegree = currentScale.FindNoteScaleDegree( currentTuning );       

        Debug.Log( "***new method! Calculating a string tuned to " + currentTuning + "***" );
        int startDegree = CalculateStartScaleDegree( currentScale, tuningScaleDegree ); 
        int startFret = CalculateStartFret( currentScale, startDegree );

        UpdateButtonAndTextArray( currentScale, startFret, startDegree );
    }

    private int CalculateStartScaleDegree( MusicScale scale, int tuningScaleDegree )
    {
        //based on the currentTuning and the MusicScale, find out which scaleDegree we need to start at
        int startScaleDegree = tuningScaleDegree;
        Note noteInScale = scale.NotesInScale[tuningScaleDegree.ToString()];
        int overlapOffset = CalculateOverlapOffset( noteInScale );
       
        int nextScaleDegree = FindNextScaleDegree( tuningScaleDegree );        
        Note nextNote = scale.NotesInScale[nextScaleDegree.ToString()];

        if ( (noteInScale.PitchValue + overlapOffset) <= currentTuning.PitchValue )   
        {
            startScaleDegree = FindNextScaleDegree( startScaleDegree );
            if ( AreNotesEnharmonic( nextNote, currentTuning ) )
            {
                startScaleDegree = FindNextScaleDegree( startScaleDegree );
            }
        }     

        return startScaleDegree;
    }

    private int CalculateOverlapOffset( Note note )
    {
        int overlapOffset = 0;
        switch( note.pitchOverlap)
        {
            case PitchOverlap.FlatOverlap:
                overlapOffset = ( PitchValues.UPPER_LIMIT * -1 );
                break;
            case PitchOverlap.SharpOverlap:
                overlapOffset = PitchValues.UPPER_LIMIT;
                break;
        }
        return overlapOffset;
    }
    
    //test data:        
    //A# lydian: A#, B#, C##, D##, E#, F##, G##, E-tuning, D-tuning, F-tuning, Bb-tuning, Gb tuning, C-tuning
    //is there any flat note that's enharmonic to a double sharp??
    //Gb locrian:  Gb, Abb, Bbb, Cb, Dbb, Ebb, Fb, G-tuning, B-tuning, F#-tuning, 
    // C major: C, D, E, F, G, A, B, 
    private bool AreEnharmonicNotesPresent( MusicScale scale, int tuningScaleDegree )    
    {       
        int prevScaleDegree = FindPrevScaleDegree( tuningScaleDegree );
        int nextScaleDegree = FindNextScaleDegree( tuningScaleDegree );

        Note prevNote = scale.NotesInScale[prevScaleDegree.ToString()];
        Note nextNote = scale.NotesInScale[nextScaleDegree.ToString()];
        bool areEnharmonic = false;
                        
        areEnharmonic = ( currentTuning.PitchValue == nextNote.PitchValue || currentTuning.PitchValue == prevNote.PitchValue );        

        return areEnharmonic;
    }

    private bool AreNotesEnharmonic( Note first, Note second )
    {
        return first.PitchValue == second.PitchValue;
    }

    private int CalculateStartFret( MusicScale scale, int startScaleDegree )
    {        
        int startFret = 0;
        Note noteForFretboard = scale.NotesInScale[startScaleDegree.ToString()];

        startFret = noteForFretboard.PitchValue - currentTuning.PitchValue;
        
        if( startFret < PitchValues.LOWER_LIMIT )
        {
            startFret += PitchValues.UPPER_LIMIT;
        }
       
        return startFret;
    }
   
    private int FindNextScaleDegree( int startDegree )
    {
        int nextScaleDegree = startDegree + 1;
        if ( nextScaleDegree > 7 )
        {
            nextScaleDegree = 1;
        }
        return nextScaleDegree;
    }

    private int FindPrevScaleDegree( int startDegree )
    {
        int nextScaleDegree = startDegree - 1;
        if ( nextScaleDegree < 1 )
        {
            nextScaleDegree = 7;
        }
        return nextScaleDegree;
    }      
   
    private void UpdateButtonAndTextArray( MusicScale scale, int startFret, int startScaleDegree )
    {
        int i = startFret - 1;
        int scaleDegree = startScaleDegree;
                
        try
        {
            while ( i < buttonArray.Length )
            {                
                string noteString = scale.NotesInScale[scaleDegree.ToString()].ToString();
                buttonArray[i].gameObject.SetActive( true );
                textArray[i].text = noteString;

                i = i + scale.Formula.scaleIntervals[scaleDegree.ToString()];
                scaleDegree = FindNextScaleDegree( scaleDegree );
            }
        }
        catch ( IndexOutOfRangeException e)
        {
            Debug.Log( "i is out of range of the array: " + i );
            Debug.Log( e.StackTrace );
        }       

        if ( fBoard.UseArpeggio() )
        {
            FilterArpeggio( fBoard.GetArplist() );
        }
    }
    
    private void ResetUI()
    {
        CancelArpeggio();
        DeactivateFrets();
        
        if ( changePreset )
        {
            presetDrop.value = 0;
        }
    }
   
    private void DeactivateFrets()
    {
        for ( int i = 0; i < textArray.Length; i++ )
        {
            buttonArray[i].gameObject.SetActive( false );
            buttonArray[i].image.color = new Color( 1.0f, 1.0f, 1.0f );
            textArray[i].text = "";
            textArray[i].color = new Color32( 0, 0, 0, 255 );
            textArray[i].fontStyle = FontStyle.Normal;
        }
    }

    public void FilterArpeggio( List<string> noteList )
    {       
        for (int i = 0; i < buttonArray.Length; i++)
        {
            if (noteList.Contains( textArray[i].text ))
            {
                buttonArray[i].gameObject.SetActive( false );
            }
        }
    }

    //function to set all the fretArray to active
    public void CancelArpeggio()
    {        
        for (int i = 0; i < buttonArray.Length; i++)
        {
            if ( textArray[i].text != "" )
            {
                buttonArray[i].gameObject.SetActive( true );
            }            
        }
    }    
       
    private void SaveTuning( int drop )
    {
        switch( this.stringNumber )
        {
            case 0:
                PlayerPrefs.SetInt( "GuitStringOne", drop );                
                break;
            case 1:
                PlayerPrefs.SetInt( "GuitStringTwo", drop );
                break;
            case 2:
                PlayerPrefs.SetInt( "GuitStringThree", drop );
                break;
            case 3:
                PlayerPrefs.SetInt( "GuitStringFour", drop );
                break;
            case 4:
                PlayerPrefs.SetInt( "GuitStringFive", drop );
                break;
            case 5:                
                PlayerPrefs.SetInt( "GuitStringSix", drop );                
                break;
            case 6:               
                PlayerPrefs.SetInt( "GuitStringSeven", drop );                
                break;
            case 7:                
                PlayerPrefs.SetInt( "GuitStringEight", drop );               
                break;
        }
        PlayerPrefs.Save();        
    }
    
    private void SetCurrentTuning ( int dropValue )
    {
        string noteName = NoteValues.ConvertNote_IntToString( dropValue );       
        currentTuning = new Note( noteName );
    } 

    private void Awake()
    {
        currentTuning = new Note();
    }
}
