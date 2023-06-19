using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScaleQuiz : MonoBehaviour
{
    private System.Random rnd = new System.Random();
   
    public Text questionText;
    public Text answerText;
    public Toggle[] keyToggles; //index 0 = A, 1 = B, 2 = C, 3 = D, 4 = E, 5 = F, 6 = G
    public Toggle halfToggle;
    public Dropdown scaleDrop;

    //private List<MusicScale> scaleList;
    private MusicScaleList scaleList;
    private ScaleFormulas.ScaleFormula currentFormula;
    private bool includeSharpKeys;
    private bool includeFlatKeys;


    // Use this for initialization
    void Start()
    {
        scaleList = new MusicScaleList();
        currentFormula = GetScaleFormulaFromDropdown( scaleDrop.value );
        PopulateScaleList();

        GenerateQuestion();        
       
        answerText.enabled = false;
    }

    private ScaleFormulas.ScaleFormula GetScaleFormulaFromDropdown( int value )
    {
        ScaleFormulas.ScaleFormula formula;

        switch ( value )
        {
            case 0:
                formula = ScaleFormulas.MAJOR_IONIAN;
                break;
            case 1:
                formula = ScaleFormulas.MINOR_AEOLIAN;
                break;
            case 2:
                formula = ScaleFormulas.DORIAN;
                break;
            case 3:
                formula = ScaleFormulas.PHRYGIAN;
                break;
            case 4:
                formula = ScaleFormulas.LYDIAN;
                break;
            case 5:
                formula = ScaleFormulas.MIXOLYDIAN;
                break;
            case 6:
                formula = ScaleFormulas.LOCRIAN;
                break;
            default:
                formula = ScaleFormulas.MAJOR_IONIAN;
                break;
        }
        return formula;
    }

    public void PopulateScaleList()
    {
        for ( int i = 0; i < keyToggles.Length; i++ )
        {
            if ( keyToggles[i].isOn )
            {
                switch ( i )
                {
                    case 0:
                        scaleList.AddScales( "A", currentFormula );
                        break;
                    case 1:
                        scaleList.AddScales( "B", currentFormula );
                        break;
                    case 2:
                        scaleList.AddScales( "C", currentFormula );
                        break;
                    case 3:
                        scaleList.AddScales( "D", currentFormula );
                        break;
                    case 4:
                        scaleList.AddScales( "E", currentFormula );
                        break;
                    case 5:
                        scaleList.AddScales( "F", currentFormula );
                        break;
                    case 6:
                        scaleList.AddScales( "G", currentFormula );
                        break;
                }
            }
        }
    }

    public void ChangeScale( int dropValue )
    {
        currentFormula = GetScaleFormulaFromDropdown( dropValue );
        scaleList.ScaleList.Clear();
        PopulateScaleList();
    }

    public void GenerateQuestion()
    {        
        if ( scaleList.ScaleList.Count == 0 )
        {
            questionText.text = "No Scale has been selected :(.";
            answerText.text = "";
            return;
        }

        int randomScaleIndex = rnd.Next( scaleList.ScaleList.Count );
        int totalIndexes = rnd.Next( 1, 5 );

        if ( answerText.isActiveAndEnabled )
        {
            ToggleAnswer();
        }
        List<int> randomScaleDegrees = GenerateRandomScaleDegrees( totalIndexes );
        SetQuestionText( randomScaleIndex, randomScaleDegrees );
        SetAnswerText( randomScaleIndex, randomScaleDegrees );    
    }

    private List<int> GenerateRandomScaleDegrees( int totalIndexes )
    {
        List<int> randoms = new List<int>();

        int i = 0;
        do
        {
            int randomDegree = rnd.Next( 1, 8 );

            if ( !randoms.Contains( randomDegree ) )
            {
                randoms.Add( randomDegree );
                i++;
            }
        }
        while ( i < totalIndexes );

        return randoms;
    }

    private void SetQuestionText( int scaleIndex, List<int> randomDegrees )
    {        
        string question;
        if ( randomDegrees.Count != 1 )
        {
            question = GenerateQuestionText(  scaleIndex, randomDegrees );
        }
        else
        {
            question = GenerateQuestionText_OnlyOneIndex( scaleIndex, randomDegrees );
        }        
        questionText.text = question;
    }

    private string GenerateQuestionText( int scaleIndex, List<int> randomDegrees )
    {
        StringBuilder sb = new StringBuilder( "Name the " );

        int lastIndex = randomDegrees.Count - 1;
        for ( int i = 0; i < lastIndex; i++ )
        {
            sb.Append( randomDegrees[i].ToString() + ", " );
        }

        sb.Append( "and " + randomDegrees[lastIndex].ToString() );
        sb.Append( " notes of " + scaleList.ScaleList[scaleIndex].ToString() );

        string question = sb.ToString();

        return question;
    }

    private string GenerateQuestionText_OnlyOneIndex( int scaleIndex, List<int> randomDegrees  )
    {
        return ( "Name the " + randomDegrees[0] + " note of " + scaleList.ScaleList[scaleIndex].ToString() );
    }

    private void SetAnswerText( int scaleIndex, List<int> randomDegrees )
    {
        StringBuilder sb = new StringBuilder();
       
        foreach( int i in randomDegrees )
        {
            sb.Append( scaleList.ScaleList[scaleIndex].NotesInScale[i.ToString()].ToString() + ", ");
        }

        int lastCommaIndex = ( sb.Length - 2 );
        sb.Remove( lastCommaIndex, 1 );
                
        answerText.text = sb.ToString();
    }
    
    public void ToggleAnswer()
    {
        answerText.enabled = !answerText.enabled;
        if ( answerText.enabled )
        {
            GameObject.Find( "ShowButton" ).GetComponentInChildren<Text>().text = "Hide Answer";
        }
        else
        {
            GameObject.Find( "ShowButton" ).GetComponentInChildren<Text>().text = "Show Answer";
        }
    }

    //add or remove the given scale from scaleList. method is called from the Toggles in the scene.
    public void ToggleScale( string scaleRootNote )
    {        
        scaleList.ToggleScale( scaleRootNote, currentFormula );
    }

    public void ToggleSharpKeys()
    {
        scaleList.ToggleSharpKeys( currentFormula );
    }

    public void ToggleFlatKeys()
    {
        scaleList.ToggleFlatKeys( currentFormula );
    }    
}
