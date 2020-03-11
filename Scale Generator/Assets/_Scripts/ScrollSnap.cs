using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//behavior that automatically adjusts a dropdown's viewport to show the selected item

public class ScrollSnap : MonoBehaviour
{


    //TODO:  is there a way to get these references without setting them to public, and then drag-dropping them in the editor?

    public Dropdown parentDrop;             //referenct to the parent's dropdown component
    public RectTransform contentTrans;      //reference to the Content object
                                            //content.position.y controls the location of the viewport and scrollbar 

    private void OnEnable()
    {
        //***Alert***
        //OnEnable() is called multiple times when the dropdown is clicked.  I'm guessing the dropdown's template OnEnable() is called, and the
        //OnEnable() of the actual DropdownList object is called

        //dividing (parentDrop.value + 1) / (total items in the Dropdown) shows us how far down the viewport we need to go
        float y = contentTrans.rect.height * (parentDrop.value + 1 / parentDrop.options.Count);        
        contentTrans.position = new Vector3(contentTrans.position.x, y);

    }


}
