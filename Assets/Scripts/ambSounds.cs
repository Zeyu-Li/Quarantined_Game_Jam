using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    public float volume = .5f;
    public AudioSource listener;
    private int index;
    private int random;
    public float timer = 0;

    void Update()
    {

        if (timer < 0) {
            random = Random.Range(0, 10);
            if (random < 4) {
                index = Random.Range(0, sounds.Length);
                timer = sounds[index].length;
                listener.PlayOneShot(sounds[index], volume);
            } else {
                timer = 20f;
            }
        }
        timer -= Time.deltaTime;
    }
}
