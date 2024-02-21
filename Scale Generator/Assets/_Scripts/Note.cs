using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note 
{    
    private string naturalName;    
    public string NaturalName { get { return naturalName; } }

    private int semitoneToNextNaturalNote;
    public int SemitoneToNextNatrualNote { get { return semitoneToNextNaturalNote; } }

    private int semitoneToPrevNaturalNote;
    public int SemitoneToPrevNatrualNote { get { return semitoneToPrevNaturalNote; } }

    private int pitchValue;
    public int PitchValue {  get { return pitchValue; } }

    public PitchModifier pitch;
    public PitchOverlap pitchOverlap = PitchOverlap.Natural_NONE;
    public bool hasDoubledPitchModifier = false;
        


    //constructor for a blank note
    public Note ()
    {
        naturalName = "";   
    }

    public Note( string n )
    {        
        ParseName( n );               
    }

    public Note( string n, int next, int prev)
    {        
        ParseName( n ); 
    }

    private void ParseName( string n )
    {        
        if ( n.Length == 1 )
        {
            naturalName = n;
            pitch = PitchModifier.Natural;
        }
        else if ( n[1] == '#' )
        {
            naturalName = n.Substring(0, 1);
            pitch = PitchModifier.Sharp;            
        }
        else
        {
            naturalName = n.Substring( 0, 1 );
            pitch = PitchModifier.Flat;
        }
        SetPitchValue();
        SetIntervalsToNextAndPrevNaturalNotes();

        //*usually notes are created by the MusicScale class, but this case refers to a note created by the 
        //  SecondaryDominantCalculator**
        if ( n.Length == 3 )
        {
            SetDoublePitchModifier();
        }
    }

    //need a separate function for to set up PitchValue because MusicScale creates the notes first,
    //  then adjusts the sharps/flats after
    public void RecalculatePitchValue()
    {
        SetPitchValue();
        SetIntervalsToNextAndPrevNaturalNotes();
    }

    private void SetPitchValue()
    {        
        pitchValue = PitchValues.PitchValueDicitonary_NaturalNotes[ this.NaturalName ];
        pitchValue = pitchValue + CalculatePitchValueOffset();
        CheckPitchValueBounds();
    }

    private int CalculatePitchValueOffset()
    {
        int offset = 0;
        
        switch( pitch )
        {
            case PitchModifier.Flat:
                offset = -1;
                break;
            case PitchModifier.Sharp:
                offset = 1;
                break;
        }        
        return offset;
    }

    private void CheckPitchValueBounds()
    {
        if ( pitchValue < PitchValues.LOWER_LIMIT )
        {
            pitchValue += PitchValues.UPPER_LIMIT;
            pitchOverlap = PitchOverlap.FlatOverlap;
        }
        else if ( pitchValue > (PitchValues.UPPER_LIMIT - 1) )
        {
            pitchValue -= PitchValues.UPPER_LIMIT;
            pitchOverlap = PitchOverlap.SharpOverlap;
        }
    }

    private void SetIntervalsToNextAndPrevNaturalNotes()
    {
        int nextNoteOffset = 0;
        int prevNoteOffset = 0;

        switch ( this.pitch )
        {
            case PitchModifier.Flat:
                nextNoteOffset = 1;
                prevNoteOffset = -1;
                break;
            case PitchModifier.Sharp:
                nextNoteOffset = -1;
                prevNoteOffset = 1;
                break;
        }
        
        semitoneToNextNaturalNote = ( nextNoteOffset + NaturalIntervals.naturalIntervals[naturalName].NextNoteSemitone );
        semitoneToPrevNaturalNote = ( prevNoteOffset + NaturalIntervals.naturalIntervals[naturalName].PrevNoteSemitone );        
    }

    public void SetDoublePitchModifier()
    {
        hasDoubledPitchModifier = true;        

        switch ( this.pitch )
        {
            case PitchModifier.Flat:
                semitoneToNextNaturalNote += 1;
                semitoneToPrevNaturalNote += -1;
                pitchValue -= 1; 
                CheckPitchValueBounds();
                break;
            case PitchModifier.Sharp:
                semitoneToNextNaturalNote += -1;
                semitoneToPrevNaturalNote += 1;
                pitchValue += 1;
                CheckPitchValueBounds();
                break;
        }
    }

    private PitchModifier DeterminePitchMod( string n )
    {
        if ( n.Length == 1 )
            return PitchModifier.Natural;
        else if ( n[1] == '#' )
            return PitchModifier.Sharp;
        else
            return PitchModifier.Flat;
    }
        
    public string GetNaturalName()
    {
        return naturalName;
    }

    public override string ToString()
    {
        //return GetNote();
        string pitchMod = "";

        switch( pitch )
        {
            case PitchModifier.Flat:
                pitchMod = "b";
                break;
            case PitchModifier.Sharp:
                pitchMod = "#";
                break;
        }

        if ( hasDoubledPitchModifier )
            pitchMod += pitchMod;

        return naturalName + pitchMod;
    }

    public override bool Equals(System.Object obj )
    {
        //Check for null and compare run-time types.
        if ( ( obj == null ) || !this.GetType().Equals( obj.GetType() ) )
        {
            return false;
        }
        else
        {
            Note n = ( Note ) obj;
            return ( this.naturalName == n.naturalName && this.pitch == n.pitch );            
        }
    }
}
