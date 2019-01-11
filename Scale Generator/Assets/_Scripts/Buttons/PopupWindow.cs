using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindow : MonoBehaviour 
{
    private bool showMenu = false;

    public GameObject menu;

    //function to toggle the menu
    public void ToggleMenu ()
    {
        showMenu = !showMenu;
        menu.SetActive( showMenu );
    }

    

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
