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

    // Update is called once per frame
    void Update () 
	{
		
	}
}
