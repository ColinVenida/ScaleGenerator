using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindow : MonoBehaviour 
{
    private bool showMenu = false;
    private bool stopAnim = false;

    public GameObject menu;
    //public Animator anim;

    //function to toggle the menu
    public void ToggleMenu ()
    {
        showMenu = !showMenu;
        menu.SetActive( showMenu );
        if( showMenu )
        {
            //anim.Play( "Entry" );            
        }
    }

    

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        //if( anim.)
        //anim.StopPlayback();
    }
}
