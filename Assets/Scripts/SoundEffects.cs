using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

    AudioSource audioSource;
    [SerializeField]
    List<AudioClip> soundEffects = new List<AudioClip>();

	// Use this for initialization
	void Start () {
        audioSource = FindObjectOfType<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayRandomSound()
    {
        int rand = Random.Range(0, soundEffects.Count);
        
        audioSource.PlayOneShot(soundEffects[rand]);
    }
}
