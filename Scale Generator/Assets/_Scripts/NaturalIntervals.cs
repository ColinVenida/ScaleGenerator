using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the distance between natural notes in music
public static class NaturalIntervals 
{
    public struct Interval
    {       
        public Interval( int prev, int next )
        {
            PrevNoteSemitone = prev;
            NextNoteSemitone = next;
        }

        public int PrevNoteSemitone { get; }
        public int NextNoteSemitone { get; }
    }

    public static Dictionary<string, Interval> naturalIntervals = new Dictionary<string, Interval>
    {
        { "A", new Interval( 2, 2 ) },
        { "B", new Interval( 2, 1 ) },
        { "C", new Interval( 1, 2 ) },
        { "D", new Interval( 2, 2 ) },
        { "E", new Interval( 2, 1 ) },
        { "F", new Interval( 1, 2 ) },
        { "G", new Interval( 2, 2 ) },
    };
}
