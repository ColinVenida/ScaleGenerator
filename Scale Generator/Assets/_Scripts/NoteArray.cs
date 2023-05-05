using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NoteArray is a data structure that holds all the notes to be referenced by the ScaleGenerator class
//this class will soon be deprecated
public class NoteArray : MonoBehaviour
{

    public Note[] noteArray = new Note[7];

    private void Awake ()
    {
        //int whole = ToneTypes.WHOLE_TONE;
        //int semi = ToneTypes.SEMI_TONE;

        ////populate the noteArray 
        //noteArray[0] = new Note( "A", whole, whole );
        //noteArray[1] = new Note( "B", semi, whole );
        //noteArray[2] = new Note( "C", whole, semi );
        //noteArray[3] = new Note( "D", whole, whole );
        //noteArray[4] = new Note( "E", semi, whole );
        //noteArray[5] = new Note( "F", whole, semi );
        //noteArray[6] = new Note( "G", whole, whole );
    }
}
