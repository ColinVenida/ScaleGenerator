using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note 
{
    private static Dictionary<int, string> noteCodes;
    private static Dictionary<int, string> wholeNoteCodes;

    //public char id;
    private string id;
    public int index;
    public int nextWholetone;
    public int prevWholetone;
    public bool usedSharp;
    public bool usedFlat;


    public Note( string name, int ind, int next, int prev)
    {
        id = name;
        index = ind;
        nextWholetone = next;
        prevWholetone = prev;
        usedSharp = false;
        usedFlat = false;
    }

    public string GetNote ()
    {
        if( usedSharp )
        {
            return id + "#";
        }
        else if ( usedFlat )
        {
            return id + "b";
        }
        else
        {
            return id;
        }
    }


    // Use this for initialization
    void Start () 
	{
        
    }

    private void Awake ()
    {
        noteCodes = new Dictionary<int, string>();
        wholeNoteCodes = new Dictionary<int, string>();

        //set the key/values of the dictionary
        //noteCodes.Add( 0, "A" );
        //noteCodes.Add( 1, "A#/Bb" );
        //noteCodes.Add( 2, "B" );
        //noteCodes.Add( 3, "C" );
        //noteCodes.Add( 4, "C#/Db" );
        //noteCodes.Add( 5, "D" );
        //noteCodes.Add( 6, "D#/Eb" );
        //noteCodes.Add( 7, "E" );
        //noteCodes.Add( 8, "F" );
        //noteCodes.Add( 9, "F#/Gb" );
        //noteCodes.Add( 10, "G" );
        //noteCodes.Add( 11, "G#/Ab" );

        noteCodes.Add( 0, "Ab" );
        noteCodes.Add( 1, "A" );
        noteCodes.Add( 2, "A#" );

        noteCodes.Add( 3, "Bb" );
        noteCodes.Add( 4, "B" );
        noteCodes.Add( 5, "B#" );

        noteCodes.Add( 6, "Cb" );
        noteCodes.Add( 7, "C" );
        noteCodes.Add( 8, "C#" );

        noteCodes.Add( 9, "Db" );
        noteCodes.Add( 10, "D" );
        noteCodes.Add( 11, "D#" );

        noteCodes.Add( 12, "Eb" );
        noteCodes.Add( 13, "E" );
        noteCodes.Add( 14, "E#" );

        noteCodes.Add( 15, "Fb" );
        noteCodes.Add( 16, "F" );
        noteCodes.Add( 17, "F#" );

        noteCodes.Add( 18, "Gb" );
        noteCodes.Add( 19, "G" );
        noteCodes.Add( 20, "G#" );


        wholeNoteCodes.Add( 0, "A" );
        wholeNoteCodes.Add( 2, "B" );
        wholeNoteCodes.Add( 3, "C" );
        wholeNoteCodes.Add( 5, "D" );
        wholeNoteCodes.Add( 7, "E" );
        wholeNoteCodes.Add( 8, "F" );
        wholeNoteCodes.Add( 10, "G" );
    }

    //TODO:  Refactor getting note codes to correctly edit the sharps and flats in the given scale
    //public string GetNote( int code, int root )
    //{        
    //    return noteCodes[code];
    //}


    public Dictionary<int, string> GetNoteCodes ()
    {
        return noteCodes;
    }

    public Dictionary<int, string> GetWholeNotes ()
    {
        return wholeNoteCodes;       
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
