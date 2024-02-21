using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SecondaryDominantCalculator 
{
    private static List<string> OrderOfNotes = new List<string> { "A", "B", "C", "D", "E", "F", "G" };

    private static Dictionary<string, string> SecondaryDominants_NaturalNotes = new Dictionary<string, string>
    {
        { "A", "E" },
        { "B", "F#" },
        { "C", "G" },
        { "D", "A" },
        { "E", "B" },
        { "F", "C" },
        { "G", "D" },
    };

    private static Dictionary<string, string> SecondaryDominants_FlatNotes = new Dictionary<string, string>
    {
        { "Ab", "Eb" },
        { "Bb", "F" },
        { "Cb", "Gb" },
        { "Db", "Ab" },
        { "Eb", "Bb" },
        { "Fb", "Cb" },
        { "Gb", "Db" },
    };

    private static Dictionary<string, string> SecondaryDominants_DoubleFlatNotes = new Dictionary<string, string>
    {
        { "Abb", "Ebb" },
        { "Bbb", "Fb" },
        { "Cbb", "Gbb" },
        { "Dbb", "Abb" },
        { "Ebb", "Bbb" },
        { "Fbb", "Cbb" },
        { "Gbb", "Dbb" },
    };

    private static Dictionary<string, string> SecondaryDominants_SharpNotes = new Dictionary<string, string>
    {
        { "A#", "E#" },
        { "B#", "F##" },
        { "C#", "G#" },
        { "D#", "A#" },
        { "E#", "B#" },
        { "F#", "C#" },
        { "G#", "D#" },
    };

    private static Dictionary<string, string> SecondaryDominants_DoubleSharpNotes = new Dictionary<string, string>
    {
        { "A##", "E##" },
        { "B##", "F###" },
        { "C##", "G##" },
        { "D##", "A##" },
        { "E##", "B##" },
        { "F##", "C##" },
        { "G##", "D##" },
    };

    public static Note CalculateSecondaryDominant ( Note note )
    {
        string nameOfFifth = GetAppropriateSecdonaryDominantDictionary( note.ToString() )[note.ToString()];
        return new Note( nameOfFifth );
    }

    private static Dictionary<string, string> GetAppropriateSecdonaryDominantDictionary( string noteName )
    {
        Dictionary<string, string> secDomDictionary;
        int nameLength = noteName.Length;

        switch ( nameLength )
        {
            case 1:
                secDomDictionary = SecondaryDominants_NaturalNotes;
                break;

            case 2:
                if ( noteName[1] == '#' )
                {
                    secDomDictionary = SecondaryDominants_SharpNotes;
                }
                else
                {
                    secDomDictionary = SecondaryDominants_FlatNotes;
                }
                break;

            case 3:                
                if ( noteName[1] == '#' )
                {
                    secDomDictionary = SecondaryDominants_DoubleSharpNotes;
                }
                else
                {
                    secDomDictionary = SecondaryDominants_DoubleFlatNotes;
                }                
                break;

            default:
                secDomDictionary = SecondaryDominants_NaturalNotes;
                Debug.Log( "Secondary Dominant calculator error.  Given note name is too long." );
                break;
        }

        return secDomDictionary;
    }
}
