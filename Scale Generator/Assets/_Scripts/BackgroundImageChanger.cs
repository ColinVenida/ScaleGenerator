using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageChanger : MonoBehaviour
{
    public Image backgroundImage;
    public List<Sprite> SpriteList;
        
    public void ChangeBackGroundImage( int value )
    {
        backgroundImage.sprite = SpriteList[value];
    }
}
