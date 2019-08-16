using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuitString : MonoBehaviour
{
    public Fretboard fBoard;    
    public NoteArray noteArray;
    public Dropdown noteSelect;
    public Text[] fretArray;
    public Dropdown presetDrop;
    public int stringNumber; 

    public static bool changePreset = true;  //variable for determining if the Preset Dropdown should be reset or not

    private Note currentTuning;
        
    public void CalculateFrets( int drop )
    {
        bool hasEnharmonic = false;
        bool includeFamilyNote = false;        

        if( !fBoard.GetGuitStringInitialized() )
        {
            return;
        }

        //set the preset dropdown to default value (0) if the tuning was changed by the preset button
        if (changePreset)
        {
            presetDrop.value = 0;
        }
       
        SaveTuning( drop );       
        int currentFret = 0;   //set currentFret to 0 to represent the first fret

        //check which scale is selected
        int[] currentScale;
        switch( fBoard.scaleDrop.value )
        {
            case 0:
                currentScale = fBoard.scaleGen.GetMajorFormula();
                break;
            case 1:
                currentScale = fBoard.scaleGen.GetMinorFormula();
                break;
            case 2:
                currentScale = fBoard.scaleGen.GetDorianFormula();
                break;
            case 3:
                currentScale = fBoard.scaleGen.GetPhrygianFormula();
                break;
            case 4:
                currentScale = fBoard.scaleGen.GetLydianFormula();
                break;
            case 5:
                currentScale = fBoard.scaleGen.GetMixolydianFormula();
                break;
            case 6:
                currentScale = fBoard.scaleGen.GetLocrianFormula();
                break;
            default:
                Debug.Log( "default in the GuitString scale assignment" );
                currentScale = fBoard.scaleGen.GetMajorFormula();
                break;
        }

        //blank out the fret board and change font color to black
        for (int i = 0; i < fretArray.Length; i++)
        {
            fretArray[i].text = "";
            fretArray[i].color = new Color32( 0, 0, 0, 255 );
            fretArray[i].fontStyle = FontStyle.Normal;
        }
                
        SetCurrentTuning( drop );
   
        //check whether the GuitString's tuning is enharmonic with any note in the noteArray
        //ie. if the GuitString is set A#, and the scale has Bb in it
        hasEnharmonic = CheckEnharmonic( noteSelect.value );
        
        
        //find the family index of the GuitString's tuning
        //  the familyIndex will represent the open GuitString, then we can find the next fret        
        int familyIndex = fBoard.scaleGen.FindFamilyIndex( noteSelect.value );

        //find the interval of currentTuning in relation to the scale            
        int scaleInterval = FindScaleInterval( fBoard.rootDrop.value, familyIndex );

        //check if the note in the current scale is a different "version" than the currentTuning
        //ie. the tuning is C, and the note in the scale is C#
        if (currentTuning.usedSharp != noteArray.noteArray[familyIndex].usedSharp || 
                currentTuning.usedFlat != noteArray.noteArray[familyIndex].usedFlat )
        {            
            includeFamilyNote = true;
        }

       
        //***find out which fret to start on and which notes to display***
        //if we have to start on the note AFTER the tuning (ie. tuining is A, and we start on B)
        if ( !includeFamilyNote )
        {           
            familyIndex++;  //move the familyIndex to the next note
            scaleInterval++;    //move the scaleInterval to the next note
            if (scaleInterval > 7)
            {
                scaleInterval = 1;
            }            
            if( currentScale[scaleInterval] == 2 )
            {
                currentFret++;
            }
            
        }
        else if ( hasEnharmonic )
        {
            familyIndex += 2;
            scaleInterval += 2;
            currentFret++;            
        }
        //if we have to include another version of the currentTuning's note (ie. a sharp or a flat)
        else
        {
            if (currentTuning.usedSharp)
            {
                familyIndex++;
                scaleInterval++;
            }
            else if ( !currentTuning.usedFlat && noteArray.noteArray[familyIndex].usedFlat )  //ie. tuning is B, but scale has a Bb in it, like F major scale
            {
                familyIndex++;
                scaleInterval++;                
            }
            //if the tuning used a flat, but scale had a natural, then we don't have to adjust the familyIndex, or the scaleInterval
        }        

        //check for array bounds
        if (familyIndex > 6)
        {
            familyIndex = 0;
        }
        if (scaleInterval > 7)
        {
            scaleInterval = 1;
        }

        //run through the fret board and set the labels to the scale
        while( currentFret < 14 )
        {
            //check if the next note is the same as the root note of the scale
            if (familyIndex == fBoard.scaleGen.GetRootIndex())
            {
                fretArray[currentFret].color = new Color( 0, 0, 1 );
            }
            else
            {
                fretArray[currentFret].color = new Color( 0, 0, 0 );
            }

            //set the currentFret's text to the note in the scale

            //****check here for editing arpeggios****

            fretArray[currentFret].text = fBoard.noteArray.noteArray[familyIndex].GetNote();
            familyIndex++;
            scaleInterval++;

            //check for array bounds
            if (familyIndex > 6)
            {
                familyIndex = 0;
            }
            if (scaleInterval > 7)
            {
                scaleInterval = 1;
            }
            //move to to the next fret according to the pattern
            currentFret += currentScale[scaleInterval];

        }        
    }//end CacculateFrets()

    //function to check if the given note/tuning has enharmonic notes in the current scale
    private bool CheckEnharmonic( int tuning )
    {
        bool hasEnharmonic = false;
        //if the tuning is an enharmonic note, then check if its equivalent is in the scale

        switch ( tuning )
        {
            case 0:     // tuning is Ab, check for G#
                if (fBoard.noteArray.noteArray[6].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 16:    //  tuning is G#, check for Ab
                if (fBoard.noteArray.noteArray[0].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 2:     //  tuning is A#, check for Bb
                if (fBoard.noteArray.noteArray[1].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 3:     //  tuning is Bb, check for A#
                if (fBoard.noteArray.noteArray[0].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 6:     //  tuning is C#, check for Db
                if (fBoard.noteArray.noteArray[3].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 7:     //  tuning is Db, check for C#
                if (fBoard.noteArray.noteArray[2].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 9:     //  tuning is D#, check for Eb
                if (fBoard.noteArray.noteArray[4].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 10:    //  tuning is Eb, check for Db
                if (fBoard.noteArray.noteArray[3].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 13:    //  tuning is F#, check for Gb
                if (fBoard.noteArray.noteArray[6].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 14:    //  tuning is Gb, check for F#
                if (fBoard.noteArray.noteArray[5].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            default:
                break;
        }
        
        return hasEnharmonic;
    }

    //function that returns the root note's interval of the given family index
        //ie. what interval/position is "A" in the C scale (A is the 6th note in C scales)
    private int FindScaleInterval( int root, int familyIndex )
    {
        int interval = 0;        
        //find the FamilyIndex of the root note
        int rootFamily = fBoard.scaleGen.FindFamilyIndex( root );        
        
        do
        {
            rootFamily++;
            interval++;
            if (rootFamily > 6)
            {
                rootFamily = 0;
            }
        } while ( rootFamily != familyIndex );

        return interval;
    }

    //gray out the 2nd, 4th, and 6th notes of the scale
    //public void SetArpeggio( int famIndex )
    public void SetArpeggio( List<string> noteList )
    {       
        for ( int i = 0; i < fretArray.Length; i++ )
        {
            if ( noteList.Contains(fretArray[i].text) )
            {                
                fretArray[i].text = "";
            }
        }
    }

    private void SaveTuning( int drop )
    {
        switch( this.stringNumber )
        {
            case 0:
                PlayerPrefs.SetInt( "GuitStringOne", drop );                
                break;
            case 1:
                PlayerPrefs.SetInt( "GuitStringTwo", drop );
                break;
            case 2:
                PlayerPrefs.SetInt( "GuitStringThree", drop );
                break;
            case 3:
                PlayerPrefs.SetInt( "GuitStringFour", drop );
                break;
            case 4:
                PlayerPrefs.SetInt( "GuitStringFive", drop );
                break;
            case 5:                
                PlayerPrefs.SetInt( "GuitStringSix", drop );                
                break;
            case 6:               
                PlayerPrefs.SetInt( "GuitStringSeven", drop );                
                break;
            case 7:                
                PlayerPrefs.SetInt( "GuitStringEight", drop );               
                break;
        }
        PlayerPrefs.Save();
        
    }

    //function that changes the current tuning to the given int value
        //int value comes from the noteSelect dropdown
    private void SetCurrentTuning ( int dropValue )
    {
       
        //change the currentTuning's id
        switch ( dropValue )
        {
            case 0:
            case 1:
            case 2:
                currentTuning.SetId( "A" );
                break;
            case 3:                
            case 4:
                currentTuning.SetId( "B" );
                break;
            case 5:                
            case 6:
                currentTuning.SetId( "C" );                 
                break;
            case 7:                
            case 8:                
            case 9:                
                currentTuning.SetId( "D" );
                break;
            case 10:                
            case 11:                
                currentTuning.SetId( "E" );
                break;
            case 12:                
            case 13:                
                currentTuning.SetId( "F" );
                break;
            case 14:                
            case 15:                
            case 16:
                currentTuning.SetId( "G" );
                break;
            default:
                break;
        }

        //update the currentTuning's sharp/flat state
        switch ( dropValue )
        {
            //cases for flats
            case 0:
            case 3:
            case 7:
            case 10:
            case 14:
                currentTuning.usedSharp = false;
                currentTuning.usedFlat = true;
                break;

            //cases for sharps
            case 2:
            case 6:
            case 9:
            case 13:
            case 16:
                currentTuning.usedFlat = false;
                currentTuning.usedSharp = true;
                break;

            //case for natural
            default:
                currentTuning.usedSharp = false;
                currentTuning.usedFlat = false;
                break;
        }
    }

    // Use this for initialization
    void Start () 
	{
        
    }

    private void Awake()
    {
        currentTuning = new Note();
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
