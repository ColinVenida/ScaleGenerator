using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleComparable : MonoBehaviour
{
    public Dropdown rootDrop;
    public Dropdown scaleDrop;
    public Toggle secondaryDomToggle;
    public TextFormatter textFormatter;
    public DisplayScale displayScale;
    public List<GameObject> secondaryDomObjects;

    private MusicScale musicScale;

    public void Start()
    {
        string root = NoteValues.ConvertNote_IntToString( rootDrop.value );
        ScaleFormulas.ScaleFormula formula = ScaleFormulas.GetFormulaFromDropValue( scaleDrop.value );
        musicScale = new MusicScale( root, formula );
    }

    public void UpdateDispalyScale()
    {
        //displayScale.
    }

}
