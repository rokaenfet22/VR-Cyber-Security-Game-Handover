using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    //objs needed
    public Canvas popup_obj;
    public Text info_text;

    //Hide popup initially
    void Start()
    {
        hide();
    }

    // OnClick show popup with corresponding info
    public void open_popup(string product_name) 
    {
        info_text.text = product_info.dict[product_name]; // info of corresponding product
        show();
    }

    public void open_popup_with_any_string(string str)
    {
        info_text.text = str;
        show();
    }

    // Hide Popup
    public void close_popup() 
    {
        hide();
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
