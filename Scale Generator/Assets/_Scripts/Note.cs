using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note 
{
    //public char id;
    private string id;
    public int index;
    public int nextWholetone;
    public int prevWholetone;
    public bool usedSharp;
    public bool usedFlat;
    public bool doubleSharp;
    public bool doubleFlat;

    //constructor for a blank note
    public Note ()
    {
        id = "";
        index = 0;
        nextWholetone = 0;
        prevWholetone = 0;
        usedSharp = false;
        usedFlat = false;
        doubleSharp = false;
        doubleFlat = false;
    }

    public Note( string name, int ind, int next, int prev)
    {
        id = name;
        index = ind;
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
                return id + "##";
            }
            else 
            {
                return id + "#";
            }            
        }
        else if ( usedFlat )
        {
            if ( doubleFlat )
            {
                return id + "bb";
            }
            else
            {
                return id + "b";
            }
        }
        else
        {
            return id;
        }
    }

    //function to change the note's id (ie. A, B, C) and then calls the ChangePrevNext function
    public void SetId( string name )
    {
        id = name;
        ChangePrevNext( id );
    }

    //function that updates the nextWholetone/prevWholetone according to he given id
    private void ChangePrevNext( string id )
    {
        switch ( id )
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

    // Use this for initialization
    void Start () 
	{
        
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
