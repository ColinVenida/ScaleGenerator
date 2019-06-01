using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//methods copied from ScaleScene.cs
public class TextFormatter : MonoBehaviour
{
    public Fretboard fBoard;
    //public ScaleGenerator scaleGen;
    //public NoteArray noteArray;
    public Text[] notePositions;
    public Text[] displayText;
    
    public Dropdown scaleDrop;
    public Dropdown rootDrop;



    public void UpdateScale ()
    {    

        //**find the starting point of the scale here***
        int familyIndex = fBoard.scaleGen.FindFamilyIndex( rootDrop.value );

        for (int i = 0; i < 8; i++)
        {
            displayText[i].text = fBoard.noteArray.noteArray[familyIndex].GetNote();
            familyIndex++;
            if (familyIndex > 6)
            {
                familyIndex = 0;
            }
        }
        UpdateDisplay();
        //SavePlayerPrefs();
    }

    public void UpdateDisplay()
    {
        if ( !fBoard.GetIsInitialized() )
        {
            return;
        }

        //add minor/diminished
        switch ( scaleDrop.value )
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
        switch ( scale.value )
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
