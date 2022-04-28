using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class main_game_handler : MonoBehaviour
{
    public timer timer;
    public tasks tasks;
    public life life;
    // Predefined Dictionary, key = difficulty [0:easy, 1:medium, 2:hard], value = {lives, num questions to ans, options}
    private static Dictionary<int,int[]> diff_val_dict = new Dictionary <int,int[]>()
    {
        {0, new int[] {3,7,2}},
        {1, new int[] {2,7,3}},
        {2, new int[] {1,10,4}}
    };
    // Predefined max number of options to answering letter mapping
    public static List<string> letters_for_answering = new List<string> {"A","B","C","D"};
    // Initialize difficulty to -1
    public int difficulty = -1;
    // difficulty dependent vars
    private int num_options;
    // GamePlay vars
    private bool game_running = false;
    public bool qs_disp = false;
    private int screen_count = 6;
    // Load popup question and ans text objects
    public Text[] popup_ans_texts;
    public Text popup_question_text;
    // Load screen GameObjects
    public GameObject[] screen_panels;
    // Preprocess questions for random selection
    private Dictionary<string,string> combined_question_dict = new Dictionary<string,string>();
    private string[] cyber_threat_names = questions.cyber_threat_names;
    private string[] cyber_product_names = questions.cyber_product_names;
    private string[] other_solution_names = questions.other_solution_names;
    private string[] temp_str_arr;
    private static System.Random rng = new System.Random();
    public Dictionary<string,string> ans_letter_to_string = new Dictionary<string,string>();
    public string answer;
    // Watson
    public watson_manager watson_manager;
    // popup
    public game_popup game_popup;
    public popup popup;
    // For changing back page
    public Canvas menu_canvas;
    public Canvas game_canvas;
    public Menu_manager menu_manager;
    
    private void Start()
    {
        // Dynamically add panels in inspector, and initialize them as disabled
        Instantiate(screen_panels[screen_count-1]);
        for (int i = 0; i < screen_count; i++)
        {
            screen_panels[i].SetActive(false);
        }
        // Dynamically add textboxes in inspector
        Instantiate(popup_ans_texts[3]);
        // Add all question:ans pair from questions.cs to combined_question_dict
        foreach(KeyValuePair<string,string> dict in questions.cyber_threat_questions_dict)
        {
            combined_question_dict[dict.Key] = dict.Value;
        }
        foreach(KeyValuePair<string,string> dict in questions.cyber_product_questions_dict)
        {
            combined_question_dict[dict.Key] = dict.Value;
        }
        foreach(KeyValuePair<string,string> dict in questions.other_questions_dict)
        {
            combined_question_dict[dict.Key] = dict.Value;
        }
    }

    private void Update()
    {
        if (game_running)
        {
            // Detect game ending
            detect_game_run_condition();
            if (qs_disp == false)
            {
                // choose random screen and display the panel on it
                int screen_i = Random.Range(0,screen_count);
                GameObject chosen_screen = screen_panels[screen_i];
                display_panel(chosen_screen);
                // pick random question
                List<string> key_list = new List<string>(combined_question_dict.Keys);
                int key_i = Random.Range(0,key_list.Count);
                string random_key = key_list[key_i];
                // random_key = question, combined_question_dict[random_key] = ans
                // remove this pair from the dictionary to prevent duplicates
                answer = combined_question_dict[random_key];
                string question = random_key;
                screen_count -= 1;
                combined_question_dict.Remove(random_key);
                // set popup question
                popup_question_text.text = question;
                // set popup answers
                string[] temp;
                if (cyber_threat_names.Contains(answer)){
                    temp = cyber_threat_names;
                } else if (cyber_product_names.Contains(answer)){
                    temp = cyber_product_names;
                } else {
                    temp = other_solution_names;
                }
                List<string> ans_pool = new List<string>(temp);
                List<string> t = new List<string>();
                t.Add(answer);
                ans_pool.Remove(answer);
                for (int i=0; i<num_options-1; i++)
                {
                    // get some random answer that's not the right answer
                    int random_i = Random.Range(0,ans_pool.Count);
                    t.Add(ans_pool[random_i]);
                    ans_pool.Remove(ans_pool[random_i]);
                }
                // Shuffle answers
                var tempo = t.OrderBy(a => rng.Next()).ToList();
                // Assign to letters
                for (int i=0; i<tempo.Count; i++)
                {
                    ans_letter_to_string[letters_for_answering[i]] = tempo[i];
                }
                // Display in each answer options
                for (int i=0; i<tempo.Count; i++)
                {
                    popup_ans_texts[i].text = letters_for_answering[i] + ": " + tempo[i];
                }
                qs_disp = true;
                // For dev
                print(answer);
            } 
        }
    }

    public void answered_correct()
    {
        print("correct!");
        tasks.decrement_tasks();
        reset_for_next_qs();
    }

    public void answered_wrong()
    {
        print("false!");
        life.decrement_life();
        reset_for_next_qs();
    }

    private void detect_game_run_condition()
    {
        if (tasks.tasks_left == 0)
        {
            print("WIN");
            proceed_with_end_game();
        }
        if (life.player_life == 0)
        {
            print("LOSE");
            proceed_with_end_game();
        }
        if (timer.time_remaining <= 0)
        {
            print("LOSE");
            proceed_with_end_game();
        }
    }

    private void proceed_with_end_game()
    {
        game_running = false;
        // swap back to main menu
        menu_canvas.GetComponent<Canvas>().enabled = true;
        game_canvas.GetComponent<Canvas>().enabled = false;
        menu_manager.reset_panel_history();
    }

    private void win_game()
    {
        popup.open_popup_with_any_string("WIN");
    }

    private void lose_game()
    {
        popup.open_popup_with_any_string("LOSE");
    }

    private void reset_for_next_qs()
    {
        game_popup.close_popup();
        qs_disp = false;
        foreach (GameObject screen in screen_panels)
        {
            screen.SetActive(false);
        }
        watson_manager.answered = false;
    }

    private void display_panel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void start_game()
    {
        // initialize lives counter
        life.player_life = diff_val_dict[difficulty][0];
        life.init_life();
        // activate timer
        timer.timer_on = true;
        // initialize tasks counter
        tasks.tasks_left = diff_val_dict[difficulty][1];
        tasks.init_task();
        // start game
        num_options = diff_val_dict[difficulty][2];
        game_running = true;
    }

}
