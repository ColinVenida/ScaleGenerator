using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour 
{

    public void LoadFretScene ()
    {
        SceneManager.LoadSceneAsync( "VirtualFretboard" );
    }

    public void LoadScaleComparerScene ()
    {
        SceneManager.LoadSceneAsync( "ScaleComparer" );
    }

    public void LoadQuizScene ()
    {
        SceneManager.LoadSceneAsync( "QuizScene" );
    }


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
