using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    private bool showMenu = false;
    private bool stopAnim = false;

    public Button menuBtn;
    public GameObject menu;

    private Color WHITE = new Color( 1.0f, 1.0f, 1.0f );
    private Color DARK_GRAY = new Color( 0.5f, 0.5f, 0.5f );
   
    public void ToggleMenu ()
    {
        showMenu = !showMenu;
        menu.SetActive( showMenu );      

        if ( showMenu )
        {
            ChangeButtonColor( DARK_GRAY );
        }
        else
        {
            ChangeButtonColor( WHITE );
        }
    }	

    private void ChangeButtonColor( Color color )
    {
        Image menuImage = menuBtn.image;
        menuImage.color = color;
    }
}
