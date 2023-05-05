
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//***CLASS IS BEING DEPRECATED***
public class ScaleGenerator : MonoBehaviour
{
    //formulas: [nothing, distance between root->2nd, distance between 2nd->3rd, 3rd->4th, ... ]
    private int[] majorFormula = { 0, 2, 2, 1, 2, 2, 2, 1 };
    private int[] minorFormula = { 0, 2, 1, 2, 2, 1, 2, 2 };
    private int[] dorianFormula = { 0, 2, 1, 2, 2, 2, 1, 2 };
    private int[] phrygianFormula = { 0, 1, 2, 2, 2, 1, 2, 2 };
    private int[] lydianFormula = { 0, 2, 2, 2, 1, 2, 2, 1 };
    private int[] mixolydianFormula = { 0, 2, 2, 1, 2, 2, 1, 2 };
    private int[] locrianFormula = { 0, 1, 2, 2, 1, 2, 2, 2 };

    private Dictionary<string, int> natrualIntervals = new Dictionary<string, int>()
    {
        {"A", 2 },
        {"B", 1 },
        {"C", 2 },
        {"D", 2 },
        {"E", 1 },
        {"F", 2 },
        {"G", 2 },
    };

    private int[] diatonicPattern = { 2, 1, 2, 2, 1, 2, 2 }; //an int[] that represents the intervals of all the notes
                                                     //each index represents the amount of semitones between the notes, starting with A
                                                     //ie. index 0 = A, and is 2 semitones away from B (index 1)

    private int rootNote = 3;
    private bool isTheoretical;
    private int rootIndex;  //variable that represents which index the root note is in

    public Text theoreticalWarning;

    public int FindFamilyIndex( int note )
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

    public string FindFamilyIndex_string( int note )
    {
        string noteName = "";
        switch ( note )
        {
            case 0:
            case 1:
            case 2:
                noteName = "A";
                break;
            case 3:
            case 4:
                noteName = "B";
                break;
            case 5:
            case 6:
                noteName = "C";
                break;
            case 7:
            case 8:
            case 9:
                noteName = "D";
                break;
            case 10:
            case 11:
                noteName = "E";
                break;
            case 12:
            case 13:
                noteName = "F";
                break;
            case 14:
            case 15:
            case 16:
                noteName = "G";
                break;
        }
        return noteName;
    }

    // the root and scale values come from the "root" and "scale" dropdown objects in the Scale/Fretboard Scenes
    //function changes the given noteArray reference
    public void GenerateScale( int root, int scale, NoteArray noteArray )
    {        
        int startIndex = FindFamilyIndex( root );
        rootIndex = startIndex;       
       
        int totalSemitonesIn_scale = 0;
        int totalSemitonesIn_daitonic = 0;
        int[] currentScale = GetScaleFormula( scale );

        ResetSharpsAndFlats( noteArray );
                
        //adjusting the scaleTotal and usedSharp/Flat in case the scale starts on a sharp/flat note
        switch (root)
        {
            //cases for flat root notes
            case NoteValues.A_flat:
            case NoteValues.B_flat:
            case NoteValues.D_flat:
            case NoteValues.E_flat:
            case NoteValues.G_flat:
                totalSemitonesIn_scale -= 1;
                noteArray.noteArray[startIndex].usedFlat = true;                
                break;
            //cases for sharp root notes
            case NoteValues.A_sharp:
            case NoteValues.C_sharp:
            case NoteValues.D_sharp:
            case NoteValues.F_sharp:
            case NoteValues.G_sharp:
                totalSemitonesIn_scale += 1;
                noteArray.noteArray[startIndex].usedSharp = true;                
                break;
        }

        //add the semitones of the diatonic pattern and the selected scale, then compare them
            //any difference between the totals will represent a sharp or a flat note in the scale
        for (int i = 1; i < diatonicPattern.Length; i++)
        {
            int noteIndex = 0;
            totalSemitonesIn_scale += currentScale[i];
            totalSemitonesIn_daitonic += diatonicPattern[startIndex];

            if (startIndex == 6)
            {
                noteIndex = 0;
            }
            else
            {
                noteIndex = startIndex + 1;
            }

            //compare here, check for sharps/flats            
            if ( totalSemitonesIn_scale > totalSemitonesIn_daitonic )
            {
                noteArray.noteArray[noteIndex].usedSharp = true;
                if (( totalSemitonesIn_scale - totalSemitonesIn_daitonic ) > 1)
                {
                    noteArray.noteArray[noteIndex].doubleSharp = true;
                    theoreticalWarning.gameObject.SetActive( true );
                }
            }
            else if ( totalSemitonesIn_scale < totalSemitonesIn_daitonic )
            {
                noteArray.noteArray[noteIndex].usedFlat = true;
                if (( totalSemitonesIn_scale - totalSemitonesIn_daitonic ) < -1)
                {
                    noteArray.noteArray[noteIndex].doubleFlat = true;
                    theoreticalWarning.gameObject.SetActive( true );
                }
            }

            //increment and check array bounds
            startIndex++;
            if (startIndex > 6)
            {
                startIndex = 0;
            }
        }
    }

    private void ResetSharpsAndFlats( NoteArray nArray )
    {
        for ( int i = 0; i < 7; i++ )
        {
            nArray.noteArray[i].usedFlat = false;
            nArray.noteArray[i].doubleFlat = false;
            nArray.noteArray[i].usedSharp = false;
            nArray.noteArray[i].doubleSharp = false;
        }

        theoreticalWarning.gameObject.SetActive( false );
    }

    private int[] GetScaleFormula( int scaleDropValue )
    {
        switch ( scaleDropValue )
        {
            case 0:
                return majorFormula;                
            case 1:
                return minorFormula;                
            case 2:
                return dorianFormula;                
            case 3:
                return phrygianFormula;                
            case 4:
                return lydianFormula;                
            case 5:
                return mixolydianFormula;                
            case 6:
                return locrianFormula;                
            default:
                return majorFormula;                
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

    public int[] GetDorianFormula()
    {
        return dorianFormula;
    }

    public int[] GetPhrygianFormula()
    {
        return phrygianFormula;
    }

    public int[] GetLydianFormula()
    {
        return lydianFormula;
    }

    public int[] GetMixolydianFormula()
    {
        return mixolydianFormula;
    }

    public int[] GetLocrianFormula()
    {
        return locrianFormula;
    }

    public int GetRootIndex()
    {
        return rootIndex;
    }

}
