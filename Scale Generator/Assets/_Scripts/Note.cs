using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note 
{
    //public char name;
    private string name;    
    public int nextWholetone;
    public int prevWholetone;
    public PitchModifier pitch;
    public bool hasDoubledPitchModifier = false;

    public bool usedSharp;
    public bool usedFlat;
    public bool doubleSharp;
    public bool doubleFlat;
        

    //constructor for a blank note
    public Note ()
    {
        name = "";       
        nextWholetone = 0;
        prevWholetone = 0;
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    public Note( string n )
    {
        //name = n;
        //pitch = DeterminePitchMod( n );
        ParseName( n );
        nextWholetone = 0;
        prevWholetone = 0;
        
        
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    public Note( string n, int next, int prev)
    {
        //name = n;
        //pitch = DeterminePitchMod( n );
        ParseName( n );
        nextWholetone = next;
        prevWholetone = prev;
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    private void ParseName( string n )
    {        
        if( n.Length == 1 )
        {
            name = n;
            pitch = PitchModifier.Natural;
        }
        else if ( n[1] == '#' )
        {
            name = n.Substring(0, 1);
            pitch = PitchModifier.Sharp;
        }
        else
        {
            name = n.Substring( 0, 1 );
            pitch = PitchModifier.Flat;
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
                return name + "##";
            }
            else 
            {
                return name + "#";
            }            
        }
        else if ( usedFlat )
        {
            if ( doubleFlat )
            {
                return name + "bb";
            }
            else
            {
                return name + "b";
            }
        }
        else
        {
            return name;
        }
    }

    public string GetName()
    {
        return name;
    }

    //function to change the note's name (ie. A, B, C) and then calls the ChangePrevNext function
    public void SetName( string n )
    {
        name = n;
        ChangePrevAndNextWholetone( name );
    }

    //function that updates the nextWholetone/prevWholetone according to he given id
    private void ChangePrevAndNextWholetone( string name )
    {
        switch ( name )
        {
            case "A":
                nextWholetone = 2;
                prevWholetone = 2;
                break;
            case "B":
                nextWholetone = 1;
                prevWholetone = 2;
                break;
            case "C":
                nextWholetone = 2;
                prevWholetone = 1;
                break;
            case "D":
                nextWholetone = 2;
                prevWholetone = 2;
                break;
            case "E":
                nextWholetone = 1;
                prevWholetone = 2;
                break;
            case "F":
                nextWholetone = 2;
                prevWholetone = 1;
                break;
            case "G":
                nextWholetone = 2;
                prevWholetone = 2;
                break;
            default:
                break;
        }
    }

    public bool IsSameNote_IgnorePitch( Note n )
    {
        return ( this.name == n.name );
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

        return name + pitchMod;
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
            return ( this.name == n.name && this.pitch == n.pitch );            
        }
    }
}
