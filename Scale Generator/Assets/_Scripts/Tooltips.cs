using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltips : MonoBehaviour
{
    public Text currentScale;
    public Text scaleTitle;
    public Text tip1;
    public Text tip2;
    public Text tip3;
    public Text tipNumber;
    public Dropdown scaleDrop;


    private int tipIndex;
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
                scaleTitle.text = "The Major (Ionian) Scale";
                break;
            case 1:
                scaleTitle.text = "The Minor (Aeolian) Scale";
                break;
            case 2:
                scaleTitle.text = "The Dorian Mode";
                break;
            case 3:
                scaleTitle.text = "The Phrygian Mode";
                break;
            case 4:
                scaleTitle.text = "The Lydian Mode";
                break;
            case 5:
                scaleTitle.text = "The Mixolydian Mode";
                break;
            case 6:
                scaleTitle.text = "The Locrian Mode";
                break;
        }

        tip1.text = firstTips[scale];
        tip2.text = secondTips[scale];
        tip3.text = thirdTips[scale];
    }

    public void UpdateCurrentScale( int scale )
    {
        //Debug.Log( "UpdateCurrentText: " + scale );
        switch (scale)
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
                currentScale.text = "Current Scale: Phrygian";
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

        //update the tipIndex and tipNumber
        tipIndex = scale;
        tipNumber.text = ( tipIndex + 1 ).ToString() + "/7";
    }

    //function to cycle the tips forwards
    public void NextTip()
    {
        tipIndex++;
        if ( tipIndex > 6 )
        {
            tipIndex = 0;
        }
        tipNumber.text = ( tipIndex + 1 ).ToString() + "/7";
        ChangeTips( tipIndex );
    }

    //function to cycle the tips backwards
    public void PrevTip()
    {
        tipIndex--;
        if (tipIndex < 0)
        {
            tipIndex = 6;
        }
        tipNumber.text = ( tipIndex + 1 ).ToString() + "/7";
        ChangeTips( tipIndex );
    }
        
    public int GetTipIndex()
    {
        return tipIndex;
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
            "-Dorian sounds \"smooth,\" \"mellow,\" \"groovy\"",
            "-Phrygian sounds \"exotic,\" \"otherwordly,\" \"tense\"",
            "-Lydian sounds \"dreamy,\" \"floaty,\" \"adventurous\"",
            "-Mixolydian sounds \"bright,\" \"upbeat,\" \"rockin!\"",
            "-Locrian sounds \"unsettling,\" \"unresolved,\" \"evil\""
        };

        thirdTips = new string[7]
        {
            "-Ionian sounds \"bright,\" \"happy,\" \"joyous\"\n-Example Songs: Tom Petty - \"Free Fallin,\" Eric Johnson - \"Cliffs Of Dover,\" Axis of Awesome - \"4 Chords\"",
            "-Aeolian sounds \"dark,\" \"sad,\" \"somber\"\n-Example Songs: REM - \"Losing My Religion,\" Bob Dylan - \"All Along the Watchtower,\" Rolling Stones - \"Gimmie Shelter\"",
            "-Example Songs: Pink Floyd - \"The Wall: Pt 2,\" Pink Floyd - \"Breath,\" Carlos Santana - \"Oye Como Va\"",
            "-Example Songs: Coheed and Cambria - \"Welcome Home\" (intro and verse), Megadeth - \"Symphony of Destruction,\" Missy Elliot - \"Get Ur Freak On\"",
            "-Example Songs: Joe Satriani - \"Flying in a Blue Dream,\" Frank Zappa - \"Zoot Alures,\" The Simpson's Theme Song",
            "-Example Songs:  AC/DC - \"Highway to Hell,\" Aerosmith - \"Walk This Way,\" Lady Gaga - \"Born This Way\" ",
            "-Example Songs: John Kirkpatrick - \"Dust to Dust,\" Bj\u00F6rk - \"Army of Me\" (just the verse), The Strokes - \"Juice Box\" (just the bass)"
        };

        tipIndex = scaleDrop.value;
        tipNumber.text = ( tipIndex + 1 ).ToString() + "/7";

        //call the ChangeTips function after everything has been initialized
        ChangeTips( scaleDrop.value );       
    }

}
