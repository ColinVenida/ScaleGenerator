using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScaleList 
{
    // Start is called before the first frame update
    private List<MusicScale> scaleList;
    public List<MusicScale> ScaleList { get { return scaleList; } }

    private bool includeSharpKeys = false;
    private bool includeFlatKeys = false;

    public MusicScaleList()
    {
        scaleList = new List<MusicScale>();       
    }

    public void ToggleScale( string root, ScaleFormulas.ScaleFormula formula )
    {
        if ( !ContainsScale( root ) )
        {
            AddScales( root, formula );
        }
        else
        {
            RemoveScales( root );
        }        
    }

    public void PrintScaleList()
    {
        Debug.Log( "===scaleList includes===" );

        foreach ( MusicScale scale in scaleList )
        {
            Debug.Log( scale.rootNote + " " + scale.Formula.ToString() );
        }
    }

    private bool ContainsScale( string rootNote )
    {
        bool hasScale = false;
        foreach ( MusicScale scale in scaleList )
        {
            if( scale.rootNote == rootNote )
            {
                hasScale = true;
            }
        }
        return hasScale;
    }

    public void AddScales( string root, ScaleFormulas.ScaleFormula formula )
    {        
        MusicScale scale = new MusicScale( root, formula );
        if ( !scale.IsTheoretical )
        {
            scaleList.Add( new MusicScale( root, formula ) );
        }        

        if ( includeSharpKeys )
        {
            MusicScale sharpScale = new MusicScale( root + "#", formula );
            if ( !sharpScale.IsTheoretical )
            {
                scaleList.Add( new MusicScale( root + "#", formula ) );
            }
        }
        if ( includeFlatKeys )
        {
            MusicScale flatScale = new MusicScale( root + "b", formula );
            if ( !flatScale.IsTheoretical )
            {
                scaleList.Add( new MusicScale( root + "b", formula ) );
            }
        }
    }

    public void RemoveScales( string root )
    {
        RemoveScaleByRootNote( root );

        if ( includeSharpKeys )
        {
            RemoveScaleByRootNote( root + "#" );
        }
        if ( includeFlatKeys )
        {
            RemoveScaleByRootNote( root + "b" );
        }       
    }

    private void RemoveScaleByRootNote( string root )
    {
        int lastIndex = scaleList.Count - 1;

        for ( int i = lastIndex; i >= 0; i-- )
        {
            if ( scaleList[i].rootNote == root )
            {
                scaleList.RemoveAt( i );
            }
        }
    }

    public void ToggleSharpKeys( ScaleFormulas.ScaleFormula formula )
    {
        includeSharpKeys = !includeSharpKeys;

        if ( includeSharpKeys )
        {
            AddSharpKeys( formula );
        }
        else 
        {
            RemoveSharpKeys();
        }
    }
    private void AddSharpKeys( ScaleFormulas.ScaleFormula formula )
    {
        int currentCount = scaleList.Count;

        for( int i = 0; i < currentCount; i++ )
        {
            if ( scaleList[i].NotesInScale[1.ToString()].pitch == PitchModifier.Natural )
            {
                string sharpRootNote = scaleList[i].rootNote + "#";
                scaleList.Add( new MusicScale( sharpRootNote, formula ) );
            }            
        }       
    }

    private void RemoveSharpKeys()
    {
        int lastIndex = scaleList.Count -1;

        for ( int i = lastIndex; i >= 0; i-- )
        {
            if ( scaleList[i].NotesInScale[1.ToString()].pitch == PitchModifier.Sharp )
            {
                scaleList.RemoveAt( i );
            }
        }
    }

    public void ToggleFlatKeys( ScaleFormulas.ScaleFormula formula )
    {
        includeFlatKeys = !includeFlatKeys;

        if ( includeFlatKeys )
        {
            AddFlatKeys( formula );
        }
        else
        {
            RemoveFlatKeys();
        }
    }

    public void AddFlatKeys( ScaleFormulas.ScaleFormula formula )
    {
        int currentCount = scaleList.Count;

        for ( int i = 0; i < currentCount; i++ )
        {            
            if ( scaleList[i].NotesInScale[1.ToString()].pitch == PitchModifier.Natural )
            {
                string sharpRootNote = scaleList[i].rootNote + "b";
                scaleList.Add( new MusicScale( sharpRootNote, formula ) );
            }
            
        }        
    }

    public void RemoveFlatKeys()
    {
        int lastIndex = scaleList.Count - 1;
        for ( int i = lastIndex; i >= 0; i-- )
        {
            if ( scaleList[i].NotesInScale[1.ToString()].pitch == PitchModifier.Flat )
            {
                scaleList.RemoveAt( i );
            }
        }
    }
}
