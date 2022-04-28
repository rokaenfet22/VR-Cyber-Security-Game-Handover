using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watson_manager : MonoBehaviour
{
    public main_game_handler main_game_handler;
    public watson_speech_to_text watson_speech_to_text;
    public bool answered = false;
    private string word;

    private void Update()
    {
        if (main_game_handler.qs_disp && answered == false)
        {
            string word = watson_speech_to_text.GetFinalTranscript();
            if (word != null)
            {
                check_text_credibility(word);
            }
        }   
    }

    // Check if speech-to-text output is close enough to one of the available options, if so change "correct" accordingly
    private void check_text_credibility(string word)
    {
        // int cur_score = 0;
        // int score = 100;
        string cur_letter = null;
        word = word.ToUpper();
        foreach (KeyValuePair<string,string> dict in main_game_handler.ans_letter_to_string)
        {
            string l = dict.Key;
            // Levenshtein distance string similarity
            // score = compute_string_similarity(l,word);
            // if (score > cur_score)
            // {
            //     cur_letter = l;
            //     cur_score = score;
            // }

            if (string.Compare(l,0,word,0,1) == 0)
            {
                cur_letter = l;
            }
        }
        if (cur_letter != null)
        {
            answered = true;
            word = null;
            if (cur_letter == main_game_handler.answer)
            {
                main_game_handler.answered_correct();

            } else {
                main_game_handler.answered_wrong();
            }
        }
        
        
        // if (cur_score <= 3) // Score threshold for string similarity
        // {
        //     // valid speech-to-text result
        //     if (cur_letter == main_game_handler.answer)
        //     {
        //         print(cur_letter);
        //         correct = "correct";
        //     }
        // }
    }

    // https://stackoverflow.com/questions/6944056/compare-string-similarity
    // string similarity algo using Levenshtein Distance
    private int compute_string_similarity(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            if (string.IsNullOrEmpty(t))
                return 0;
            return t.Length;
        }

        if (string.IsNullOrEmpty(t))
        {
            return s.Length;
        }

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // initialize the top and right of the table to 0, 1, 2, ...
        for (int i = 0; i <= n; d[i, 0] = i++);
        for (int j = 1; j <= m; d[0, j] = j++);

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                int min1 = d[i - 1, j] + 1;
                int min2 = d[i, j - 1] + 1;
                int min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Mathf.Min(Mathf.Min(min1, min2), min3);
            }
        }
        return d[n, m];
    }
}
