using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PopupWindow without references to NavMenu components
public class SimplePopupWindow : MonoBehaviour
{
    private bool isActive;

    public void Start()
    {
        isActive = this.gameObject.activeSelf;
    }
    public void ToggleMenu()
    {
        if ( !isActive )
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
    private void Activate()
    {
        isActive = true;
        this.gameObject.SetActive( true );
    }

    private void Deactivate()
    {
        isActive = false;
        this.gameObject.SetActive( false );
    }
}
