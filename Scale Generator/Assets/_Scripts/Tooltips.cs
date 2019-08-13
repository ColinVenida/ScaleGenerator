﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltips : MonoBehaviour
{
    public Text currentScale;
    public Text tip1;
    public Text tip2;
    public Text tip3;
    public Dropdown scaleDrop;

    private string[] firstTips;
    private string[] secondTips;
    private string[] thirdTips;



    public void ChangeTips( int scale )
    {
        //check if the toolTip arrays are populated
        if (firstTips == null)
        {
            return;
        }

        //switch statement
        switch ( scale )
        {
            case 0:
                currentScale.text = "Current Scale: Major (Ionian)";
                break;
            case 1:
                currentScale.text = "Current Scale: Minor (Aeolian)";
                break;
            case 2:
                currentScale.text = "Current Scale: Dorian";
                break;
            case 3:
                currentScale.text = "Current Scale:  Phrygian";
                break;
            case 4:
                currentScale.text = "Current Scale: Lydian";
                break;
            case 5:
                currentScale.text = "Current Scale: Mixolydian";
                break;
            case 6:
                currentScale.text = "Current Scale: Locrian";
                break;
        }

        tip1.text = firstTips[scale];
        tip2.text = secondTips[scale];
        tip3.text = thirdTips[scale];
    }


    void Awake()
    {
        //populate the toolTip arrays
        firstTips = new string[7]
        {
            "-The formula for Major Scale:  [Root]-W-W-H-W-W-W-H-[Octave]",
            "-The formula for Minor Scale: [Root]-W-H-W-W-H-W-W-[Octave]",
            "-Dorian is a minor scale with sharp 6th note.",
            "-Phrygian is a minor scale with a flat 2nd note note.",
            "-Lydian is a major scale with a sharp 4th note.",
            "-Mixolydian is a major scale with a flat 7th note.",
            "-Locrian is a minor scale with flat 2nd, and flat 5th notes."
        };

        secondTips = new string[7]
        {
            "-W is a \"Whole Step\" (2 Frets), H is a \"Half-Step\" (1 Fret) on the Fretboard.",
            "-The difference between major and minor scale is minor has a flat 3rd, 6th, and 7th note.",
            "-Dorian sounds \"Smooth,\" \"Mellow,\" \"Groovy,\"",
            "-Phrygian sounds \"Exotic,\" \"Otherwordly,\" \"Tense,\"",
            "-Lydian sounds \"Dreamy,\" \"Floaty,\" \"Adventurous,\"",
            "-Mixolydian sounds \"Bright,\" \"Upbeat,\" \"Rockin!\"",
            "-Locrian sounds \"Unsettling,\" \"Unresolved,\" \"Evil\""
        };

        thirdTips = new string[7]
        {
            "-Ionian sounds \"Bright,\" \"Happy,\" \"Joyous\"\n-Example Songs: Tom Petty - \"Free Fallin,\" Eric Johnson - \"Cliffs Of Dover,\" Axis of Awesome - \"4 Chords\"",
            "-Aeolian sounds \"Dark,\" \"Sad,\" \"Somber\"\n-Example Songs: REM - \"Losing My Religion,\" Bob Dylan - \"All Along the Watchtower,\" Rolling Stones - \"Gimmie Shelter\"",
            "-Example Songs: Pink Floyd - \"The Wall: Pt 2,\" Pink Floyd - \"Breath,\" Carlos Santana - \"Oye Como Va\"",
            "-Example Songs: Coheed and Cambria - \"Welcome Home\" (intro and verse), Megadeth - \"Symphony of Destruction,\" Missy Elliot - \"Get Ur Freak On\"",
            "-Example Songs: Joe Satriani - \"Flying in a Blue Dream,\" Frank Zappa - \"Zoot Alures,\" The Simpson's Theme Song",
            "-Example Songs:  AC/DC - \"Highway to Hell,\" Aerosmith - \"Walk This Way,\" Lady Gaga - \"Born This Way\" ",
            "-Example Songs: John Kirkpatrick - \"Dust to Dust,\" Bj\u00F6rk - \"Army of Me\" (just the verse), The Strokes - \"Juice Box\" (just the bass)"
        };

        //call the ChangeTips function after everything has been initialized
        ChangeTips( scaleDrop.value );
    }

}
