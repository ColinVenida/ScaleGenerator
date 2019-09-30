using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//behavior to move the scroll bar to the selected item
public class FocusScroller : MonoBehaviour
{

    public Dropdown parentDrop;

    private void OnEnable()
    {
        Debug.Log( "FocusScroller Enabled!" );
        Debug.Log( "parentDrop.value = " + parentDrop.value );
        //Debug.Log( "options.Count = " + parentDrop.options.Count );
        float scrollPos = ( parentDrop.value / parentDrop.options.Count );
        //parentDrop.GetComponentInChildren<Scrollbar>().value = scrollPos;
        //Debug.Log( "scrollPos = " + scrollPos );
        //Debug.Log( "ScrollBar.value = " + parentDrop.GetComponentInChildren<Scrollbar>().value );
        parentDrop.GetComponentInChildren<Scrollbar>().value = 0.5f;
    }

}
