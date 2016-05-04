using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    int currentTrack = -1;

    public AudioClip[] musicList;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if ( audioSource.isPlaying == false)
        {
            audioSource.clip = musicList[++currentTrack%musicList.Length];
            audioSource.Play();
        }
	}
}
