using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FretButton_ScaleDegree : MonoBehaviour
{
    public Fretboard fBoard;
    public int degree;

    public void HighlightColor()
    {
        fBoard.HighlightFrets( degree );
    }
}
