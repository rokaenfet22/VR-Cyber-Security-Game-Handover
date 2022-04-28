    using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class timer : MonoBehaviour
{
    // timer sec
    public float time_remaining = 120;
    public bool timer_on = false;
    // timer text obj
    public Text timer_text;

    private void Update()
    {
        //timer starting condition
        if (timer_on)
        {
            // timer running condition
            if (time_remaining > 0)
            {
                time_remaining -= Time.deltaTime;
                display_time(time_remaining);
            }
            else // timeout, lock timer to 0, output fail
            {
                timer_on = false;
            }
        }
    }

    private void display_time(float time)
    {
        // zero disp
        if (time < 0)
        {
            time = 0;
        }
        else // for countdown to zero not being actual full second issue
        {
            time += 1;
        }
        // calculate minutes and seconds in int form (rounded down)
        float mins = Mathf.FloorToInt(time / 60);
        float secs = Mathf.FloorToInt(time % 60);
        // format to string and display on text
        timer_text.text = string.Format("{0:00}:{1:00}",mins,secs);
    }
}
