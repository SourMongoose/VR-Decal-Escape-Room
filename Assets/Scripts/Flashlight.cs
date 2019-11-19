using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public AudioClip soundOn;
    public AudioClip soundOff; 
    public Light light;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            light.enabled = !light.enabled;
            audio.clip = audio.clip == soundOn ? soundOff : soundOn;
            audio.Play();
        }
    }
}
