using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleComparableManager : MonoBehaviour
{
    public List<ScaleComparable> scaleList;

    private int currentScaleIndex;
    private const int MINIMUM_SCALE_INDEX = 1;
    private const int MAXIMUM_SCALE_INDEX = 3;

    public void Start()
    {
        currentScaleIndex = 1;
        InitializeFromPlayerPrefs();
    }

    private void InitializeFromPlayerPrefs()
    {
        if ( PlayerPrefs.HasKey("ScaleComparableIndex") )
        {            
            ShowScaleComparablesBasedOnPlayerPref();
        }
        else
        {
            PlayerPrefs.SetInt( "ScaleComparableIndex", 1 );            
        }
    }

    private void ShowScaleComparablesBasedOnPlayerPref()
    {
        int playerPrefIndex = PlayerPrefs.GetInt( "ScaleComparableIndex" );

        while ( currentScaleIndex < playerPrefIndex )
        {
            currentScaleIndex++;
            scaleList[currentScaleIndex].gameObject.SetActive( true );
        }
    }

    public void AddScaleComparable()
    {
        if ( currentScaleIndex == MAXIMUM_SCALE_INDEX )
        {
            return;
        }

        currentScaleIndex++;
        PlayerPrefs.SetInt( "ScaleComparableIndex", currentScaleIndex );
        scaleList[currentScaleIndex].gameObject.SetActive( true );
    }

    public void RemoveScaleComparable()
    {
        if ( currentScaleIndex == MINIMUM_SCALE_INDEX )
        {
            return;
        }
        scaleList[currentScaleIndex].gameObject.SetActive( false );
        currentScaleIndex--;
        PlayerPrefs.SetInt( "ScaleComparableIndex", currentScaleIndex );
    }
}
