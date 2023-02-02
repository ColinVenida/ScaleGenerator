using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NoteArray is a data structure that holds all the notes to be referenced by the ScaleGenerator class
public class NoteArray : MonoBehaviour
{

    public Note[] noteArray = new Note[7];

    private void Awake ()
    {
        int whole = (int) ToneType.WHOLE_TONE;
        int semi = ( int )ToneType.SEMI_TONE;

        //populate the noteArray 
        noteArray[0] = new Note( "A", 0, whole, whole );
        noteArray[1] = new Note( "B", 0, semi, whole );
        noteArray[2] = new Note( "C", 0, whole, semi );
        noteArray[3] = new Note( "D", 0, whole, whole );
        noteArray[4] = new Note( "E", 0, semi, whole );
        noteArray[5] = new Note( "F", 0, whole, semi );
        noteArray[6] = new Note( "G", 0, whole, whole );

    }
}
