using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end_popup : MonoBehaviour
{
    //objs needed
    public Canvas popup_obj;

    //Hide popup initially
    void Start()
    {
        hide();
    }

    // Hide Popup
    public void close_popup() 
    {
        hide();
    }

    public void show_popup()
    {
        show();
    }

    void show()
    {
        popup_obj.enabled = true;
    }

    void hide()
    {
        popup_obj.enabled = false;
    }
}
