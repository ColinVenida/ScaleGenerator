using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScale 
{
    public string rootNote { get; }
    private Note rootNoteObject;

    private Dictionary<string, string> notesInScale;
    //private Dictionary<string, Note> notesInScale;

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

        notesInScale = new Dictionary<string, string>();
        //notesInScale = new Dictionary<string, Note>();

        PopulateNotesInScale();
        AdjustSharpsOrFlats( formula );
        PrintNotesInScale();
    }

    private void PopulateNotesInScale()
    {
        const int SCALE_LENGTH_NO_ROOT = 7;

        //add the notes in the correct order
        notesInScale.Add( "1", rootNote );
        //notesInScale.Add( "1", rootNoteObject );
        string next = FindNextNote( rootNote );

        for ( int i = 2; i <= SCALE_LENGTH_NO_ROOT; i++ )
        {
            notesInScale.Add( i.ToString(), next );
            next = FindNextNote( next );
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
        char firstChar = note[0];
        if( note.Length > 1 )
        {
            //sharp/flat function here
        }

        switch ( firstChar )
        {
            case 'A':
                next = "B";
                break;
            case 'B':
                next = "C";
                break;
            case 'C':
                next = "D";
                break;
            case 'D':
                next = "E";
                break;
            case 'E':
                next = "F";
                break;
            case 'F':
                next = "G";
                break;
            case 'G':
                next = "A";
                break;
            default:
                next = "C";
                break;
        }
        return next;
    }

    //private Note FindNextNote( Note note )
    //{
    //    Note nextNote;
    //    string next;
    //    char firstChar = note.ToString()[0];
    //    //if ( note.Length > 1 )
    //    //{
    //    //    //sharp/flat function here
    //    //}

    //    switch ( firstChar )
    //    {
    //        case 'A':
    //            next = "B";
    //            break;
    //        case 'B':
    //            next = "C";
    //            break;
    //        case 'C':
    //            next = "D";
    //            break;
    //        case 'D':
    //            next = "E";
    //            break;
    //        case 'E':
    //            next = "F";
    //            break;
    //        case 'F':
    //            next = "G";
    //            break;
    //        case 'G':
    //            next = "A";
    //            break;
    //        default:
    //            next = "C";
    //            break;
    //    }
    //    nextNote = new Note( next );
    //    return nextNote;
    //}

    //function to handle sharp/flat quality here
    private void AdjustSharpsOrFlats( ScaleFormulas.ScaleFormula formula )
    {
        //natrualIntervals["Note"] gives int; the distance between natural notes in music
        //forumla.intervalDistance["interval"] gives int; distance between each of the scale's intervals

        string note = rootNote;
        int natIntervals = 0;
        int formulaIntervals = 0;
        for( int i = 1; i < formula.intervalDistances.Count + 1; i++ )
        {
            //add up the intervals in the formula, and then compare to the intervals in the natural notes
            //any differences between the totals means the NEXT note needs to be sharp/flat
            Debug.Log( "note = " + note );
            natIntervals += naturalIntervals[note];
            formulaIntervals += formula.intervalDistances[i.ToString()];
            
            
            note = FindNextNote( note );
            if( formulaIntervals > natIntervals )
            {
                //next note needs to be sharp
                //Debug.Log( note + " needs to be SHARP in " + rootNote + " " + formula.scaleName );
               

            }
            else if ( formulaIntervals < natIntervals )
            {
                //next note needs to be flat
                //Debug.Log( note + " needs to be FLAT in " + rootNote + " " + formula.scaleName );
                
            }
            else
            {
                //Debug.Log( note + " needs to be NATURAL in " + rootNote + " " + formula.scaleName );
            }            
        }
    }

}
