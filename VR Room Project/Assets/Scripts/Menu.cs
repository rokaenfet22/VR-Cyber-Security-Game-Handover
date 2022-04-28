using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas canvas = null;
    private Menu_manager menu_manager = null;

    // Start is called before the first frame update
    private void Awake()
    {
        canvas = GetComponent<Canvas>(); // Returns canvas component if attached, otherwise returns null
    }

    // Updater for menu manager.
    public void Setup(Menu_manager menu_manager)
    {
        this.menu_manager = menu_manager;
        Hide();
    }

    // show current canvas
    public void Show()
    {
        canvas.enabled = true;
    }
    // hide current canvas
    public void Hide()
    {
        canvas.enabled = false;
    }
}
