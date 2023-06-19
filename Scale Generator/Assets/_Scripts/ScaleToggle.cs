using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToggle : MonoBehaviour
{
    public string scaleRootNote;
    public ScaleQuiz scaleQuiz;

    public void ToggleScale()
    {
        scaleQuiz.ToggleScale( scaleRootNote );
    }    

    public void ToggleSharps()
    {
        scaleQuiz.ToggleSharpKeys();
    }

    public void ToggleFlats()
    {
        scaleQuiz.ToggleFlatKeys();
    }
}
