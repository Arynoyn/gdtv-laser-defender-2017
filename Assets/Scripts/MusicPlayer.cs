using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    static MusicPlayer instance = null;
	public AudioClip StartClip;
    public AudioClip GameClip;
    public AudioClip EndClip;
    
    private AudioSource _music;
    private void Awake(){
        if (instance != null && instance != this) {
            Destroy(gameObject);
            print("Duplicate music player self-destructing");
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            _music = GetComponent<AudioSource>();
            PlayClip(StartClip);
        }
    }
    
    private void OnLevelWasLoaded(int level){
        Debug.Log("Music Player: Loaded Level: " + level);
        switch (level) {
            case 0:
                PlayClip(StartClip);
                break;
            case 1:
                PlayClip(GameClip);
                break;
            case 2:
                PlayClip(EndClip);
                break;
            default:
                PlayClip(StartClip);
                break;
        }        
    }
    
    private void PlayClip(AudioClip clip){
        Debug.Log("Music Player: Clip: " + clip.name );
        if(_music){
            _music.Stop();
            _music.clip = clip;
            _music.loop = true;
            _music.Play();
        }
    }
    	
	// Update is called once per frame
	private void Update () {
	
	}
}
