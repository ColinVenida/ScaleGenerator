﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleQuiz : MonoBehaviour
{

    private int[] quizIndexes = { 0, 0, 0, 0 };
                                                
    private int rootNote;
    private System.Random rnd = new System.Random();
    private string[] currentScale;

    public Note notes;
    public ScaleGenerator scaleGen;
    public NoteArray noteArray;
    public Text questionText;
    public Text answerText;
    public Toggle[] keyToggles;
    public Toggle halfToggle;
    public Dropdown scaleDrop;
     
    public void GenerateQuestion ()
    {
        //set rootNote of one of the whole notes 
        int rootNote = GenerateRoot();

        //create a new scale        
        scaleGen.GenerateScale( rootNote, scaleDrop.value, noteArray );

        //get  1-4 random values, values range form 1-7, values can't be equal twice in a row
        int totalPositions = rnd.Next (1, 5 );     
        
        //if the answer is still showing, hide it for next question
        if( answerText.enabled )
        {
            ShowAnswer();
        }

        //set the first index of quizIndexes
        quizIndexes[0] = rnd.Next( 1, 7 );

        for ( int i = 1; i < totalPositions; i++ )
        {
            int position = 0;

            do
            {
                position = rnd.Next(1, 7);
            }
            while ( position == quizIndexes[i-1] );

            quizIndexes[i] = position;
            
        }

        //zero out the remaining array indexes
        if (totalPositions < quizIndexes.Length)
        {
            for (int j = totalPositions; j < quizIndexes.Length; j++)
            {
                quizIndexes[j] = 0;
            }
        }

        //find the associated notes based on random values and replace question and answer text
        SetQuestionText( rootNote, quizIndexes );
        SetAnswerText( rootNote, quizIndexes );
                
    }


    //function to generate a root note that respects the filters    
    private int GenerateRoot ()
    {        
        int key = 0;
        int root = 0;

        //find what key to set the quiz in based on the active toggles
        do
        {
            key = rnd.Next( 0, 7 );
        }
        while (!keyToggles[key].isOn);

        //convert that Key to another int to give to the ScaleGenerator
        switch ( key )
        {
            case 0:     
                root = 1;   //set to A natural
                break;
            case 1:     
                root = 4;   //set to B
                break;
            case 2:     
                root = 5;   //set to C
                break;
            case 3:
                root = 8;   //set to D
                break;
            case 4:
                root = 11;    //set to E
                break;
            case 5:
                root = 12;    //set to F
                break;
            case 6:
                root = 15;    //set to G
                break;            
            default:
                Debug.Log( "default statement, defaulting to C" );
                root = 5;
                break;
        }

        //if the toggle is on, then add a sharp, flat, or nothing
        if ( halfToggle.isOn )
        {
            root += rnd.Next( -1, 2 );  
        }       
                
        return root;
    }

    private void SetQuestionText ( int root, int[] quiz )
    {
        string strRoot = "";
        string sequence = "";
        string scale;

        //find the starting note/root note
        int familyIndex = scaleGen.FindFamilyIndex( root );
        strRoot = noteArray.noteArray[familyIndex].GetNote();
                
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

        switch( scaleDrop.value )
        {
            case 0:
                scale = " Major (Ionian) scale";
                break;
            case 1:
                scale = " Minor (Aeolian) scale";
                break;
            case 2:
                scale = " Dorian scale";
                break;
            case 3:
                scale = " Phrygian scale";
                break;
            case 4:
                scale = " Lydian scale";
                break;
            case 5:
                scale = " Mixolydian scale";
                break;
            case 6:
                scale = " Locrian scale";
                break;

            default:
                Debug.Log( "default statement in SetQuestionText()" );
                scale = " major scale";
                break;
        }
        questionText.text = "Name the " + sequence + "note(s) in the " + strRoot + scale;
    }   

    private void SetAnswerText ( int root, int[] quiz )
    {
        string answer = "";
        int familyIndex = scaleGen.FindFamilyIndex( root );
        for (int i = 0; i < quiz.Length; i++)
        {
            
            if (quiz[i] == 0)
            {
                continue;
            }

            if (quiz[i] >= 7)
            {
                Debug.Log( "quiz[i] >= 7" );
            }

            int index = quiz[i] + familyIndex;
            if ( index > 6 )
            {
                index -= 7;
            }            
            answer += noteArray.noteArray[index].GetNote() + ", ";
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
