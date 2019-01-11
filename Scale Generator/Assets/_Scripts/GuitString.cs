﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuitString : MonoBehaviour 
{
    public Note notes;
    public NoteArray noteArray;
    public Dropdown noteSelect;
    public Text[] fretArray;
    public Dropdown presetDrop;
    public int stringNumber;

    public static bool changePreset = true;  //variable for determining if the Preset Dropdown should be reset or not


    //TODO:  REFACTOR the notes being calculated based on dropdown values of a given rootNote and scaleType
    public void CalculateFrets ( int drop )    
    {        
        //if the dropdown menu is the blank
        if ( drop == 12 )
        {
            Debug.Log( "drop 12, ending CalculateFrets()" );
            return;
        }

        //based on the dropdown value, change the text of the fretArray elements
        //to show all the whole notes on the fretboard

        //set the preset dropdown to default value (0) if the tuning was changed by the preset button
        if( changePreset )
        {
            presetDrop.value = 0;
        }

        SaveTuning( drop );
        int currentNote = drop;
        int currentFret = 0;              

        //blank out the fret board
        for ( int i = 0; i < fretArray.Length; i++ )
        {
            fretArray[i].text = "";
        }

        //run through the fret board        
        while (currentFret <= 12) 
        {

            // ****OLD METHOD****

            //calculate the next note
            switch (currentNote)
            {
                //in the case where the next whole note is 2 frets away
                case 0:
                case 3:
                case 5:
                case 8:
                case 10:
                    currentFret += 2;
                    currentNote += 2;

                    break;

                //in the case where the next whole note is 1 fret away
                case 1:
                case 2:
                case 4:
                case 6:
                case 7:
                case 9:
                case 11:
                    currentFret += 1;
                    currentNote += 1;
                    break;
            }//end switch

            if (currentNote > 11)
            {
                currentNote = currentNote - 12;
            }

            //currentFret - 1 because fret 0 is NOT the open string
            //fretArray[0] is the first fret
            fretArray[currentFret - 1].text = notes.GetNoteCodes()[currentNote];

            // *****************************

        }
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