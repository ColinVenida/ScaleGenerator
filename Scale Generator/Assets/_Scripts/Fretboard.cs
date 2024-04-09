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
    public Text theoreticalWarning;
    public Text scaleTitle;

    public TextFormatter textForm;
    public Image fretImage;
    public Sprite[] fretboardImages;     //8-string fretboardImage is in [index 0], 7-string is in [index 1], etc.                                         

    private bool areGuitStringsInitialized = false;
    public bool AreGuitStringsInitialized { get { return areGuitStringsInitialized; } }
    
    private bool useArpeggio = false;    
    
    private int visibleStrings;
    private int fretImageIndex;

    private List<string> arpList;
    private const int SECOND_DEGREE = 2;
    private const int FOURTH_DEGREE = 4;
    private const int SIXTH_DEGREE = 6;

    private MusicScale currentMusicScale;
    public MusicScale CurrentMusicScale { get { return currentMusicScale; } }

    private bool hasStartMethodFinished = false;


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

    private Color TEAL = new Color( 0.0f, 0.8f, 0.6f );
    private Color WHITE = new Color( 1.0f, 1.0f, 1.0f );


    private void Awake()
    {
        InitializeMusicScale();
    }

    private void InitializeMusicScale()
    {
        InitializePlayerPrefs();
        int scaleValue = PlayerPrefs.GetInt( "scaleType" );
        int rootValue = PlayerPrefs.GetInt( "rootNote" );

        string rootNote = NoteValues.ConvertNote_IntToString( rootValue );
        ScaleFormulas.ScaleFormula formula = ScaleFormulas.GetFormulaFromDropValue( scaleValue );
        currentMusicScale = new MusicScale( rootNote, formula );

        scaleDrop.value = scaleValue;
        rootDrop.value = rootValue;
        
        InitializeGuitStringTuning();
        UpdateScaleTitle();
    }

    private void InitializePlayerPrefs()
    {
        //check and set the PlayerPrefs
        if ( !PlayerPrefs.HasKey( "GuitStringsVisible" ) )
        {
            PlayerPrefs.SetInt( "GuitStringsVisible", 6 );
        }

        if ( !PlayerPrefs.HasKey( "GuitStringOne" ) )
        {
            //if it doesn't have the first one, then set them all
            PlayerPrefs.SetInt( "GuitStringOne", 11 );
            PlayerPrefs.SetInt( "GuitStringTwo", 4 );
            PlayerPrefs.SetInt( "GuitStringThree", 15 );
            PlayerPrefs.SetInt( "GuitStringFour", 8 );
            PlayerPrefs.SetInt( "GuitStringFive", 1 );
            PlayerPrefs.SetInt( "GuitStringSix", 11 );
            PlayerPrefs.SetInt( "GuitStringSeven", 4 );
            PlayerPrefs.SetInt( "GuitStringEight", 15 );
        }

        if ( !PlayerPrefs.HasKey( "scaleType") )
        {
            PlayerPrefs.SetInt( "scaleType", 0 );
            PlayerPrefs.SetInt( "rootNote", 5 );
        }

        if ( !PlayerPrefs.HasKey( "presetValue" ) )
        {
            PlayerPrefs.SetInt( "presetValue", 0 );
        }
    }

    private void InitializeGuitStringTuning()
    {
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
            }
        }
        areGuitStringsInitialized = true;        
    }

    private void UpdateScaleTitle()
    {
        scaleTitle.text = currentMusicScale.ToString();
    }


    void Start()
    {
        arpList = new List<string>();
        SetScale();        
        visibleStrings = PlayerPrefs.GetInt( "GuitStringsVisible" );
        ToggleStrings( PlayerPrefs.GetInt( "GuitStringsVisible" ) );
        hasStartMethodFinished = true;
    }    

    //function to update the scale the GuitString displays when a new value of scaleDrop or rootDrop is selected
    public void SetScale()
    {
        //workaround for avoiding the OnValueChanged event during Fretboard.Awake()
        if ( !areGuitStringsInitialized )
        {            
            return;
        }
        ResetArpeggio();        
        SaveScalePrefs();

        //workaround for avoiding the MusicSale being initialized during Start()
        if ( hasStartMethodFinished )
        {
            string rootNote = NoteValues.ConvertNote_IntToString( rootDrop.value );
            ScaleFormulas.ScaleFormula formula = ScaleFormulas.GetFormulaFromDropValue( scaleDrop.value );
            currentMusicScale = new MusicScale( rootNote, formula );
        }

        UpdateGuitStringsWithNewScale();        

        //textForm.DisableArpeggioColor();
        textForm.UpdateDisplayScalesOnFretboard();
        textForm.UpdateArpeggioTextColor();
        UpdateTheoreticalWarning();
        UpdateScaleTitle();
    }

    private void UpdateGuitStringsWithNewScale()
    {
        for ( int i = 0; i < guitStrings.Length; i++ )
        {
            switch ( i )
            {
                case 0:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringOne" ) );
                    break;
                case 1:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringTwo" ) );
                    break;
                case 2:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringThree" ) );
                    break;
                case 3:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringFour" ) );
                    break;
                case 4:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringFive" ) );
                    break;
                case 5:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringSix" ) );
                    break;
                case 6:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringSeven" ) );
                    break;
                case 7:
                    guitStrings[i].CalculateFrets_New( PlayerPrefs.GetInt( "GuitStringEight" ) );
                    break;
            }
        }
    }

    private void UpdateTheoreticalWarning()
    {
        bool isActive = currentMusicScale.IsTheoretical;
        theoreticalWarning.gameObject.SetActive( isActive );
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
        UpdateFretboardImage();
    }
    
    private void UpdateFretboardImage()
    {        
        const float GSTRING_HEIGHT = 85;
        float adjustedY = GSTRING_HEIGHT * ( visibleStrings );

        fretImage.rectTransform.sizeDelta = new Vector2( fretImage.rectTransform.sizeDelta.x, adjustedY );        

        PlayerPrefs.SetInt( "GuitStringsVisible", visibleStrings );
        PlayerPrefs.Save();
    }

    private void SetTuning( int[] tuning )
    {
        GuitString.changePreset = false;
        
        for( int i = 0; i < visibleStrings; i++ )
        {            
            guitStrings[i].noteSelect.value = tuning[i];
        }
        GuitString.changePreset = true;
    }
    
    public void AddGuitString()
    {
        //check visible strings
        if( visibleStrings < 8 )
        {         
            guitStrings[visibleStrings].noteSelect.gameObject.SetActive( true );
            guitStrings[visibleStrings].gameObject.SetActive( true );

            visibleStrings++;            
            UpdateFretboardImage();
        }
        presetDrop.value = 0;
    }
        
    public void RemoveGuitString()
    {
        //check visible strings
        if (visibleStrings > 4)
        {
            guitStrings[visibleStrings - 1].noteSelect.gameObject.SetActive( false );
            guitStrings[visibleStrings - 1].gameObject.SetActive( false );

            visibleStrings--;            
            UpdateFretboardImage();
        }
        presetDrop.value = 0;
    }

    public void SetPreset()
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

    //function that sets the 2nd, 4th, and 6th scale degrees inactive on the fretboard
    public void SetArpeggio()
    {
        useArpeggio = true;
        arpList.Clear();
       
        arpList.Add( currentMusicScale.NotesInScale[SECOND_DEGREE.ToString()].ToString() );
        arpList.Add( currentMusicScale.NotesInScale[FOURTH_DEGREE.ToString()].ToString() );
        arpList.Add( currentMusicScale.NotesInScale[SIXTH_DEGREE.ToString()].ToString() );        

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
    public void HighlightFrets( string noteName )
    {
        for (int i = 0; i < guitStrings.Length; i++)
        {        
            for (int j = 0; j < guitStrings[i].textArray.Length; j++)
            {          
                if (guitStrings[i].textArray[j].text == noteName )
                {                    
                    guitStrings[i].buttonArray[j].image.color = TEAL;
                    guitStrings[i].textArray[j].fontStyle = FontStyle.Bold;
                }
                else 
                {
                    guitStrings[i].buttonArray[j].image.color = WHITE;
                    guitStrings[i].textArray[j].fontStyle = FontStyle.Normal;
                }                
            }//end j
        }//end i
    }

    public void HighlightFrets( int scaleDegree )
    {
        string noteName = currentMusicScale.NotesInScale[scaleDegree.ToString()].ToString();        
        HighlightFrets( noteName );
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
