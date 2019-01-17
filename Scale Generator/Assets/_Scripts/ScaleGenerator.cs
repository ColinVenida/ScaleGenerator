//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class ScaleGenerator : MonoBehaviour
//{       
//    private int[] majorFormula = { 0, 2, 2, 1, 2, 2, 2, 1 };
//    private int[] minorFormula = { 0, 2, 1, 2, 2, 1, 2, 2 };    
//    private int rootNote = 3;


//    //public Note notes;
//    public NoteArray noteArray;
//    public Dropdown scaleDrop;
//    public Dropdown rootDrop;
//    public Text[] notePositions;
//    public Text[] displayText;


//    public void GenerateScale ()
//    {     

//        //rootNote is also set here incase this method is called before the
//        //Awake() method finishes (yes it happens)
//        //                              -OnValueChanged event fires when scaleDrop is set during Awake() method

//        rootNote = PlayerPrefs.GetInt( "rootNote" );
//        rootDrop.value = PlayerPrefs.GetInt( "rootNote" );       

//        //set the first value to the root note
//        int note = rootNote;        
//        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
//        PlayerPrefs.SetInt( "rootNote", rootDrop.value );

//        int familyIndex = 0;

//        //find the familyIndex
//        switch( note )
//        {
//            case 0:
//            case 1:
//            case 2:
//                familyIndex = 0; //set to A
//                break;
//            case 3:
//            case 4:
//                familyIndex = 1; //set to B
//                break;
//            case 5:                
//            case 6:
//                familyIndex = 2; //set to C
//                break;
//            case 7:
//            case 8:               
//            case 9:
//                familyIndex = 3; //set to D
//                break;
//            case 10:
//            case 11: 
//                familyIndex = 4; //set to E
//                break;
//            case 12:
//            case 13:
//                familyIndex = 5; //set to F
//                break;  
//            case 14:                
//            case 15:
//            case 16: 
//                familyIndex = 6; //set to G
//                break;
//        }

//        //adjusting the sharp/flat variable of the first note
//        switch( note )
//        {
//            //cases for flats
//            case 0:
//            case 3:
//            case 7:
//            case 10:
//            case 14:
//                noteArray.noteArray[familyIndex].usedFlat = true;
//                break;
//            //cases for sharps
//            case 2:
//            case 6:
//            case 9:
//            case 13:
//            case 16:
//                noteArray.noteArray[familyIndex].usedSharp = true;
//                break;            
//        }

//        //calculate the rest of the scale based on the root note and scale formula
//        int[] currentScale;

//        switch( scaleDrop.value )
//        {
//            case 0:
//                currentScale = majorFormula;
//                break;
//            case 1:
//                currentScale = minorFormula;
//                break;
//            default:
//                Debug.Log( "Default Scale [MAJOR] has been used" );
//                currentScale = majorFormula;
//                break;
//        }

//        displayText[0].text = noteArray.noteArray[familyIndex].GetNote();
//        displayText[7].text = displayText[0].text;
//        familyIndex++;
//        if (familyIndex > 6)
//        {
//            familyIndex = 0;
//        }

//        //generate the rest of the scale including sharps/flats based on the note before
//        for (int j = 1; j < 7; j++)
//        {

//            //determine whether the next note is sharp/flat     

//            if (currentScale[j] == 2) //if the next note in scale is two frets away
//            {
//                if (familyIndex == 0)  //check for array bounds
//                {
//                    //if used sharp
//                    if (noteArray.noteArray[6].usedSharp)
//                    {
//                        noteArray.noteArray[familyIndex].usedSharp = true;
//                    }
//                    else if (noteArray.noteArray[6].usedFlat) //if used flat
//                    {
//                        noteArray.noteArray[familyIndex].usedFlat = true;
//                    }

//                }
//                else
//                {
//                    //check if the next tone family is 1 fret away (ie. B->C, E->F)
//                    if (noteArray.noteArray[familyIndex - 1].nextWholetone == 1)
//                    {
//                        if (!noteArray.noteArray[familyIndex - 1].usedFlat && !noteArray.noteArray[familyIndex - 1].usedSharp)
//                        {
//                            noteArray.noteArray[familyIndex].usedSharp = true;
//                        }
//                        //if previous note used a flat, then leave the next note alone
//                    }
//                    //in these cases, the next tone family is two frets away
//                    //if used sharp
//                    else if (noteArray.noteArray[familyIndex - 1].usedSharp)
//                    {
//                        noteArray.noteArray[familyIndex].usedSharp = true;
//                    }
//                    else if (noteArray.noteArray[familyIndex - 1].usedFlat) //if used flat
//                    {
//                        noteArray.noteArray[familyIndex].usedFlat = true;
//                    }
//                }
//            }

//            if (currentScale[j] == 1) //if the next note in scale is one fret away
//            {
//                if (familyIndex == 0) //check array bounds
//                {
//                    //if previous note is natural, add flat to next note
//                    if (!noteArray.noteArray[6].usedSharp && !noteArray.noteArray[6].usedFlat)
//                    {
//                        noteArray.noteArray[familyIndex].usedFlat = true;
//                    }
//                }
//                else
//                {
//                    if (noteArray.noteArray[familyIndex - 1].nextWholetone == 2)
//                    {
//                        //if prev. used natural, add flat
//                        if (!noteArray.noteArray[familyIndex - 1].usedSharp && !noteArray.noteArray[familyIndex - 1].usedFlat)
//                        {
//                            noteArray.noteArray[familyIndex].usedFlat = true;
//                        }

//                        //if prev. used sharp, then keep next natural
//                        //if prev. note used flat, then it's theoretical scale
//                    }
//                    else   //next wholeTone == 1
//                    {
//                        noteArray.noteArray[familyIndex].usedSharp = noteArray.noteArray[familyIndex - 1].usedSharp;
//                        noteArray.noteArray[familyIndex].usedFlat = noteArray.noteArray[familyIndex - 1].usedFlat;
//                    }
//                }
//            }

//            displayText[j].text = noteArray.noteArray[familyIndex].GetNote();
//            familyIndex++;
//            if (familyIndex > 6)
//            {
//                familyIndex = 0;
//            }
//        }

//        UpdateDisplay();

//        //reset the sharp/flat booleans

//        for ( int i = 0; i < 7; i++ )
//        {
//            noteArray.noteArray[i].usedFlat = false;
//            noteArray.noteArray[i].usedSharp = false;
//        }

//        //save the PlayerPrefs       
//        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
//        PlayerPrefs.Save();
//    }


//    public void ChangeScaleType ()
//    {

//    }

//    //implicitely calls GenerateScale()    
//    public void ChangeRoot ()
//    {              
//        rootNote = rootDrop.value;        
//        PlayerPrefs.SetInt( "rootNote", rootDrop.value );
//        PlayerPrefs.Save();
//        GenerateScale();        
//    }



//    //update symbols to accurately reflect the current scale
//    public void UpdatePositions ( Dropdown scale )
//    {
//        switch ( scale.value )
//        {
//            case 0:
//                notePositions[0].text = "I";
//                notePositions[1].text = "ii";
//                notePositions[2].text = "iii";
//                notePositions[3].text = "IV";
//                notePositions[4].text = "V";
//                notePositions[5].text = "vi";
//                notePositions[6].text = "vii";
//                notePositions[7].text = "VIII";
//                break;
//            case 1:
//                notePositions[0].text = "i";
//                notePositions[1].text = "ii";
//                notePositions[2].text = "III";
//                notePositions[3].text = "iv";
//                notePositions[4].text = "v";
//                notePositions[5].text = "VI";
//                notePositions[6].text = "VII";
//                notePositions[7].text = "viii";
//                break;
//        }
//    }


//    public void UpdateDisplay ()
//    {
//        UpdatePositions( scaleDrop );

//        //add minor/diminished
//        switch ( scaleDrop.value )
//        {
//            case 0:
//                displayText[1].text += "m";
//                displayText[2].text += "m";
//                displayText[5].text += "m";
//                displayText[6].text += "dim";
//                break;
//            case 1:
//                displayText[0].text += "m";
//                displayText[1].text += "dim";
//                displayText[3].text += "m";
//                displayText[4].text += "m";
//                displayText[7].text += "m";
//                break;
//        }
//    }

//    private void SavePlayerPrefs ()
//    {
//        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
//        PlayerPrefs.SetInt( "rootNote", rootDrop.value );
//        PlayerPrefs.Save();

//        Debug.Log( "Saving!" );       
//        //Debug.Log( "scaleType = " + PlayerPrefs.GetInt( "scaleType" ) );
//        Debug.Log( "SAVE playerpref rootNote = " + PlayerPrefs.GetInt( "rootNote" ) );
//    }



//    // Use this for initialization
//    void Start ()
//    {

//    }

//    private void Awake ()
//    {
//        //check and set the Playerprefs
//        //set the default scale/root dropdowns to Major C
//        if ( !PlayerPrefs.HasKey( "scaleType" ) )
//        {
//            PlayerPrefs.SetInt( "scaleType", 0 );
//        }

//        if ( !PlayerPrefs.HasKey( "rootNote" ) )
//        {
//            PlayerPrefs.SetInt( "rootNote", 5 );
//        }

//        //set the variables based on the PlayerPrefs
//        scaleDrop.value = PlayerPrefs.GetInt( "scaleType" );
//        rootDrop.value = PlayerPrefs.GetInt( "rootNote" );
//        rootNote = rootDrop.value;
//    }

//    // Update is called once per frame
//    void Update ()
//    {

//	}
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScaleGenerator : MonoBehaviour
{
    private int[] majorFormula = { 0, 2, 2, 1, 2, 2, 2, 1 };
    private int[] minorFormula = { 0, 2, 1, 2, 2, 1, 2, 2 };
    private int rootNote = 3;


    //public Note notes;
    public NoteArray noteArray;
    //public Dropdown scaleDrop;
    //public Dropdown rootDrop;

    //public void GenerateScale()
    public string[] GenerateScale ( int root, int scale )
    {

        string[] strArray= { "", "", "", "", "", "", "", "" };

        //rootNote is also set here incase this method is called before the
        //Awake() method finishes (yes it happens)
        //                              -OnValueChanged event fires when scaleDrop is set during Awake() method

        //rootNote = PlayerPrefs.GetInt( "rootNote" );
        //rootDrop.value = PlayerPrefs.GetInt( "rootNote" );

        //set the first value to the root note
        int note = root;
        //PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
        //PlayerPrefs.SetInt( "rootNote", rootDrop.value );

        //find the familyIndex
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

        //Debug.Log( "familyIndex = " + familyIndex );
        //Debug.Log( "noteArray[] = " + noteArray.noteArray[familyIndex] );

        strArray[0] = noteArray.noteArray[familyIndex].GetNote();
        strArray[7] = strArray[0];
        familyIndex++;
        if (familyIndex > 6)
        {
            familyIndex = 0;
        }

        //generate the rest of the scale including sharps/flats based on the note before
        for (int j = 1; j < 7; j++)
        {

            //determine whether the next note is sharp/flat     

            if (currentScale[j] == 2) //if the next note in scale is two frets away
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
            }

            if (currentScale[j] == 1) //if the next note in scale is one fret away
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
            }

            strArray[j] = noteArray.noteArray[familyIndex].GetNote();
            familyIndex++;
            if (familyIndex > 6)
            {
                familyIndex = 0;
            }
        }        

        //reset the sharp/flat booleans

        for (int i = 0; i < 7; i++)
        {
            noteArray.noteArray[i].usedFlat = false;
            noteArray.noteArray[i].usedSharp = false;
        }

        //save the PlayerPrefs       
        //PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
        //PlayerPrefs.Save();

        //for (int i = 0; i < 8; i++)
        //{
        //    Debug.Log( "strArray[" + i + "] = " + strArray[i] );
        //}
        

        return strArray;
    }
    
    public int FindFamilyIndex ( int note )
    {
        int index = 0;

        //switch ( note )
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
   
}
