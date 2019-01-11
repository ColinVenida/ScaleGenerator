using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleQuiz : MonoBehaviour
{

    private int[] quizNotes = { 0, 0, 0, 0 };
    private int[] wholeNotes = { 0, 2, 3, 5, 7, 8, 10 };  //array of just whole notes
    private int[] majorDistances = { 0, 2, 4, 5, 7, 9, 11 }; //how many half-steps it takes to get from the root note to a given scale Position
                                            //root note + 4 half steps = 3rd note of scale
                                            
    private int rootNote;
    private System.Random rnd = new System.Random();

    public Note notes;
    public Text questionText;
    public Text answerText;
    public Toggle[] keyToggles;
    public Toggle halfToggle;
     
    public void GenerateQuestion ()
    {

        //set rootNote of one of the whole notes
        //int rootNote = wholeNotes[rnd.Next(0, 7)];       
        int rootNote = wholeNotes[GenerateRoot()];

        //get  1-4 random values, values range form 1-7, values can't be equal twice in a row
        int totalPositions = rnd.Next (1, 5 );     
        
        //if the answer is still showing, hide it for next question
        if( answerText.enabled )
        {
            ShowAnswer();
        }

        //set the first index of quizNotes
        quizNotes[0] = rnd.Next( 1, 7 );

        for ( int i = 1; i < totalPositions; i++ )
        {
            int position = 0;

            do
            {
                position = rnd.Next(1, 7);
            }
            while ( position == quizNotes[i-1] );

            quizNotes[i] = position;
            
        }

        //zero out the remaining array indexes
        if (totalPositions < quizNotes.Length)
        {
            for (int j = totalPositions; j < quizNotes.Length; j++)
            {
                quizNotes[j] = 0;
            }
        }
               
        //find the associated notes based on random values and replace question and answer text
        SetQuestionText( rootNote, quizNotes );
        SetAnswerText( rootNote, quizNotes );
    }


    //function to generate a root note that respects the filters
    //TODO:  ADD THE HALF-NOTES FILTER
        // ***NEED TO DETERMINE HOW TO PROPERLY LABEL NOTES****
    private int GenerateRoot ()
    {
        int root = 0;

        do
        {
            root = rnd.Next( 0, 7 );
        }
        while ( !keyToggles[root].isOn );
                
        return root;
    }

    private void SetQuestionText ( int root, int[] quiz )
    {
        string strRoot = "";
        string sequence = "";

        strRoot = notes.GetNoteCodes()[root];
                
        //add to the question text based on the generated positions
        for ( int i = 0; i < quiz.Length; i++ )
        {
            switch( quiz[i] )
            {
                case 0:
                    break;
                case 1:
                    sequence += "2nd, ";
                    break;
                case 2:
                    sequence += "3rd, ";
                    break;
                case 3:
                    sequence += "4th, ";
                    break;
                case 4:
                    sequence += "5th, ";
                    break;
                case 5:
                    sequence += "6th, ";
                    break;
                case 6:
                    sequence += "7th, ";
                    break;                
            }
        }

        if ( sequence.Length > 5 )
        {            
            sequence = sequence.Insert( sequence.Length - 5, "and " );
        }

        //remove the last comma
        sequence = sequence.Remove( sequence.Length - 2, 1 );

        questionText.text = "Name the " + sequence + "note(s) in the " + strRoot + " major scale";
    }   

    private void SetAnswerText ( int root, int[] quiz )
    {
        string answer = "";
        int note = 0;
        
        for ( int i = 0; i < quiz.Length; i++ )
        {
            if ( quiz[i] == 0 )
            {
                continue;
            }

            if ( quiz[i] >= 7 )
            {
                Debug.Log("quiz[i] >= 7");
            }
            note = root + majorDistances[ quiz[i] ];

            if ( note > 11 )
            {
                note = note - 12;
            }           
            answer += ( notes.GetNoteCodes()[note] + ", " );
        }

        //remove the last comma
        answer = answer.Remove(answer.Length - 2, 1);
        answerText.text = answer;

    }    

    public void ShowAnswer ()
    {
        answerText.enabled = !answerText.enabled;
        if ( answerText.enabled )
        {
            GameObject.Find("ShowButton").GetComponentInChildren<Text>().text = "Hide Answer";
        }
        else
        {
            GameObject.Find("ShowButton").GetComponentInChildren<Text>().text = "Show Answer";
        }
    }

	// Use this for initialization
	void Start () 
	{
        GenerateQuestion();
        answerText.enabled = false;
	}

    
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
