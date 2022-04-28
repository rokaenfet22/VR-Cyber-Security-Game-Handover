using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    public void quit_game()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
