using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuitString : MonoBehaviour
{
    public Fretboard fBoard;
    public Note notes;
    public NoteArray noteArray;
    public Dropdown noteSelect;
    public Text[] fretArray;
    public Dropdown presetDrop;
    public int stringNumber;

    public static bool changePreset = true;  //variable for determining if the Preset Dropdown should be reset or not


    //TODO:  REFACTOR the notes being calculated based on dropdown values of a given rootNote and scaleType
    public void CalculateFrets( int drop )
    {
        bool isEharmonic = false;

        //if the dropdown menu is the blank
        if (drop == 12)
        {
            Debug.Log( "drop 12, ending CalculateFrets()" );
            return;
        }

        //based on the dropdown value, change the text of the fretArray elements


        //set the preset dropdown to default value (0) if the tuning was changed by the preset button
        if (changePreset)
        {
            presetDrop.value = 0;
        }

        SaveTuning( drop );
        int currentNote = drop;
        int currentFret = 0;

        //blank out the fret board
        for (int i = 0; i < fretArray.Length; i++)
        {
            fretArray[i].text = "";
        }

        //          *******CURRENT TASK******
        //calculate the frets based on the GuitString tuning and current scale in the Fretboard

        //run through the fret board and set the labels to the scale
        Debug.Log( "string " + this.stringNumber + " noteSelect.value = " + noteSelect.value );

        //find the starting position in the scale
        //check whether the noteSelect valu0e is equal to any note in the scale
                //******idea*****
                //convert the currentScale[] into an int[] then check the values
                    //******OR******
                    //have the generateScale return a Note[] instead of a string[] so we can use the sharp/flat data in other parts of the program

        //check whether the GuitString's tuning is eharmonic to any note in the scale
        //ie. if the GuitString's tuning is A#, check if there is a Bb note in the scale

        //values come from all the eharmonic notes that the user can select from rootDrop 
        switch (drop)
        {
            case 0:     //Ab
            case 2:     //A#
            case 3:     //Bb
            case 6:     //C#
            case 7:     //Db
            case 9:     //D#
            case 10:    //Eb
            case 13:    //F#
            case 14:    //Gb
            case 16:    //G#
                isEharmonic = true;
                break;
            default:
                break;
        }

        //find the starting point of the scale.  Which note should be displayed first?
        int familyIndex = 0;

        //find the familyIndex
        switch (drop)
        {
            case 0:
            case 1:
            case 2:
                familyIndex = 0; //set to A
                break;
            case 3:
            case 4:
                familyIndex = 1; //set to B
                break;
            case 5:
            case 6:
                familyIndex = 2; //set to C
                break;
            case 7:
            case 8:
            case 9:
                familyIndex = 3; //set to D
                break;
            case 10:
            case 11:
                familyIndex = 4; //set to E
                break;
            case 12:
            case 13:
                familyIndex = 5; //set to F
                break;
            case 14:
            case 15:
            case 16:
                familyIndex = 6; //set to G
                break;
        }



        // *************old code**************
        ////run through the fret board        
        //while (currentFret <= 12) 
        //{

        //    // ****OLD METHOD****

        //    //calculate the next note
        //    switch (currentNote)
        //    {
        //        //in the case where the next whole note is 2 frets away
        //        case 0:
        //        case 3:
        //        case 5:
        //        case 8:
        //        case 10:
        //            currentFret += 2;
        //            currentNote += 2;

        //            break;

        //        //in the case where the next whole note is 1 fret away
        //        case 1:
        //        case 2:
        //        case 4:
        //        case 6:
        //        case 7:
        //        case 9:
        //        case 11:
        //            currentFret += 1;
        //            currentNote += 1;
        //            break;
        //    }//end switch

        //    if (currentNote > 11)
        //    {
        //        currentNote = currentNote - 12;
        //    }

        //    //currentFret - 1 because fret 0 is NOT the open string
        //    //fretArray[0] is the first fret
        //    fretArray[currentFret - 1].text = notes.GetNoteCodes()[currentNote];

        //    
        //}//end while

        // ************* end old code ****************

    }

    //function that determines whether a given note is in a given scale
    private bool NoteInScale( int note, string[] scale )
    {
        
        return false;
    }

    private void SaveTuning( int drop )
    {        
        switch( this.stringNumber )
        {
            case 0:
                PlayerPrefs.SetInt( "GuitStringOne", drop );
                //Dbug.Log( "value = " + PlayerPrefs.GetInt( "GuitStringOne" ) );
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
    }


    // Use this for initialization
    void Start () 
	{
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
