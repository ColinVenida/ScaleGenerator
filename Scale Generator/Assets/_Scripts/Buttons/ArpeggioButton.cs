using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArpeggioButton : MonoBehaviour
{
    public int scale;
    public ArpeggioManager arpManager;    

    public void Arpeggio()   
    {
        arpManager.FilterScale( scale );               
    }
}
