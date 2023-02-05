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

    public static readonly ScaleFormula DORIAN = new ScaleFormula( "DORIAN",
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula PHRYGIAN = new ScaleFormula( "PHRYGIAN",
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE,
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula LYDIAN = new ScaleFormula( "LYDIAN",
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE );

    public static readonly ScaleFormula MIXOLYDIAN = new ScaleFormula( "MIXOLYDIAN",
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE );

    public static readonly ScaleFormula LOCRIAN = new ScaleFormula( "LOCRIAN",
        ToneTypes.SEMI_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.SEMI_TONE,
        ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE, ToneTypes.WHOLE_TONE );

    public class ScaleFormula
    {
        public string scaleName { get; }
        public Dictionary<string, int> intervalDistances { get; }

        public ScaleFormula( string name, int one, int two, int three, int four, int five, int six, int seven )
        {
            scaleName = name;
            intervalDistances = new Dictionary<string, int>()
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
