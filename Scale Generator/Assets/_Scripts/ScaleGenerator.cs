
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScaleGenerator : MonoBehaviour
{    
    private int[] majorFormula = { 0, 2, 2, 1, 2, 2, 2, 1 };
    private int[] minorFormula = { 0, 2, 1, 2, 2, 1, 2, 2 };
    private int rootNote = 3;
    private bool isTheoretical;

    public Text theoreticalWarning;

    // the root and scale values come from the "root" and "scale" dropdown objects in the Scale/Fretboard Scenes
    //function changes the given noteArray reference
    public void GenerateScale( int root, int scale, NoteArray noteArray )
    {
        //rootNote is also set here incase this method is called before the
        //Awake() method finishes (yes it happens)
        //                              -OnValueChanged event fires when scaleDrop is set during Awake() method

        //check if scale is theoretical
        if ( CheckTheoretical( root, scale ) )
        {
            theoreticalWarning.gameObject.SetActive( true );
        }
        else
        {
            theoreticalWarning.gameObject.SetActive( false );
        }

        //reset the sharp/flat booleans
        for (int i = 0; i < 7; i++)
        {
            noteArray.noteArray[i].usedFlat = false;
            noteArray.noteArray[i].usedSharp = false;
        }

        //set the first value to the root note
        int note = root;
 
        //find the familyIndex of the root note.  ie. is it an A, B, C, etc.
        int familyIndex = FindFamilyIndex ( note );

        //adjusting the sharp/flat variable of the first note
        switch (note)
        {
            //cases for flats
            case 0:
            case 3:
            case 7:
            case 10:
            case 14:
                noteArray.noteArray[familyIndex].usedFlat = true;
                break;
            //cases for sharps
            case 2:
            case 6:
            case 9:
            case 13:
            case 16:
                noteArray.noteArray[familyIndex].usedSharp = true;
                break;
        }

        //calculate the rest of the scale based on the root note and scale formula
        int[] currentScale;

        switch ( scale )
        {
            case 0:
                currentScale = majorFormula;
                break;
            case 1:
                currentScale = minorFormula;
                break;
            default:
                Debug.Log( "Default Scale [MAJOR] has been used" );
                currentScale = majorFormula;
                break;
        }

        //move to the next note to calculate
        familyIndex++;
        if (familyIndex > 6)
        {
            familyIndex = 0;
        }

        //generate the rest of the scale including sharps/flats based on the note before
        for (int j = 1; j < 7; j++)
        {

            //determine whether the next note is sharp/flat     

            if (currentScale[j] == 2) //if the next note in scale pattern is two frets away
            {
                if (familyIndex == 0)  //check for array bounds
                {
                    //if used sharp
                    if (noteArray.noteArray[6].usedSharp)
                    {
                        noteArray.noteArray[familyIndex].usedSharp = true;
                    }
                    else if (noteArray.noteArray[6].usedFlat) //if used flat
                    {
                        noteArray.noteArray[familyIndex].usedFlat = true;
                    }

                }
                else
                {
                    //check if the next tone family is 1 fret away (ie. B->C, E->F)
                    if (noteArray.noteArray[familyIndex - 1].nextWholetone == 1)
                    {
                        if (!noteArray.noteArray[familyIndex - 1].usedFlat && !noteArray.noteArray[familyIndex - 1].usedSharp)
                        {
                            noteArray.noteArray[familyIndex].usedSharp = true;
                        }
                        //if previous note used a flat, then leave the next note alone
                    }
                    //in these cases, the next tone family is two frets away
                    //if used sharp
                    else if (noteArray.noteArray[familyIndex - 1].usedSharp)
                    {
                        noteArray.noteArray[familyIndex].usedSharp = true;
                    }
                    else if (noteArray.noteArray[familyIndex - 1].usedFlat) //if used flat
                    {
                        noteArray.noteArray[familyIndex].usedFlat = true;
                    }
                }
            }// end ** if (currentScale[j] == 2) **


            if (currentScale[j] == 1) //if the next note in scale pattern is one fret away
            {
                if (familyIndex == 0) //check array bounds
                {
                    //if previous note is natural, add flat to next note
                    if (!noteArray.noteArray[6].usedSharp && !noteArray.noteArray[6].usedFlat)
                    {
                        noteArray.noteArray[familyIndex].usedFlat = true;
                    }
                }
                else
                {
                    if (noteArray.noteArray[familyIndex - 1].nextWholetone == 2)
                    {
                        //if prev. used natural, add flat
                        if (!noteArray.noteArray[familyIndex - 1].usedSharp && !noteArray.noteArray[familyIndex - 1].usedFlat)
                        {
                            noteArray.noteArray[familyIndex].usedFlat = true;
                        }

                        //if prev. used sharp, then keep next natural
                        //if prev. note used flat, then it's theoretical scale
                    }
                    else   //next wholeTone == 1
                    {
                        noteArray.noteArray[familyIndex].usedSharp = noteArray.noteArray[familyIndex - 1].usedSharp;
                        noteArray.noteArray[familyIndex].usedFlat = noteArray.noteArray[familyIndex - 1].usedFlat;
                    }
                }
            }// end  ** if (currentScale[j] == 1) **

            familyIndex++;
            if (familyIndex > 6)
            {
                familyIndex = 0;
            }
        }                   
    }
    
    public int FindFamilyIndex ( int note )
    {
        int index = 0;

        switch (note)
        {
            case 0:
            case 1:
            case 2:
                index = 0; //set to A
                break;
            case 3:
            case 4:
                index = 1; //set to B
                break;
            case 5:
            case 6:
                index = 2; //set to C
                break;
            case 7:
            case 8:
            case 9:
                index = 3; //set to D
                break;
            case 10:
            case 11:
                index = 4; //set to E
                break;
            case 12:
            case 13:
                index = 5; //set to F
                break;
            case 14:
            case 15:
            case 16:
                index = 6; //set to G
                break;
        }

        return index;
    }

    private bool CheckTheoretical( int root, int scale )
    {
        bool theoretical = false;
        int[] diatonicPattern = { 2, 1, 2, 2, 1, 2, 2 }; //an int[] that represents the intervals of all the notes
                                                         //each index represents the amount of semitones between the notes, starting with A
                                                         //ie. index 0 = A, and is 2 semitones away from B (index 1)
        int startIndex = FindFamilyIndex( root );
        int scaleTotal = 0;         //an int that represents the amount of semitones in the given scale
        int diatonicTotal = 0;          //an int that represents the amount of semitones in the diatonicPattern;
        //int sharpsTotal = 0;
        //int flatsTotal = 0;

        //adjusting the scaleTotal in case it starts on a sharp/flat note
        switch ( root )
        {
            //cases for flats
            case 0:
            case 3:
            case 7:
            case 10:
            case 14:
                scaleTotal -= 1;
                //flatsTotal++;
                break;
            //cases for sharps
            case 2:
            case 6:
            case 9:
            case 13:
            case 16:
                scaleTotal += 1;
                //sharpsTotal++;
                break;
        }

        int[] currentScale;
        
        switch( scale )
        {
            case 0:
                currentScale = majorFormula;
                break;
            case 1:
                currentScale = minorFormula;
                break;
            default:
                currentScale = majorFormula;
                break;
        }

        

        //add the semitones of the diatonic pattern and the given scale, then compare them
                    //any difference between the totals will represent a sharp or a flat note in the scale
        for ( int i = 1; i < 7; i++ )
        {
            scaleTotal += currentScale[i];
            diatonicTotal += diatonicPattern[startIndex];

            //compare here
            //Debug.Log( "iteration " + i + ": scaleTotal = " + scaleTotal + ", diatonicTotal = " + diatonicTotal );
            if( scaleTotal > diatonicTotal )
            {
                //sharpsTotal++;
                if ( (scaleTotal - diatonicTotal) > 1 )
                {
                    Debug.Log( "Theoretical Scale!!!! double sharp" );
                    theoretical = true;
                }
            }
            else if ( scaleTotal < diatonicTotal )
            {
                //flatsTotal++;
                if ( (scaleTotal - diatonicTotal) < -1)
                {
                    Debug.Log( "Theoretical Scale!!!! double flat" );
                    theoretical = true;
                }
            }

            //increment and check array bounds
            startIndex++;
            if( startIndex > 6)
            {
                startIndex = 0;
            }
        }

        //Debug.Log( "sharpsTotal = " + sharpsTotal );
        //Debug.Log( "flatsTotal = " + flatsTotal );

        return theoretical;
    }

    public int[] GetMajorFormula()
    {
        return majorFormula;
    }

    public int[] GetMinorFormula()
    {
        return minorFormula;
    }
   
}
