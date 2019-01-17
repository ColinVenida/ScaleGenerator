using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteArray : MonoBehaviour
{

    public Note[] noteArray = new Note[7];

    private void Awake ()
    {
        //populate the noteArray 

        noteArray[0] = new Note( "A", 0, 2, 2 );
        noteArray[1] = new Note( "B", 0, 1, 2 );
        noteArray[2] = new Note( "C", 0, 2, 1 );
        noteArray[3] = new Note( "D", 0, 2, 2 );
        noteArray[4] = new Note( "E", 0, 1, 2 );
        noteArray[5] = new Note( "F", 0, 2, 1 );
        noteArray[6] = new Note( "G", 0, 2, 2 );

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
