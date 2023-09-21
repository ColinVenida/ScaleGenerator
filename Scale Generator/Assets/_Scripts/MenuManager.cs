using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

//class that opens/closes PopupMenus and dropdown windows in the scene
public class MenuManager : MonoBehaviour
{
    public List<PopupWindow> windowList;
    public List<Dropdown> dropdownList;
    public Button closeNavMenuBtn;

    public void OpenMenu( int id )
    {
        CloseAllMenus();
        windowList[id].OpenMenu();
        closeNavMenuBtn.interactable = false;
    }

    public void CloseAllMenus()
    {
        foreach( PopupWindow window in windowList )
        {
            window.CloseMenu();
        }
        closeNavMenuBtn.interactable = true;
    }

    public void CloseMenu( int id )
    {
        windowList[id].CloseMenu();
        closeNavMenuBtn.interactable = true;
    }    
}
