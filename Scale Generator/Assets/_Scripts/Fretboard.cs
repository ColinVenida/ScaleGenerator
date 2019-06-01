using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fretboard : MonoBehaviour
{

    public GuitString[] guitStrings;
    public Button AddBtn;
    public Button RemoveBtn;
    public Dropdown presetDrop;
    public Dropdown scaleDrop;
    public Dropdown rootDrop;
    public ScaleGenerator scaleGen;
    public NoteArray noteArray;
    public TextFormatter textForm;

    private bool isInitialized;
    private bool guitStringInitialized;
    private string[] currentScale;
    private int visibleStrings;

    //*REMEMBER*  #1 String goes in index 0!!!!!
    private int[] GUITAR_STANDARD = { 11, 4, 15, 8, 1, 11 };
    private int[] GUITAR_DROP_D = { 11, 4, 15, 8, 1, 8 };
    private int[] GUITAR_HALF_DOWN = { 9, 2, 13, 6, 16, 9 };
    private int[] GUITAR_FULL_DOWN = { 8, 1, 12, 5, 15, 8 };    
    private int[] GUITAR_OPEN_G = { 8, 4, 15, 8, 15, 8 };    
    private int[] GUITAR_DADGAD = { 8, 1, 15, 8, 1, 8 };    
    private int[] GUITAR_RAIN_SONG = { 8, 5, 15, 5, 15, 8 };

    private int[] BASS_STANDARD = { 15, 8, 1, 11 };
    private int[] UKULELE_STANDARD = { 1, 11, 5, 15 };


    private void ToggleStrings ( int activeStrings )
    {
        for ( int i = 0; i < guitStrings.Length; i++ ) 
        {
            //iterate over guitStrings and turn off the remainder strings
            //ex. if 6 is given, turn on strings 0-5, and turn off strings 6-7
            if (i - activeStrings < 0)
            {                
                guitStrings[i].gameObject.SetActive( true );
            }
            else
            {                
                guitStrings[i].gameObject.SetActive( false );
            }
        }
        visibleStrings = activeStrings;
        PlayerPrefs.SetInt( "GuitStringsVisible", activeStrings );
    }

    private void SetTuning ( int[] tuning )
    {
        GuitString.changePreset = false;
        for( int i = 0; i < visibleStrings; i++ )
        {           
            guitStrings[i].noteSelect.value = tuning[i];
        }
        GuitString.changePreset = true;
    }

    
    public void AddGuitString ()
    {
        //check visible strings
        if( visibleStrings < 8 )
        {            
            guitStrings[visibleStrings].gameObject.SetActive( true );
            visibleStrings++;
            PlayerPrefs.SetInt( "GuitStringsVisible", PlayerPrefs.GetInt( "GuitStringsVisible" ) + 1 );
        }
        presetDrop.value = 0;
    }
        
    public void RemoveGuitString ()
    {
        //check visible strings
        if (visibleStrings > 4)
        {            
            guitStrings[visibleStrings - 1].gameObject.SetActive( false );
            visibleStrings--;
            PlayerPrefs.SetInt( "GuitStringsVisible", PlayerPrefs.GetInt( "GuitStringsVisible" ) - 1 );
        }
        presetDrop.value = 0;
    }

    public void SetPreset ()
    {
        switch (presetDrop.value)
        {
            case 0:
                break;
            case 1: 
                ToggleStrings( 6 );
                SetTuning( GUITAR_STANDARD );
                break;
            case 2:
                ToggleStrings( 6 );
                SetTuning( GUITAR_DROP_D );
                break;
            case 3:
                ToggleStrings( 6 );
                SetTuning( GUITAR_HALF_DOWN );
                break;
            case 4:
                ToggleStrings( 6 );
                SetTuning( GUITAR_FULL_DOWN );
                break;
            case 5:
                ToggleStrings( 6 );
                SetTuning( GUITAR_OPEN_G );
                break;
            case 6:
                ToggleStrings( 6 );
                SetTuning( GUITAR_DADGAD );
                break;
            case 7:
                ToggleStrings( 6 );
                SetTuning( GUITAR_RAIN_SONG );
                break;                
            case 8:
                ToggleStrings( 4 );
                SetTuning( BASS_STANDARD );
                break;
            case 9:
                ToggleStrings( 4 );
                SetTuning( UKULELE_STANDARD );
                break;            
            default:
                break;
        };

        PlayerPrefs.SetInt( "presetValue", presetDrop.value );
    }

    //function to update the scale the GuitString displays when a new value of scaleDrop or RootDrop is selected
    public void SetScale() 
    {
        if( !isInitialized )
        {
            //Debug.Log( "Not initialized, returning" );
            return;
        }
        scaleGen.GenerateScale( rootDrop.value, scaleDrop.value, noteArray );
        SaveScalePrefs();        

        //update all the string's scales
        for (int i = 0; i < 8; i++)
        {          
            switch (i)
            {
                case 0:                    
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringOne" ) );
                    break;
                case 1:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringTwo" ) );
                    break;
                case 2:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringThree" ) );
                    break;
                case 3:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringFour" ) );
                    break;
                case 4:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringFive" ) );
                    break;
                case 5:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringSix" ) );
                    break;
                case 6:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringSeven" ) );
                    break;
                case 7:
                    guitStrings[i].CalculateFrets( PlayerPrefs.GetInt( "GuitStringEight" ) );
                    break;
            }//end switch
        }

        textForm.UpdateScale();
    }

    //change the color of all the fret texts that match the given note
    public void HighlightFrets( string note )
    {
        for (int i = 0; i < guitStrings.Length; i++)
        {
            for (int j = 0; j < guitStrings[i].fretArray.Length; j++)
            {
                if (guitStrings[i].fretArray[j].text == note)
                {
                    guitStrings[i].fretArray[j].color = new Color( (float)0.8, (float)0.8, (float)0, 255 );
                    guitStrings[i].fretArray[j].fontStyle = FontStyle.Bold;
                }
                else if (guitStrings[i].fretArray[j].color != Color.blue)
                {
                    guitStrings[i].fretArray[j].color = new Color( 0, 0, 0 );
                    guitStrings[i].fretArray[j].fontStyle = FontStyle.Normal;
                }
            }//end j
        }//end i
    }

    public bool GetGuitStringInitialized()
    {
        return guitStringInitialized;
    }
    
    public bool GetIsInitialized()
    {
        return isInitialized;
    }
    
    // Use this for initialization
    void Start () 
    {
        
        //set the scale and root to the PlayerPrefs
        scaleDrop.value = PlayerPrefs.GetInt( "scaleType" );
        rootDrop.value = PlayerPrefs.GetInt( "rootNote" );
        
        

        
        //set each string's tuning       
        for (int i = 0; i < guitStrings.Length; i++)
        {
            switch (i)
            {
                case 0:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringOne" );
                    break;
                case 1:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringTwo" );
                    break;
                case 2:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringThree" );
                    break;
                case 3:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringFour" );
                    break;
                case 4:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringFive" );
                    break;
                case 5:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringSix" );
                    break;
                case 6:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringSeven" );
                    break;
                case 7:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringEight" );
                    break;
            }//end switch
        }//end for

        isInitialized = true;
        guitStringInitialized = true;
        SetScale();
        ToggleStrings( PlayerPrefs.GetInt( "GuitStringsVisible" ) );
        visibleStrings = PlayerPrefs.GetInt( "GuitStringsVisible" );
	}

    private void Awake()
    {        
        //check and set the PlayerPrefs
        if (!PlayerPrefs.HasKey( "GuitStringsVisible" ))
        {
            PlayerPrefs.SetInt( "GuitStringsVisible", 6 );
        }

        if (!PlayerPrefs.HasKey( "GuitStringOne" ))
        {
            //if it doesn't have the first one, then set them all
            PlayerPrefs.SetInt( "GuitStringOne", 7 );
            PlayerPrefs.SetInt( "GuitStringTwo", 2 );
            PlayerPrefs.SetInt( "GuitStringThree", 10 );
            PlayerPrefs.SetInt( "GuitStringFour", 5 );
            PlayerPrefs.SetInt( "GuitStringFive", 0 );
            PlayerPrefs.SetInt( "GuitStringSix", 7 );
            PlayerPrefs.SetInt( "GuitStringSeven", 2 );
            PlayerPrefs.SetInt( "GuitStringEight", 10 );
        }

        if (!PlayerPrefs.HasKey( "presetValue" ))
        {
            PlayerPrefs.SetInt( "presetValue", 0 );
        }

        isInitialized = false;
        guitStringInitialized = false;

    }

    private void SaveScalePrefs()
    {       
        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
        PlayerPrefs.SetInt( "rootNote", rootDrop.value );
        PlayerPrefs.Save();
    }


    // Update is called once per frame
    void Update () 
    {
		
	}
}
