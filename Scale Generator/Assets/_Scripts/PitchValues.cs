using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Values to represent notes as integer/indexes WITH overlap.  Used for evaluating enharmonic notes
//ie.
//  G#/Ab = 0 (same value, meaning they are enharmonic),
//  A = 1, 
//  A#/Bb = 2
//  B = 3
public static class PitchValues 
{   
    public static Dictionary<string, int> PitchValueDicitonary_NaturalNotes = new Dictionary<string, int>()
    {
        { "A", 1 },
        { "B", 3 },
        { "C", 4 },
        { "D", 6 },
        { "E", 8 },
        { "F", 9 },
        { "G", 11 },
    };

    public const int LOWER_LIMIT = 0;
    public const int UPPER_LIMIT = 12;    
}
