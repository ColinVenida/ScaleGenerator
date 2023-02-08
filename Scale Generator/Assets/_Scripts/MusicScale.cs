using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicScale 
{
    public string rootNote { get; }
    private Note rootNoteObject;

    private Dictionary<string, Note> notesInScale;

    //the distance between natural notes in music
    private Dictionary<string, int> naturalIntervals = new Dictionary<string, int>()
    {
        {"A", 2 },
        {"B", 1 },
        {"C", 2 },
        {"D", 2 },
        {"E", 1 },
        {"F", 2 },
        {"G", 2 },
    };

    public MusicScale( string root, ScaleFormulas.ScaleFormula formula )
    {
        rootNote = root;
        rootNoteObject = new Note( root );
        
        notesInScale = new Dictionary<string, Note>();

        PopulateNotesInScale();
        AdjustSharpsOrFlatsInScale( formula );
        PrintNotesInScale();
    }

    private void PopulateNotesInScale()
    {
        //add the notes in the correct order 
        notesInScale.Add( "1", rootNoteObject );

        const int REMAINING_NOTES = 7;
        string nextName = FindNextNote( rootNoteObject.GetName() );        

        for ( int i = 2; i <= REMAINING_NOTES; i++ )
        {
            notesInScale.Add( i.ToString(), new Note( nextName ) );
            nextName = FindNextNote( nextName );
        }
    }

    private void PrintNotesInScale()
    {
        foreach ( var kvp in notesInScale )
        {            
            Debug.Log( "key = " + kvp.Key + ", value = " + kvp.Value );
        }
    }

    private string FindNextNote( string note )
    {
        string next;

        switch ( note )
        {
            case "A":
                next = "B";
                break;
            case "B":
                next = "C";
                break;
            case "C":
                next = "D";
                break;
            case "D":
                next = "E";
                break;
            case "E":
                next = "F";
                break;
            case "F":
                next = "G";
                break;
            case "G":
                next = "A";
                break;
            default:
                next = "C";
                break;
        }
        return next;
    }
    
    private void AdjustSharpsOrFlatsInScale( ScaleFormulas.ScaleFormula formula )
    {
        //***TODO need to program if the root note is already sharp/flat? will that affectd the math and if-conditions?***

        string noteName = rootNoteObject.GetName();
        int offset = GetPitchOffset( rootNoteObject.pitch );
        int scaleSemitones = offset;
        int formulaIntervals = 0;

        for( int i = 1; i < formula.scaleIntervals.Count + 1; i++ )
        {
            //add up the intervals in the formula, and then compare to the intervals in the natural notes
            //any differences between the totals means the NEXT note needs to be sharp/flat

            scaleSemitones += naturalIntervals[noteName];
            formulaIntervals += formula.scaleIntervals[i.ToString()];

            int nextInterval = i + 1;
            if ( nextInterval > 7 )
                nextInterval = 1;

            AdjustPitchInNote( nextInterval, formulaIntervals, scaleSemitones );            
            noteName = FindNextNote( noteName );
        }
    }
    private int GetPitchOffset( PitchModifier mod )
    {
        switch ( mod )
        {
            case PitchModifier.Flat:        //next note needs one semitone MORE to get to the next natural note
                return 1;
            case PitchModifier.Natural:
                return 0;
            case PitchModifier.Sharp:       //next note needs one semitone LESS to get to the next natural note
                return -1;
            default:
                return 0;
        }
    }

    private void AdjustPitchInNote( int interval, int formulaIntervals, int scaleSemitones )
    {
        if ( formulaIntervals > scaleSemitones )
        {
            //note needs to be sharp                
            notesInScale[interval.ToString()].pitch = PitchModifier.Sharp;
        }
        else if ( formulaIntervals < scaleSemitones )
        {
            //note needs to be flat                
            notesInScale[interval.ToString()].pitch = PitchModifier.Flat;
        }
        else
        {
            //note is natural
            notesInScale[interval.ToString()].pitch = PitchModifier.Natural;
        }

        notesInScale[interval.ToString()].hasDoubledPitchModifier = HasDoublePitchModifier( formulaIntervals, scaleSemitones );
    }

    private bool HasDoublePitchModifier( int formulaIntervals, int scaleSemitones )
    {
        int difference = Math.Abs( formulaIntervals - scaleSemitones );

        return ( difference > 1 );
    }
}
