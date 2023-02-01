﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArpeggioButton : MonoBehaviour
{
    public Button selfBtn;
    public int arpID;
    public ArpeggioManager arpManager;  

    public void PromptArpeggio()   
    {
        arpManager.ProcessArpeggio( arpID, selfBtn );
    }
}
