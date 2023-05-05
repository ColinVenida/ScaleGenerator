﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuitString : MonoBehaviour
{
    public Fretboard fBoard;    
    public NoteArray noteArray;
    public Dropdown noteSelect;    
    public Button[] buttonArray;
    public Text[] textArray; 
    public Dropdown presetDrop;
    public int stringNumber;

    public static bool changePreset = true;  //variable for determining if the Preset Dropdown should be reset or not

    private Note currentTuning;

    private int[] arpeggioFrets = { 0, 0, 0, 0 };
    private string[] arpeggioNotes = { "", "", "", "" };

    public void CalculateFrets_New( int dropValue )
    {
        //workaround for avoiding the OnValueChanged event during Fretboard.Awake()
        if ( !fBoard.AreGuitStringsInitialized )
        {           
            return;
        }
        ResetUI();        
        SetCurrentTuning( dropValue );
        SaveTuning( dropValue );

        MusicScale currentScale = fBoard.CurrentMusicScale;

        int tuningScaleDegree = currentScale.FindNoteScaleDegree( currentTuning );
        int startDegree;
        int startFret;

        Debug.Log( "***new method! Calculating a string tuned to " + currentTuning + "***" );

        CompareGuitStringTuningToNoteInScale( currentScale, tuningScaleDegree, out startDegree, out startFret );

        int startDegree_new = CalculateStartScaleDegree( currentScale, tuningScaleDegree );
        Debug.Log( "GuitString " + currentTuning.ToString() + " should start at the " + startDegree_new + " degree" );

        UpdateButtonAndTextArray( currentScale, startFret, startDegree );
    }

    private int CalculateStartScaleDegree( MusicScale scale, int tuningScaleDegree )
    {
        //based on the currentTuning and the MusicScale, find out which scaleDegree we need to start at
        int startScaleDegree = tuningScaleDegree;
        Note noteInScale = scale.NotesInScale[tuningScaleDegree.ToString()];
        int overlapOffset = CalculateOverlapOffset( noteInScale );
       
        int nextScaleDegree = FindNextScaleDegree( tuningScaleDegree );        
        Note nextNote = scale.NotesInScale[nextScaleDegree.ToString()];

        if ( (noteInScale.PitchValue + overlapOffset) <= currentTuning.PitchValue )   
        {
            startScaleDegree = FindNextScaleDegree( startScaleDegree );
            if ( AreNotesEnharmonic( nextNote, currentTuning ) )
            {
                startScaleDegree = FindNextScaleDegree( startScaleDegree );
            }
        }     

        return startScaleDegree;
    }

    private int CalculateOverlapOffset( Note note )
    {
        int overlapOffset = 0;
        switch( note.pitchOverlap)
        {
            case PitchOverlap.FlatOverlap:
                overlapOffset = -12;
                break;
            case PitchOverlap.SharpOverlap:
                overlapOffset = 12;
                break;
        }
        return overlapOffset;
    }
    
    //test data:        
    //A# lydian: A#, B#, C##, D##, E#, F##, G##, E-tuning, D-tuning, F-tuning, Bb-tuning, Gb tuning, C-tuning
    //is there any flat note that's enharmonic to a double sharp??
    //Gb locrian:  Gb, Abb, Bbb, Cb, Dbb, Ebb, Fb, G-tuning, B-tuning, F#-tuning, 
    // C major: C, D, E, F, G, A, B, 
    private bool AreEnharmonicNotesPresent( MusicScale scale, int tuningScaleDegree )    
    {       
        int prevScaleDegree = FindPrevScaleDegree( tuningScaleDegree );
        int nextScaleDegree = FindNextScaleDegree( tuningScaleDegree );

        Note prevNote = scale.NotesInScale[prevScaleDegree.ToString()];
        Note nextNote = scale.NotesInScale[nextScaleDegree.ToString()];
        bool areEnharmonic = false;
                        
        areEnharmonic = ( currentTuning.PitchValue == nextNote.PitchValue || currentTuning.PitchValue == prevNote.PitchValue );        

        return areEnharmonic;
    }

    private bool AreNotesEnharmonic( Note first, Note second )
    {
        return first.PitchValue == second.PitchValue;
    }

    private int CalculateStartFret( int startScaleDegree )
    {
        //determine whether the scaleDegree == the tuningScaleDegree, then go from there (incomplete thought, sorry)
        int startFret = 0;

        return startFret;
    }

   
    //***LOGIC IS FLAWED, REFACTORING THIS METHOD WITH AreEnharmonicNotesPresent() and CalculateStartFret() METHODS***
    private void CompareGuitStringTuningToNoteInScale( MusicScale scale, int tuningScaleDegree, out int startScaleDegree, out int startFret )
    {
        startScaleDegree = tuningScaleDegree;
        startFret = 1;
        
        Note noteInScale = scale.NotesInScale[tuningScaleDegree.ToString()];

        //Debug.Log( "Are There Enharmonic notes? " + AreEnharmonicNotesPresent( scale, tuningScaleDegree ) );

        if ( currentTuning.Equals(noteInScale) )
        {            
            //note is in scale, adjust startDegree            
            startFret = scale.Formula.scaleIntervals[startScaleDegree.ToString()];
            startScaleDegree = FindNextScaleDegree( startScaleDegree );
        }
        else
        {            
            switch( currentTuning.pitch )
            {
                case PitchModifier.Flat:    //tuning is flat, but note is sharp or natural
                    //no need to change the scaleDegree
                    startFret = ResolveFlatTuning( scale, tuningScaleDegree );
                    break;
                case PitchModifier.Natural: //tuning is natural, but note is sharp or flat                    
                    startScaleDegree = ResolveNaturalTuning( scale, tuningScaleDegree );
                    break;
                case PitchModifier.Sharp:   //tuning is sharp, but note is flat or natural
                    startFret = ResolveSharpTuning( scale, tuningScaleDegree, ref startScaleDegree );
                    startScaleDegree = FindNextScaleDegree( startScaleDegree );    //always need to change which scaleDegree for a sharp tuning                
                    break;
            }
        }
    }

    private int FindNextScaleDegree( int startDegree )
    {
        int nextScaleDegree = startDegree + 1;
        if ( nextScaleDegree > 7 )
        {
            nextScaleDegree = 1;
        }
        return nextScaleDegree;
    }

    private int FindPrevScaleDegree( int startDegree )
    {
        int nextScaleDegree = startDegree - 1;
        if ( nextScaleDegree < 1 )
        {
            nextScaleDegree = 7;
        }
        return nextScaleDegree;
    }
       
    private int CalculateSemitoneOfNextInterval( MusicScale scale, int scaleDegree )
    {        
        int formulaInterval = scale.Formula.scaleIntervals[scaleDegree.ToString()];        
        int offset = 0;

        Note noteInScale = scale.NotesInScale[scaleDegree.ToString()];        
        switch ( noteInScale.pitch )
        {
            case PitchModifier.Flat:
                offset -= 1;
                break;
            case PitchModifier.Sharp:
                offset += 1;
                break;
        }
        
        int nextFret = formulaInterval + offset;
        return nextFret;
    }


    private int ResolveFlatTuning( MusicScale scale, int tuneInterval )
    {                
        int startFret = 1;
        Note noteInScale = scale.NotesInScale[tuneInterval.ToString()];
        
        //we only need to change startFret if it's sharp
        if ( noteInScale.pitch == PitchModifier.Sharp )
        {
            startFret = 2;
        }
        
        return startFret;
    }

    private int ResolveNaturalTuning( MusicScale scale, int tuningScaleDegree )
    {        
        int startScaleDegree = tuningScaleDegree;
        Note noteInScale = scale.NotesInScale[tuningScaleDegree.ToString()];

        if ( noteInScale.pitch == PitchModifier.Flat )
        {
            startScaleDegree = FindNextScaleDegree( startScaleDegree );            
        }

        //if the pitch is sharp, then we don't need to adjust the scaleDegree
        return startScaleDegree;
    }

    private int ResolveSharpTuning( MusicScale scale, int tuningScaleDegree, ref int startScaleDegree)
    {        
        int startFret = (CalculateSemitoneOfNextInterval( scale, startScaleDegree ) - 1);
                
        //check for enharmonic here
        if ( startFret == 0 )
        {            
            startScaleDegree = FindNextScaleDegree( startScaleDegree );
        }
        
        return startFret;
    }

    private void UpdateButtonAndTextArray( MusicScale scale, int startFret, int startScaleDegree )
    {
        int i = startFret - 1;
        int scaleDegree = startScaleDegree;
                
        try
        {
            while ( i < buttonArray.Length )
            {
                //Debug.Log( "i = " + i );
                string noteString = scale.NotesInScale[scaleDegree.ToString()].ToString();
                buttonArray[i].gameObject.SetActive( true );
                textArray[i].text = noteString;

                i = i + scale.Formula.scaleIntervals[scaleDegree.ToString()];
                scaleDegree = FindNextScaleDegree( scaleDegree );
            }
        }
        catch ( IndexOutOfRangeException e)
        {
            Debug.Log( "i is out of range of the array: " + i );
            Debug.Log( e.StackTrace );
        }       

        if ( fBoard.UseArpeggio() )
        {
            FilterArpeggio( fBoard.GetArplist() );
        }
    }
    
    private void ResetUI()
    {
        CancelArpeggio();
        DeactivateFrets();
        
        if ( changePreset )
        {
            presetDrop.value = 0;
        }
    }

    //*****OLD ALGORITHM GOING TO BE DEPRECATED*****
    public void CalculateFrets( int drop )
    {
        //CalculateFrets_New( drop );

        bool hasEnharmonic = false;
        bool includeFamilyNote = false;
       
        CancelArpeggio();

        //set the preset dropdown to default value (0) if the tuning was changed by the preset button
        if (changePreset)
        {
            presetDrop.value = 0;
        }
       
        SaveTuning( drop );       
        int currentFret = 0;   //set currentFret to 0 to represent the first fret
        
        int[] currentScale = SetCurrentScale(); 
        DeactivateFrets();       
                
        SetCurrentTuning( drop );
   
        //check whether the GuitString's tuning is enharmonic with any note in the noteArray
        //ie. if the GuitString is set A#, and the scale has Bb in it
        hasEnharmonic = CheckEnharmonic( noteSelect.value );        
        
        //find the family index of the GuitString's tuning
        //  the familyIndex will represent the open GuitString, then we can find the next fret        
        int familyIndex = fBoard.scaleGen.FindFamilyIndex( noteSelect.value );

        //find the interval of currentTuning in relation to the scale            
        int scaleInterval = FindScaleInterval( fBoard.rootDrop.value, familyIndex );

        //check if the note in the current scale is a different pitch than the currentTuning
        //ie. the tuning is C, and the note in the scale is C#
        if (currentTuning.usedSharp != noteArray.noteArray[familyIndex].usedSharp || 
                currentTuning.usedFlat != noteArray.noteArray[familyIndex].usedFlat )
        {            
            includeFamilyNote = true;
        }
               
        //***find out which fret to start on and which notes to display***
        //if we have to start on the note AFTER the tuning (ie. tuining is A, and we start on B)
        if ( !includeFamilyNote )
        {           
            familyIndex++;  //move the familyIndex to the next note
            scaleInterval++;    //move the scaleInterval to the next note

            scaleInterval = CheckScaleIntervalBounds( scaleInterval );

            if ( currentScale[scaleInterval] == 2 )
            {
                currentFret++;
            }            
        }
        else if ( hasEnharmonic )
        {
            familyIndex += 2;
            scaleInterval += 2;
            currentFret++;            
        }
        //if we have to include another version of the currentTuning's note (ie. a sharp or a flat)
        else
        {
            if (currentTuning.usedSharp)
            {
                familyIndex++;
                scaleInterval++;
            }
            else if ( !currentTuning.usedFlat && noteArray.noteArray[familyIndex].usedFlat )  //ie. tuning is B, but scale has a Bb in it, like F major scale
            {
                familyIndex++;
                scaleInterval++;                
            }
            //if the tuning used a flat, but scale had a natural, then we don't have to adjust the familyIndex, or the scaleInterval
        }

        //check for array bounds
        familyIndex = CheckFamilyIndexBounds( familyIndex );
        scaleInterval = CheckScaleIntervalBounds( scaleInterval );
        

        //run through the fret board and set the labels to the scale        
        while( currentFret < 24 )
        {
            //check if the next note is the same as the root note of the scale
            if (familyIndex == fBoard.scaleGen.GetRootIndex())
            {                
                buttonArray[currentFret].image.color = new Color( 0.0f, 0.6f, 0.8f );
            }
            else
            {
                textArray[currentFret].color = new Color( 0, 0, 0 );
            }

            //set the currentFret's text to the note in the scale
            buttonArray[currentFret].gameObject.SetActive( true );
            textArray[currentFret].text = fBoard.noteArray.noteArray[familyIndex].GetNote();
            familyIndex++;
            scaleInterval++;

            //check for array bounds
            familyIndex = CheckFamilyIndexBounds( familyIndex );
            scaleInterval = CheckScaleIntervalBounds( scaleInterval );

            //move to to the next fret according to the pattern
            currentFret += currentScale[scaleInterval];
        }        

        //check to set it back to arpeggio
        if ( fBoard.UseArpeggio() )
        {
            FilterArpeggio( fBoard.GetArplist() );
        }

    }//end CacculateFrets()

    private int[] SetCurrentScale()
    {
        int[] scale;
        switch ( fBoard.scaleDrop.value )
        {
            case 0:
                scale = fBoard.scaleGen.GetMajorFormula();
                break;
            case 1:
                scale = fBoard.scaleGen.GetMinorFormula();
                break;
            case 2:
                scale = fBoard.scaleGen.GetDorianFormula();
                break;
            case 3:
                scale = fBoard.scaleGen.GetPhrygianFormula();
                break;
            case 4:
                scale = fBoard.scaleGen.GetLydianFormula();
                break;
            case 5:
                scale = fBoard.scaleGen.GetMixolydianFormula();
                break;
            case 6:
                scale = fBoard.scaleGen.GetLocrianFormula();
                break;
            default:                
                scale = fBoard.scaleGen.GetMajorFormula();
                break;
        }
        return scale;
    }

    private void DeactivateFrets()
    {
        for ( int i = 0; i < textArray.Length; i++ )
        {
            buttonArray[i].gameObject.SetActive( false );
            buttonArray[i].image.color = new Color( 1.0f, 1.0f, 1.0f );
            textArray[i].text = "";
            textArray[i].color = new Color32( 0, 0, 0, 255 );
            textArray[i].fontStyle = FontStyle.Normal;
        }
    }

   
    private int CheckFamilyIndexBounds( int index )
    {
         if ( index > 6)
         {
            index = 0;
         }            
        return index;
    }

    private int CheckScaleIntervalBounds( int interval )
    {
        if ( interval > 7 )
        {
            interval = 1;
        }
        return interval;
    }

    public void FilterArpeggio( List<string> noteList )
    {
       
        for (int i = 0; i < buttonArray.Length; i++)
        {
            if (noteList.Contains( textArray[i].text ))
            {
                buttonArray[i].gameObject.SetActive( false );

            }
        }
    }

    //function to set all the fretArray to active
    public void CancelArpeggio()
    {        
        for (int i = 0; i < buttonArray.Length; i++)
        {
            if ( textArray[i].text != "" )
            {
                buttonArray[i].gameObject.SetActive( true );
            }            
        }
    }

    //function to check if the given note/tuning has enharmonic notes in the current scale
    private bool CheckEnharmonic( int tuning )
    {
        bool hasEnharmonic = false;
        //if the tuning is an enharmonic note, then check if its equivalent is in the scale

        switch ( tuning )
        {
            case 0:     // tuning is Ab, check for G#
                if (fBoard.noteArray.noteArray[6].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 16:    //  tuning is G#, check for Ab
                if (fBoard.noteArray.noteArray[0].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 2:     //  tuning is A#, check for Bb
                if (fBoard.noteArray.noteArray[1].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 3:     //  tuning is Bb, check for A#
                if (fBoard.noteArray.noteArray[0].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 6:     //  tuning is C#, check for Db
                if (fBoard.noteArray.noteArray[3].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 7:     //  tuning is Db, check for C#
                if (fBoard.noteArray.noteArray[2].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 9:     //  tuning is D#, check for Eb
                if (fBoard.noteArray.noteArray[4].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 10:    //  tuning is Eb, check for Db
                if (fBoard.noteArray.noteArray[3].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            case 13:    //  tuning is F#, check for Gb
                if (fBoard.noteArray.noteArray[6].usedFlat)
                {
                    hasEnharmonic = true;
                }
                break;
            case 14:    //  tuning is Gb, check for F#
                if (fBoard.noteArray.noteArray[5].usedSharp)
                {
                    hasEnharmonic = true;
                }
                break;
            default:
                break;
        }
        
        return hasEnharmonic;
    }

    //function that returns the root note's interval of the given family index
        //ie. what interval/position is "A" in the C scale (A is the 6th note in C scales)
    private int FindScaleInterval( int root, int familyIndex )
    {
        int interval = 0;        
        //find the FamilyIndex of the root note
        int rootFamily = fBoard.scaleGen.FindFamilyIndex( root );        
        
        do
        {
            rootFamily++;
            interval++;
            if (rootFamily > 6)
            {
                rootFamily = 0;
            }
        } while ( rootFamily != familyIndex );

        return interval;
    }
       
    private void SaveTuning( int drop )
    {
        switch( this.stringNumber )
        {
            case 0:
                PlayerPrefs.SetInt( "GuitStringOne", drop );                
                break;
            case 1:
                PlayerPrefs.SetInt( "GuitStringTwo", drop );
                break;
            case 2:
                PlayerPrefs.SetInt( "GuitStringThree", drop );
                break;
            case 3:
                PlayerPrefs.SetInt( "GuitStringFour", drop );
                break;
            case 4:
                PlayerPrefs.SetInt( "GuitStringFive", drop );
                break;
            case 5:                
                PlayerPrefs.SetInt( "GuitStringSix", drop );                
                break;
            case 6:               
                PlayerPrefs.SetInt( "GuitStringSeven", drop );                
                break;
            case 7:                
                PlayerPrefs.SetInt( "GuitStringEight", drop );               
                break;
        }
        PlayerPrefs.Save();        
    }

    //function that changes the current tuning to the given int value
        //int value comes from the noteSelect dropdown
    private void SetCurrentTuning ( int dropValue )
    {
        string noteName = NoteValues.ConvertNote_IntToString( dropValue );       
        currentTuning = new Note( noteName );
        
        //update the currentTuning's sharp/flat state
        switch ( dropValue )
        {
            //cases for flats
            case 0:
            case 3:
            case 7:
            case 10:
            case 14:
                currentTuning.usedSharp = false;
                currentTuning.usedFlat = true;
                break;

            //cases for sharps
            case 2:
            case 6:
            case 9:
            case 13:
            case 16:
                currentTuning.usedFlat = false;
                currentTuning.usedSharp = true;
                break;

            //case for natural
            default:
                currentTuning.usedSharp = false;
                currentTuning.usedFlat = false;
                break;
        }
    } 

    private void Awake()
    {
        currentTuning = new Note();
    }
}
