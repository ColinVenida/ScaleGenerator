using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note 
{    
    private string naturalName;    
    public string NaturalName { get { return naturalName; } }

    private int semitoneToNextNote;
    public int SemitoneToNextNote { get { return semitoneToNextNote; } }

    private int semitoneToPrevNote;
    public int SemitoneToPrevNote { get { return semitoneToPrevNote; } }

    private int pitchValue;
    public int PitchValue {  get { return pitchValue; } }

    public PitchModifier pitch;
    public PitchOverlap pitchOverlap = PitchOverlap.Natural_NONE;
    public bool hasDoubledPitchModifier = false;

    public bool usedSharp;
    public bool usedFlat;
    public bool doubleSharp;
    public bool doubleFlat;
        

    //constructor for a blank note
    public Note ()
    {
        naturalName = "";               
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    public Note( string n )
    {        
        ParseName( n );       
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    public Note( string n, int next, int prev)
    {        
        ParseName( n );       
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
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
        SetIntervalsToNextAndPrevNotes();
    }

    //need a separate function for to set up PitchValue because MusicScale creates the notes first,
    //  then adjusts the sharps/flats after
    public void RecalculatePitchValue()
    {
        SetPitchValue();
        SetIntervalsToNextAndPrevNotes();
    }

    private void SetPitchValue()
    {
        pitchValue = PitchValues.AssignPitchValue( this );
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
        if ( pitchValue < 0 )
        {
            pitchValue += 12;
            pitchOverlap = PitchOverlap.FlatOverlap;
        }
        else if ( pitchValue > 11 )
        {
            pitchValue -= 12;
            pitchOverlap = PitchOverlap.SharpOverlap;
        }
    }

    private void SetIntervalsToNextAndPrevNotes()
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
        
        semitoneToNextNote = ( nextNoteOffset + NaturalIntervals.naturalIntervals[naturalName].NextNoteSemitone );
        semitoneToPrevNote = ( prevNoteOffset + NaturalIntervals.naturalIntervals[naturalName].PrevNoteSemitone );        
    }

    public void SetDoublePitchModifier()
    {
        hasDoubledPitchModifier = true;        

        switch ( this.pitch )
        {
            case PitchModifier.Flat:
                semitoneToNextNote += 1;
                semitoneToPrevNote += -1;
                pitchValue -= 1; 
                CheckPitchValueBounds();
                break;
            case PitchModifier.Sharp:
                semitoneToNextNote += -1;
                semitoneToPrevNote += 1;
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

    public string GetNote ()
    {
        if( usedSharp )
        {
            if( doubleSharp )
            {
                return naturalName + "##";
            }
            else 
            {
                return naturalName + "#";
            }            
        }
        else if ( usedFlat )
        {
            if ( doubleFlat )
            {
                return naturalName + "bb";
            }
            else
            {
                return naturalName + "b";
            }
        }
        else
        {
            return naturalName;
        }
    }

    public string GetNaturalName()
    {
        return naturalName;
    }

    public bool IsSameNote_IgnorePitch( Note n )
    {
        return ( this.naturalName == n.naturalName );
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
