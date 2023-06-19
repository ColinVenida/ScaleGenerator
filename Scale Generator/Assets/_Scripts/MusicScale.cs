using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicScale 
{
    public string rootNote { get; }
    private Note rootNoteObject;
    private ScaleFormulas.ScaleFormula formula;
    public ScaleFormulas.ScaleFormula Formula { get { return formula; } }

    private Dictionary<string, Note> notesInScale;
    public Dictionary<string, Note> NotesInScale { get { return notesInScale; } }

    private bool isTheoretical = false;
    public bool IsTheoretical 
    {
        get { return isTheoretical; }
        private set { isTheoretical = value; }
    }


    public MusicScale( string root, ScaleFormulas.ScaleFormula formula )
    {
        rootNote = root;
        rootNoteObject = new Note( root );
        this.formula = formula;        
        
        notesInScale = new Dictionary<string, Note>();

        PopulateNotesInScale();
        AdjustSharpsOrFlatsInScale( formula );        
    }

    private void PopulateNotesInScale()
    {
        //add the notes in the correct order 
        notesInScale.Add( "1", rootNoteObject );

        const int REMAINING_NOTES = 7;
        string nextName = FindNextNote( rootNoteObject.GetNaturalName() );        

        for ( int i = 2; i <= REMAINING_NOTES; i++ )
        {
            notesInScale.Add( i.ToString(), new Note( nextName ) );
            nextName = FindNextNote( nextName );
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
        string noteName = rootNoteObject.GetNaturalName();
        int offset = GetPitchOffset( rootNoteObject.pitch );
        int scaleSemitones = offset;
        int formulaIntervals = 0;

        for( int i = 1; i < formula.scaleIntervals.Count + 1; i++ )
        {
            //add up the intervals in the formula, and then compare to the natural note intervals
            //any differences between the totals means the NEXT note needs to be sharp/flat
            
            scaleSemitones += NaturalIntervals.naturalIntervals[noteName].NextNoteSemitone;
            formulaIntervals += formula.scaleIntervals[i.ToString()];

            int nextScaleDegree = i + 1;
            if ( nextScaleDegree > 7 )
            {
                nextScaleDegree = 1;
            }
               
            AdjustPitchInNote( nextScaleDegree, formulaIntervals, scaleSemitones );            
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

    private void AdjustPitchInNote( int scaleDegree, int formulaIntervals, int scaleSemitones )
    {
        if ( formulaIntervals > scaleSemitones )
        {
            //note needs to be sharp                
            notesInScale[scaleDegree.ToString()].pitch = PitchModifier.Sharp;
        }
        else if ( formulaIntervals < scaleSemitones )
        {
            //note needs to be flat                
            notesInScale[scaleDegree.ToString()].pitch = PitchModifier.Flat;
        }
        else
        {
            //note is natural
            notesInScale[scaleDegree.ToString()].pitch = PitchModifier.Natural;
        }

        notesInScale[scaleDegree.ToString()].RecalculatePitchValue();
        DetermineDoublePitchModifier( formulaIntervals, scaleSemitones, notesInScale[scaleDegree.ToString()] );
    }
        
    private void DetermineDoublePitchModifier( int formulaIntervals, int scaleSemitones, Note note )
    {
        int difference = Math.Abs( formulaIntervals - scaleSemitones );
        if ( difference > 1 )
        {
            note.SetDoublePitchModifier();
            IsTheoretical = true;
        }            
    }

    public void PrintNotesInScale()
    {
        foreach ( var kvp in notesInScale )
        {
            Debug.Log( "key = " + kvp.Key + ", value = " + kvp.Value );
        }
    }

    //find which scaleDegree in the music scale the given note is
    //eg. which scaleDegree is the note "A" in the music scale "C" (result = 6th scaleDegree)
    public int FindNoteScaleDegree( Note n )
    {
        int scaleDegree = 0;
        string name = n.GetNaturalName();

        for( int i = 1; i < notesInScale.Count + 1; i++ )
        {
            if ( name == notesInScale[i.ToString()].GetNaturalName() )
                scaleDegree = i;                
        }
        //Debug.Log( "the note " + n + " is the " + scaleDegree + " scale degree in " + this );
        return scaleDegree;
    }

    public override string ToString()
    {
        return ( rootNote + " " + formula.scaleName );
    }

    public override bool Equals( System.Object obj)
    {
        //Check for null and compare run-time types.
        if ( ( obj == null ) || !this.GetType().Equals( obj.GetType() ) )
        {
            return false;
        }
        else
        {
            MusicScale scale = ( MusicScale )obj;
            return ( ( this.rootNoteObject.NaturalName == scale.rootNoteObject.NaturalName ) &&
                        ( this.rootNoteObject.pitch == scale.rootNoteObject.pitch ) ); 
        }
    }

    public bool Equals_StringArgument( string rootNoteName )
    {
        //true if the "1" note in the dictionary has the same name
        return ( rootNoteName == this.NotesInScale[1.ToString()].ToString() );
    }
}
