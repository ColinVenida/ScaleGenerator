using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PitchValues 
{
    public const int A = 1;
    public const int B = 3;
    public const int C = 4;
    public const int D = 6;
    public const int E = 8;
    public const int F = 9;
    public const int G = 11;

    public static int AssignPitchValue( Note note )
    {
        int value = 0;
        switch ( note.NaturalName )
        {
            case "A":
                value = A;
                break;
            case "B":
                value = B;
                break;
            case "C":
                value = C;
                break;
            case "D":
                value = D;
                break;
            case "E":
                value = E;
                break;
            case "F":
                value = F;
                break;
            case "G":
                value = G;
                break;            
        }
        return value;
    }
}
