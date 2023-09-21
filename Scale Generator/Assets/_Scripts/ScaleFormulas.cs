using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScaleFormulas 
{
    public static readonly ScaleFormula MAJOR_IONIAN = new ScaleFormula ( "Major/Ionian", 
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, 
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE);

    public static readonly ScaleFormula MINOR_AEOLIAN = new ScaleFormula ( "Minor/Aeolian",
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, 
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula DORIAN = new ScaleFormula( "Dorian",
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula PHRYGIAN = new ScaleFormula( "Phrygian",
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE,
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula LYDIAN = new ScaleFormula( "Lydian",
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE );

    public static readonly ScaleFormula MIXOLYDIAN = new ScaleFormula( "Mixolydian",
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula LOCRIAN = new ScaleFormula( "Locrian",
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE );


    public static ScaleFormula GetFormulaFromDropValue( int value )
    {
        switch ( value )
        {
            case 0:
                return MAJOR_IONIAN;
            case 1:
                return MINOR_AEOLIAN;
            case 2:
                return DORIAN;
            case 3:
                return PHRYGIAN;
            case 4:
                return LYDIAN;
            case 5:
                return MIXOLYDIAN;
            case 6:
                return LOCRIAN;
            default:
                return MAJOR_IONIAN;
        }
    }

    public class ScaleFormula
    {
        public string scaleName { get; }
        public Dictionary<string, int> scaleIntervals { get; }

        public ScaleFormula( string name, int one, int two, int three, int four, int five, int six, int seven )
        {
            scaleName = name;

            //the distance between each of the scale's notes
            scaleIntervals = new Dictionary<string, int>()
            {
                { "1", one },
                { "2", two },
                { "3", three},
                { "4", four },
                { "5", five },
                { "6", six },
                { "7", seven },
            };                
        }
    }
    
}
