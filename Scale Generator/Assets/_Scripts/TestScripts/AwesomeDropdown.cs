using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwesomeDropdown : MonoBehaviour
{
    public Image dropImage;

    private bool clicked = false;
    public void ChangeDropColor()
    {
        Debug.Log( "changing color" );
        dropImage.color = new Color( 0.4f, 0.4f, 0.7f );
    }

    public void ResetColor()    
    {
        Debug.Log( "reset color" );
        dropImage.color = new Color( 1.0f, 1.0f, 1.0f );
    }

    public void Clicked()
    {
        Debug.Log( "clicked = " + clicked );
        if (!clicked)
        {
            dropImage.color = new Color( 0.4f, 0.4f, 0.7f );
        }
        else
        {
            dropImage.color = new Color( 1.0f, 1.0f, 1.0f );
        }
        clicked = !clicked;
    }
   
}
