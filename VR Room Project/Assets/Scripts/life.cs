using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class life : MonoBehaviour
{
    public Text life_text;
    public int player_life;

    public void init_life()
    {
        display_life(player_life);
    }

    public void decrement_life()
    {
        player_life -= 1;
        display_life(player_life);
    }

    // display current lives left
    private void display_life(int life)
    {
        if (life < 0) // game over
        {
            life_text.text = "DEAD";
        }
        else
        {
            life_text.text = life.ToString();
        }
    }
}
