using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficulty_handler : MonoBehaviour
{
    // objs
    public Menu_manager menu_manager;
    public main_game_handler game_handler;
    // game objs to handle
    public Canvas menu_canvas;
    public Canvas game_canvas;

    private void Start()
    {
        menu_manager = GameObject.Find("Main_menu").GetComponent<Menu_manager>();
        game_handler = GameObject.Find("game_manager").GetComponent<main_game_handler>();
    }

    // easy:0, medium:1, hard:2. Change difficulty value in main_game_handler
    // Reset Panel history to consider for panel_history == Oculus' "Back" button functionality
    public void set_difficulty_on_select(int new_difficulty)
    {
        game_handler.difficulty = new_difficulty;
        // reset panel_history
        menu_manager.reset_panel_history();
        // change canvas to game cavnas
        change_to_game_canvas();
        // fire game starter
        game_handler.start_game();
    }

    // change to game canvas
    private void change_to_game_canvas()
    {
        menu_canvas.GetComponent<Canvas>().enabled = false;
        game_canvas.GetComponent<Canvas>().enabled = true;
    }
}
