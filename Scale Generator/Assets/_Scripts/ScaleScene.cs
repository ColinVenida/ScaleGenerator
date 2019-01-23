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

    public void UpdateScale(  )
    {
        //string[] scaleNew = scaleGen.GenerateScale( rootDrop.value, scaleDrop.value );
        scaleGen.GenerateScale( rootDrop.value, scaleDrop.value, noteArray );

        //**find the starting point of the scale here***
        int familyIndex = scaleGen.FindFamilyIndex( rootDrop.value );

        for ( int i = 0; i < 8; i++ )
        {
            //displayText[i].text = scaleNew[i];
            displayText[i].text = noteArray.noteArray[familyIndex].GetNote();
            familyIndex++;
            if (familyIndex > 6)
            {
                familyIndex = 0;
            }
        }
        UpdateDisplay();
    }

    public void UpdatePositions( Dropdown scale )
    {
        switch (scale.value)
        {
            case 0:
                notePositions[0].text = "I";
                notePositions[1].text = "ii";
                notePositions[2].text = "iii";
                notePositions[3].text = "IV";
                notePositions[4].text = "V";
                notePositions[5].text = "vi";
                notePositions[6].text = "vii";
                notePositions[7].text = "VIII";
                break;
            case 1:
                notePositions[0].text = "i";
                notePositions[1].text = "ii";
                notePositions[2].text = "III";
                notePositions[3].text = "iv";
                notePositions[4].text = "v";
                notePositions[5].text = "VI";
                notePositions[6].text = "VII";
                notePositions[7].text = "viii";
                break;
        }
    }


    public void UpdateDisplay()
    {
        UpdatePositions( scaleDrop );

        //add minor/diminished
        switch (scaleDrop.value)
        {
            case 0:
                displayText[1].text += "m";
                displayText[2].text += "m";
                displayText[5].text += "m";
                displayText[6].text += "dim";
                break;
            case 1:
                displayText[0].text += "m";
                displayText[1].text += "dim";
                displayText[3].text += "m";
                displayText[4].text += "m";
                displayText[7].text += "m";
                break;
        }
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
        PlayerPrefs.SetInt( "rootNote", rootDrop.value );
        PlayerPrefs.Save();

        Debug.Log( "Saving!" );
        //Debug.Log( "scaleType = " + PlayerPrefs.GetInt( "scaleType" ) );
        Debug.Log( "SAVE playerpref rootNote = " + PlayerPrefs.GetInt( "rootNote" ) );
    }

    void SetDropdownValues()
    {        
        scaleDrop.value = PlayerPrefs.GetInt( "scaleType" );
        rootDrop.value = PlayerPrefs.GetInt( "rootNote" );
    }



    // Use this for initialization
    void Start()
    {
        SetDropdownValues();
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

        //set the variables based on the PlayerPrefs
        //scaleDrop.value = PlayerPrefs.GetInt( "scaleType" );
        //rootDrop.value = PlayerPrefs.GetInt( "rootNote" );
        //rootNote = rootDrop.value;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
