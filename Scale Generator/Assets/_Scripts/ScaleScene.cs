using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleScene : MonoBehaviour 
{
    public Text[] notePositions;
    public Text[] displayText;
    public ScaleGenerator scaleGen;
    public NoteArray noteArray;

    public Dropdown scaleDrop;
    public Dropdown rootDrop;

    private bool isInitialized;

    public void UpdateScale()
    {
        //the scale will not update until the dropdowns have properly updated, see the Start() function
        if( !isInitialized )
        {
            return;
        }

        scaleGen.GenerateScale( rootDrop.value, scaleDrop.value, noteArray );        

        //**find the starting point of the scale here***
        int familyIndex = scaleGen.FindFamilyIndex( rootDrop.value );

        for ( int i = 0; i < 8; i++ )
        {            
            displayText[i].text = noteArray.noteArray[familyIndex].GetNote();
            familyIndex++;
            if (familyIndex > 6)
            {
                familyIndex = 0;
            }
        }
        UpdateDisplay();
        SavePlayerPrefs();
    }

    public void UpdateDisplay()
    {
        //add minor/diminished
        switch (scaleDrop.value)
        {
            case 0: //ionian
            case 4: //lydian
            case 5: //mixolydian
                displayText[1].text += "m";
                displayText[2].text += "m";
                displayText[5].text += "m";
                displayText[6].text += "dim";
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                displayText[0].text += "m";
                displayText[1].text += "dim";
                displayText[3].text += "m";
                displayText[4].text += "m";
                displayText[7].text += "m";
                break;
        }
        UpdatePositions( scaleDrop );
    }

    public void UpdatePositions( Dropdown scale )
    {
        switch (scale.value)
        {
            //'\u2205' is for half-diminished symbol
            //unicode symbols found here https://www.fileformat.info/info/unicode/font/arial_unicode_ms/list.htm
            case 0: //ionian
            case 4: //lydian
            case 5: //mixolydian
                notePositions[0].text = "I";
                notePositions[1].text = "ii";
                notePositions[2].text = "iii";
                notePositions[3].text = "IV";
                notePositions[4].text = "V";
                notePositions[5].text = "vi";
                notePositions[6].text = "vii" + '\u2205';
                notePositions[7].text = "I";
                break;
            case 1: //aeolian
            case 2: //dorian
            case 3: //phrygian
            case 6: //locrian
                notePositions[0].text = "i";
                notePositions[1].text = "ii" + '\u2205';
                notePositions[2].text = "III";
                notePositions[3].text = "iv";
                notePositions[4].text = "v";
                notePositions[5].text = "VI";
                notePositions[6].text = "VII";
                notePositions[7].text = "i";
                break;
        }
    }
    
    private void SavePlayerPrefs()
    {        
        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
        PlayerPrefs.SetInt( "rootNote", rootDrop.value );
        PlayerPrefs.Save();
    }

    // Use this for initialization
    void Start()
    {
        scaleDrop.value = PlayerPrefs.GetInt( "scaleType" );  //OnValueChanged Event, which updates the scale, gets triggered                                                                 
        rootDrop.value = PlayerPrefs.GetInt( "rootNote" );      //every time you change the either drop value

        //now that the dropdowns have been properly updated, we can update the scale
        isInitialized = true;
        UpdateScale();
    }

    private void Awake()
    {
        //check and set the Playerprefs
        //set the default scale/root dropdowns to Major C
        if (!PlayerPrefs.HasKey( "scaleType" ))
        {
            PlayerPrefs.SetInt( "scaleType", 0 );
        }

        if (!PlayerPrefs.HasKey( "rootNote" ))
        {
            PlayerPrefs.SetInt( "rootNote", 5 );
        }

        //we don't want to update the scale pre-maturely, so we set a bool to false
        //the bool is set to true after BOTH the scaleDrop and rootDrop values have been changed
        isInitialized = false;        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
