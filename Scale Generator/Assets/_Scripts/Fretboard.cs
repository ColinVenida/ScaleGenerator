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
    

    private int visibleStrings;

    //*REMEMBER*  #1 String goes in index 0!!!!!
    private int[] GUITAR_STANDARD = { 7, 2, 10, 5, 0, 7 };
    private int[] GUITAR_DROP_D = { 7, 2, 10, 5, 0, 5 };
    private int[] GUITAR_HALF_DOWN = { 6, 1, 9, 4, 11, 6 };
    private int[] GUITAR_FULL_DOWN = { 5, 0, 8, 3, 10, 5 };    
    private int[] GUITAR_OPEN_G = { 5, 2, 10, 5, 10, 5 };    
    private int[] GUITAR_DADGAD = { 5, 0, 10, 5, 0, 5 };    
    private int[] GUITAR_RAIN_SONG = { 5, 3, 10, 3, 10, 5 };

    private int[] BASS_STANDARD = { 10, 5, 0, 7 };
    private int[] UKULELE_STANDARD = { 0, 7, 3, 10 };


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

    
    public void AddString ()
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
        
        

    public void RemoveString ()
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

    private void WakeupTuning ()
    {
        for( int i = 0; i < guitStrings.Length; i++ )
        {
            switch( i )
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
                    Debug.Log( " *wakeup* GuitStringSeven = " + PlayerPrefs.GetInt( "GuitStringSeven" ) );
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringSeven" );
                    break;
                case 7:
                    guitStrings[i].noteSelect.value = PlayerPrefs.GetInt( "GuitStringEight" );
                    break;
            }//end switch
        }//end for
    }

    
    // Use this for initialization
    void Start () 
    {
        //Debug.Log( "====Fretboard Start() ====" );
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
        ToggleStrings( PlayerPrefs.GetInt( "GuitStringsVisible" ) );
        visibleStrings = PlayerPrefs.GetInt( "GuitStringsVisible" );    

	}

    private void Awake()
    {
        //Debug.Log( "Fretboard Awake()" );
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
    }



    // Update is called once per frame
    void Update () 
    {
		
	}
}
