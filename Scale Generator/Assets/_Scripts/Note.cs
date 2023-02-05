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
        name = n;
        nextWholetone = 0;
        prevWholetone = 0;
        pitch = PitchModifier.Natural;
        
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    public Note( string n, PitchModifier p )
    {
        name = n;
        pitch = p;
    }

    public Note( string n, int next, int prev)
    {
        name = n;        
        nextWholetone = next;
        prevWholetone = prev;
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
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

    public override string ToString()
    {
        return GetNote();
    }
}
