using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_manager : MonoBehaviour
{
    public Menu cur_panel = null;
    //Oculus "Back" button's functionality can be implemented with this
    private List<Menu> panel_history = new List<Menu>();

    private void Start()
    {
        setup_panels();
    }

    // 
    private void setup_panels()
    {
        Menu[] panels = GetComponentsInChildren<Menu>(); // returns component of matching type [Menu]
        foreach(Menu panel in panels)
            panel.Setup(this);
        cur_panel.Show();
    }

    // check for "Back" prompt, temporarilily using key bind
    private void update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            print("P pressed");
            go_to_prev_panel();
        }
    }

    // return to previous panel in panel_history if exists. Otherwise caught in first if statement
    public void go_to_prev_panel()
    {
        if(panel_history.Count == 0)
        {
            //Ask user if they want to exit the app, since they "backed" from root panel
            return;
        }
        int last_i = panel_history.Count - 1;
        set_current(panel_history[last_i]);
        panel_history.RemoveAt(last_i);
    }

    // active when page swapped
    public void set_current_with_history(Menu new_panel)
    {
        panel_history.Add(cur_panel);
        set_current(new_panel);
    }

    // update panel history and update displayed panel
    private void set_current(Menu new_panel)
    {
        cur_panel.Hide();
        cur_panel = new_panel;
        cur_panel.Show();
    }

    // For resetting panel history
    public void reset_panel_history()
    {
        setup_panels();
    }
}
