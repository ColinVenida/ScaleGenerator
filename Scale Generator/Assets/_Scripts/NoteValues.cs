using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoteValues
{
    public const int A_flat = 0;
    public const int A_natural = 1;
    public const int A_sharp = 2;

    public const int B_flat = 3;
    public const int B_natural = 4;

    public const int C_natural = 5;
    public const int C_sharp = 6;

    public const int D_flat = 7;
    public const int D_natural = 8;
    public const int D_sharp = 9;

    public const int E_flat = 10;
    public const int E_natural = 11;

    public const int F_natural = 12;
    public const int F_sharp = 13;

    public const int G_flat = 14;
    public const int G_natural = 15;
    public const int G_sharp = 16;

    public static string ConvertNote_IntToString( int value )
    {
        string noteName = "";
        switch( value )
        {
            case 0:
                noteName = "Ab";
                break;
            case 1:
                noteName = "A";
                break;
            case 2:
                noteName = "A#"
                break;
            case 3:
                noteName = "Bb";
                break;
            case 4:
                noteName = "B";
                break;
            case 5:
                noteName = "C";
                break;
            case 6:
                noteName = "C#";
                break;
            case 7:
                noteName = "Db";
                break;
            case 8:
                noteName = "D";
                break;
            case 9:
                noteName = "D#";
                break;
            case 10:
                noteName = "Eb";
                break;
            case 11:
                noteName = "E";
                break;
            case 12:
                noteName = "F";
                break;
            case 13:
                noteName = "F#";
                break;
            case 14:
                noteName = "Gb";
                break;
            case 15:
                noteName = "G";
                break;
            case 16:
                noteName = "G#";
                break;
            default:
                noteName = "invalid";
                break;
        }
        return noteName;
    }
}
