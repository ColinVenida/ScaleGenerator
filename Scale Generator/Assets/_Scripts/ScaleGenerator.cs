
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
    private int rootIndex;  //variable that represents which index the root note is in

    public Text theoreticalWarning;

    
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

    // the root and scale values come from the "root" and "scale" dropdown objects in the Scale/Fretboard Scenes
            //function changes the given noteArray reference
    public void GenerateScale( int root, int scale, NoteArray noteArray )
    {
        int[] diatonicPattern = { 2, 1, 2, 2, 1, 2, 2 }; //an int[] that represents the intervals of all the notes
                                                         //each index represents the amount of semitones between the notes, starting with A
                                                         //ie. index 0 = A, and is 2 semitones away from B (index 1)
        int startIndex = FindFamilyIndex( root );
        rootIndex = startIndex;
        int scaleTotal = 0;         //an int that represents the amount of semitones in the given scale
        int diatonicTotal = 0;          //an int that represents the amount of semitones in the diatonicPattern;
        

        //clear the existing sharps/flats/doubles
        //reset the sharp/flat booleans
        for (int i = 0; i < 7; i++)
        {
            noteArray.noteArray[i].usedFlat = false;
            noteArray.noteArray[i].doubleFlat = false;
            noteArray.noteArray[i].usedSharp = false;
            noteArray.noteArray[i].doubleSharp = false;
        }

        theoreticalWarning.gameObject.SetActive( false );

        //adjusting the scaleTotal and usedSharp/Flat in case it starts on a sharp/flat note
        switch ( root )
        {
            //cases for flats
            case 0:
            case 3:
            case 7:
            case 10:
            case 14:
                scaleTotal -= 1;
                noteArray.noteArray[startIndex].usedFlat = true;
                //flatsTotal++;
                break;
            //cases for sharps
            case 2:
            case 6:
            case 9:
            case 13:
            case 16:
                scaleTotal += 1;
                noteArray.noteArray[startIndex].usedSharp = true;
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
            int noteIndex = 0;
            scaleTotal += currentScale[i];
            diatonicTotal += diatonicPattern[startIndex];

            if( startIndex == 6)
            {
                noteIndex = 0;
            }
            else
            {
                noteIndex = startIndex + 1;    
            }

            //compare here            
            if( scaleTotal > diatonicTotal )
            {                
                noteArray.noteArray[noteIndex].usedSharp = true;
                if ( (scaleTotal - diatonicTotal) > 1 )
                {
                    noteArray.noteArray[noteIndex].doubleSharp = true;
                    theoreticalWarning.gameObject.SetActive( true );                    
                }
            }
            else if ( scaleTotal < diatonicTotal )
            {                
                noteArray.noteArray[noteIndex].usedFlat = true;
                if ( (scaleTotal - diatonicTotal) < -1)
                {
                    noteArray.noteArray[noteIndex].doubleFlat = true;
                    theoreticalWarning.gameObject.SetActive( true );                    
                }
            }
                        
            //increment and check array bounds
            startIndex++;
            if( startIndex > 6)
            {
                startIndex = 0;
            }
        }       
    }

    public int[] GetMajorFormula()
    {
        return majorFormula;
    }

    public int[] GetMinorFormula()
    {
        return minorFormula;
    }

    public int GetRootIndex()
    {
        return rootIndex;
    }

}
