using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//behavior that changes the dropdown's color when the window is open
public class DropdownColorChanger : MonoBehaviour, IPointerClickHandler, IDeselectHandler
{
    //private Color DARK_GRAY = new Color( 0.5f, 0.5f, 0.5f );    
    public Color highlightColor;
    public Dropdown drop;

    private ColorBlock originalColors;

    public void Start()
    {
        originalColors = drop.colors;        
    }    

    public void OnPointerClick( PointerEventData eventData )
    {        
        ChangeColor( highlightColor );                
    }

    private void ChangeColor ( Color color_new )
    {
        ColorBlock cb = drop.colors;
        cb.normalColor = color_new;
        cb.highlightedColor = color_new;
        drop.colors = cb;
    }   

    public void OnDeselect( BaseEventData baseEventData )
    {
        //Debug.Log( "deselected!" );
        drop.colors = originalColors;
    }
}
