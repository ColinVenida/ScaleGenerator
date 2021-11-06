using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindow : MonoBehaviour 
{
    private bool showMenu = false;
    private bool stopAnim = false;

    public GameObject menu;
   
    public void ToggleMenu ()
    {
        showMenu = !showMenu;
        menu.SetActive( showMenu );       
    }	
}
