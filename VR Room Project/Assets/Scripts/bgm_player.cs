using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm_player : MonoBehaviour
{
    public AudioSource audio_source;
    private float volume = .1f;

    // Start is called before the first frame update
    void Start()
    {
        audio_source.Play(); //Start music
    }

    // Update is called once per frame
    void Update()
    {
        audio_source.volume = volume;

    }

    public void update_volume(float new_volume)
    {
        volume = new_volume;
    }
}
