using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleComparable : MonoBehaviour
{
    public Dropdown rootDrop;
    public Dropdown scaleDrop;
    public Toggle secondaryDomToggle;
    public Text locrianWarning;
    public DisplayScale displayScale;
    public List<GameObject> secondaryDomObjects;
    public int playerPrefID;

    private string playerPrefName_root = "ScaleComparable_Root_";
    private string playerPrefName_scale = "ScaleComparable_Scale_";
    private MusicScale musicScale;
    private List<Note> secondaryDominantNotes;
    private bool isInitialized = false;

    public void Start()
    {
        secondaryDominantNotes = new List<Note>();
        playerPrefName_root = "ScaleComparable_Root_" + playerPrefID;
        playerPrefName_scale = "ScaleComparable_Scale_" + playerPrefID;

        InitializeDropdownSettingFromPlayerPref();
        //leave the secondaryDomToggle off by default?
        CalculateScale();        
    }

    private void InitializeDropdownSettingFromPlayerPref()
    {
        if ( PlayerPrefs.HasKey(playerPrefName_root) )
        {
            rootDrop.value = PlayerPrefs.GetInt( playerPrefName_root );
            scaleDrop.value = PlayerPrefs.GetInt( playerPrefName_scale );
        }
        else
        {
            PlayerPrefs.SetInt( playerPrefName_root, rootDrop.value );
            PlayerPrefs.SetInt( playerPrefName_scale, scaleDrop.value );
        }
        isInitialized = true;
    }  

    public void CalculateScale()
    {
        if ( !isInitialized )
        {
            return;
        }
        string root = NoteValues.ConvertNote_IntToString( rootDrop.value );
        ScaleFormulas.ScaleFormula formula = ScaleFormulas.GetFormulaFromDropValue( scaleDrop.value );

        PlayerPrefs.SetInt( playerPrefName_root, rootDrop.value );
        PlayerPrefs.SetInt( playerPrefName_scale, scaleDrop.value );

        musicScale = new MusicScale( root, formula );

        ToggleSecondaryDominants();
        if ( UsingLocrian() )
        {            
            Debug.Log( "Using Locrian!" );            
            return;
        }
        else
        {
            CalculateSecondaryDominants();
            UpdateDisplayScale();
            UpdateSecondaryDominantDisplay();
        }        
    }

    private bool UsingLocrian()
    {
        return ( ScaleFormulas.GetFormulaFromDropValue( scaleDrop.value ).scaleName == ScaleFormulas.LOCRIAN.scaleName );
    }

    public void ToggleSecondaryDominants()
    {
        if ( UsingLocrian() )
        {
            locrianWarning.gameObject.SetActive( secondaryDomToggle.isOn );
            foreach ( GameObject obj in secondaryDomObjects )
            {
                obj.SetActive( false );
            }
        }
        else
        {
            locrianWarning.gameObject.SetActive( false );
            foreach ( GameObject obj in secondaryDomObjects )
            {
                obj.SetActive( secondaryDomToggle.isOn );
            }
        }
    }

    private void CalculateSecondaryDominants()
    {
        secondaryDominantNotes.Clear();
        int semitonesToFifth = 7;
        AddSecondaryDominantNotes( semitonesToFifth );        
    }

    private void AddSecondaryDominantNotes( int semitonesToFifth )
    {
        for ( int i = 1; i < musicScale.NotesInScale.Count + 1; i++ )
        {
            Note noteInScale = musicScale.NotesInScale[i.ToString()];
            Note secondaryDominant = SecondaryDominantCalculator.CalculateSecondaryDominant( noteInScale );
            secondaryDominantNotes.Add( secondaryDominant );
        }
        Note octave = secondaryDominantNotes[0];
        secondaryDominantNotes.Add( octave );
    }    
       
    private void UpdateDisplayScale()
    {
        displayScale.UpdateNotes_NoScaleDegreeSymbols( musicScale, scaleDrop.value );
    }

    private void UpdateSecondaryDominantDisplay()
    {
        for( int i = 0; i < secondaryDomObjects.Count; i++ )
        {            
            secondaryDomObjects[i].GetComponent<Text>().text = secondaryDominantNotes[i].ToString();
        }      
    }    
}
