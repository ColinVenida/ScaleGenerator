using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    private bool showMenu = false;
    private bool stopAnim = false;

    public Button menuBtn;
    public Button closeNavBtn;
    public GameObject menu;
    public int menuID;

    private Color WHITE = new Color( 1.0f, 1.0f, 1.0f );
    private Color DARK_GRAY = new Color( 0.5f, 0.5f, 0.5f );
   
    public void ToggleMenu ()
    {
        showMenu = !showMenu;
        menu.SetActive( showMenu );      

        if ( showMenu )
        {
            ChangeButtonColor( DARK_GRAY );
            closeNavBtn.interactable = false;
        }
        else
        {
            ChangeButtonColor( WHITE );
            closeNavBtn.interactable = true;
        }
    }	

    public void OpenMenu()
    {
        menu.SetActive( true );
        ChangeButtonColor( DARK_GRAY );
    }

    public void CloseMenu()
    {
        menu.SetActive( false );
        ChangeButtonColor( WHITE );
    }

    private void ChangeButtonColor( Color color )
    {
        Image menuBtnImage = menuBtn.image;
        menuBtnImage.color = color;
    }
}
