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
    public Image fretImage;
    public Sprite[] fretboardImages;     //8-string fretboardImage is in [index 0], 7-string is in [index 1], etc.    
    
    private bool useArpeggio = false;
    
    private string[] currentScale;
    private int visibleStrings;
    private int fretImageIndex;
    private List<string> arpList;
    

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



    private void Awake()
    {
        //check and set the PlayerPrefs
        if ( !PlayerPrefs.HasKey( "GuitStringsVisible" ) )
        {
            PlayerPrefs.SetInt( "GuitStringsVisible", 6 );
        }

        if ( !PlayerPrefs.HasKey( "GuitStringOne" ) )
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

        if ( !PlayerPrefs.HasKey( "presetValue" ) )
        {
            PlayerPrefs.SetInt( "presetValue", 0 );
        }
    }

    void Start()
    {
        //set the scale and root to the PlayerPrefs
        scaleDrop.value = PlayerPrefs.GetInt( "scaleType" );
        rootDrop.value = PlayerPrefs.GetInt( "rootNote" );

        //set each string's tuning       
        for ( int i = 0; i < guitStrings.Length; i++ )
        {
            switch ( i )
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
        
        arpList = new List<string>();
        SetScale();
        ToggleStrings( PlayerPrefs.GetInt( "GuitStringsVisible" ) );
        visibleStrings = PlayerPrefs.GetInt( "GuitStringsVisible" );
    }

    private void SaveScalePrefs()
    {
        PlayerPrefs.SetInt( "scaleType", scaleDrop.value );
        PlayerPrefs.SetInt( "rootNote", rootDrop.value );
        PlayerPrefs.Save();
    }

    private void ToggleStrings ( int activeStrings )
    {
        for ( int i = 0; i < guitStrings.Length; i++ ) 
        {
            //iterate over guitStrings and turn off the remainder strings
            //ex. if 6 is given, turn on strings 0-5, and turn off strings 6-7
            if (i - activeStrings < 0)
            {                
                guitStrings[i].gameObject.SetActive( true );
                guitStrings[i].noteSelect.gameObject.SetActive( true );
            }
            else
            {                
                guitStrings[i].gameObject.SetActive( false );
                guitStrings[i].noteSelect.gameObject.SetActive( false );
            }
        }
        visibleStrings = activeStrings;
        fretImage.rectTransform.sizeDelta = new Vector2( fretImage.rectTransform.sizeDelta.x, 85 * (activeStrings));

        switch (visibleStrings)
        {
            case 4:
                fretImageIndex = 4;
                break;
            case 5:
                fretImageIndex = 3;
                break;
            case 6:
                fretImageIndex = 2;
                break;
            case 7:
                fretImageIndex = 1;
                break;
            case 8:
                fretImageIndex = 0;
                break;
            default:
                fretImageIndex = 0;
                break;
        }

        fretImage.sprite = fretboardImages[fretImageIndex];
        fretImage.sprite = fretboardImages[0];

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
            guitStrings[visibleStrings].noteSelect.gameObject.SetActive( true );
            guitStrings[visibleStrings].gameObject.SetActive( true );

            fretImage.rectTransform.sizeDelta = new Vector2( fretImage.rectTransform.sizeDelta.x, 
                fretImage.rectTransform.sizeDelta.y + 85 );

            fretImageIndex--;
            fretImage.sprite = fretboardImages[fretImageIndex];

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

            guitStrings[visibleStrings - 1].noteSelect.gameObject.SetActive( false );
            guitStrings[visibleStrings - 1].gameObject.SetActive( false );

            fretImage.rectTransform.sizeDelta = new Vector2( fretImage.rectTransform.sizeDelta.x,
               fretImage.rectTransform.sizeDelta.y - 85 );

            fretImageIndex++;
            fretImage.sprite = fretboardImages[fretImageIndex];

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
        
        ResetArpeggio();

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
        textForm.ShowArpeggio( false ); 
        textForm.UpdateScale();
    }

    //function that sets the 2nd, 4th, and 6th notes inactive   
    public void SetArpeggio()
    {
        useArpeggio = true;
        arpList.Clear();

        //find the key of the scale; use the familyIndex to better fit with the rest of the program
        int famIndex = scaleGen.FindFamilyIndex( rootDrop.value );      
        
        //find the 2nd, 4th, and 5th notes of the scale
        int[] intervals =
        {
            famIndex + 1,
            famIndex + 3,
            famIndex + 5
        };

        for (int i = 0; i < 3; i++)
        {
            if (intervals[i] > 6)
            {
                intervals[i] -= 7;
            }
        }
        
        arpList.Add( noteArray.noteArray[intervals[0]].GetNote() );
        arpList.Add( noteArray.noteArray[intervals[1]].GetNote() );
        arpList.Add( noteArray.noteArray[intervals[2]].GetNote() );

        for (int i = 0; i < 8; i++)
        {           
            guitStrings[i].FilterArpeggio( arpList );
        }
    }

    public void ResetArpeggio()
    {
        useArpeggio = false;
        for (int i = 0; i < 8; i++)
        {
            guitStrings[i].CancelArpeggio();
        }        
    }
    
    //change the color of all the fret texts that match the given note
    public void HighlightFrets( string note )
    {
        for (int i = 0; i < guitStrings.Length; i++)
        {        
            for (int j = 0; j < guitStrings[i].textArray.Length; j++)
            {                
                if (guitStrings[i].buttonArray[j].image.color.Equals( new Color( 0.0f, 0.6f, 0.8f ) )) //always keep the blue root notes
                {
                    continue;
                }
                if (guitStrings[i].textArray[j].text == note)
                {                    
                    guitStrings[i].buttonArray[j].image.color = new Color( 0.0f, 0.8f, 0.6f );
                    guitStrings[i].textArray[j].fontStyle = FontStyle.Bold;
                }
                else if (guitStrings[i].textArray[j].color != Color.blue)
                {                    
                    guitStrings[i].buttonArray[j].image.color = new Color( 1.0f, 1.0f, 1.0f );
                    guitStrings[i].textArray[j].fontStyle = FontStyle.Normal;
                }
            }//end j

        }//end i
    }

    public List<string> GetArplist()
    {
        return arpList;
    }

    public bool UseArpeggio()
    {
        return useArpeggio;
    }       
}
