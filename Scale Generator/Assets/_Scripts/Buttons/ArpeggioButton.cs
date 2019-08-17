using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArpeggioButton : MonoBehaviour
{
    public int scale;
    public Fretboard fBoard;
    public Dropdown scaleDrop;

    public void FilterScale()
    {
        scaleDrop.value = scale;
        fBoard.SetArpeggio();
        fBoard.textForm.ShowArpeggio( true );

    }
}
