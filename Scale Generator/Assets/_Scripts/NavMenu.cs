using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//class that controls the position of a navigation bar
public class NavMenu : MonoBehaviour
{

    public RectTransform uiRectTransform;
    public Button openBtn;
    public Button closeBtn;

    private bool inPlace;
    private bool moveDown;
    private Vector3 direction;    
    private float targetY;
    private float upperY;   

    void Start()
    {
        direction = Vector3.down;
        upperY = uiRectTransform.rect.height;
        moveDown = true;
        inPlace = true;
    }

    public void OpenMenu()
    {
        //deactivate open button
        openBtn.gameObject.SetActive( false );

        //set the move UI game object to true
        direction = new Vector3( 0.0f, -10.0f );
        targetY = 0;
        moveDown = true;
        inPlace = false;

        //activate close button
        closeBtn.gameObject.SetActive( true );
    }

    public void CloseMenu()
    {
        closeBtn.gameObject.SetActive( false );

        direction = new Vector3( 0.0f, 10.0f );
        targetY = upperY;
        moveDown = false;
        inPlace = false;

        openBtn.gameObject.SetActive( true );
    }

    void Update()
    {        
        if ( !inPlace )
        {            
            uiRectTransform.Translate( direction, Space.Self );           

            if ( moveDown )
            {
                if ( uiRectTransform.localPosition.y <= 0 )
                {
                    inPlace = true;
                }
            }
            else 
            {
                if ( uiRectTransform.localPosition.y >= upperY )
                {
                    inPlace = true;
                }
            }            
        }        
    }
}
