using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tasks : MonoBehaviour
{
    public Text task_text;
    public int tasks_left;

    public void init_task()
    {
        display_task_num(tasks_left);
    }

    public void decrement_tasks()
    {
        tasks_left -= 1;
        display_task_num(tasks_left);
    }

    private void display_task_num(int tasks)
    {
        if (tasks <= 0)
        {
            task_text.text = "0";
        }
        else
        {
            task_text.text = tasks.ToString();
        }
    }

}
