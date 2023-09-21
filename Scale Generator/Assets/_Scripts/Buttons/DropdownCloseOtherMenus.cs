using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//behavior for dropdown objects.  If the dropdown is clicked, then the other PopWindow menus should be closed
public class DropdownCloseOtherMenus : MonoBehaviour, IPointerClickHandler
{
    public MenuManager manager;

    public void OnPointerClick( PointerEventData eventData )
    {
        manager.CloseAllMenus();
    }
}
