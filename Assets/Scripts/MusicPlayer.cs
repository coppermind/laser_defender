using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}
	
	void OnLevelWasLoaded(int level) {
		Debug.Log("Level loaded: " + level);
		
		if (music != null) {
			music.Stop();
				
			switch (level) {
				// Start menu
				case 0:
					music.clip = startClip;
					break;
				// Game screen
				case 1:
					music.clip = gameClip;
					break;
				// Win/Lose Screen
				default:
					music.clip = endClip;
					break;
			}
			music.loop = true;
			music.Play();
		}
	}
}
