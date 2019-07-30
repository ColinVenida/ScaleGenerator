using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltips : MonoBehaviour
{
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

        //switch statement if we need it???

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
            "-Locrean is a minor scale with flat 2nd, and flat 5th notes."
        };

        secondTips = new string[7]
        {
            "-W is a \"Whole Step\" (2 Frets), H is a \"Half-Step\" (1 Fret) on the Fretboard.",
            "-The difference between major and minor scale is minor has a flat 3rd, 6th, and 7th note.",
            "-[Dorian Description]",
            "-[Phrygian Description]",
            "-[Lydian Description]",
            "-[Mixolydian Description]",
            "-[Locrean Description]"
        };

        thirdTips = new string[7]
        {
            "-[Ionian Example Songs]",
            "-[Aeolian Example Songs]",
            "-[Dorian Example Songs]",
            "-[Phrygian Example Songs]",
            "-[Lydian Example Songs]",
            "-[Mixolydian Example Songs]",
            "-[Locrean Example Songs]"
        };

        //call the ChangeTips function after everything has been initialized
        ChangeTips( scaleDrop.value );
    }

}
