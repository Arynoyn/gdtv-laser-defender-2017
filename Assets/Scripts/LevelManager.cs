using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
        Debug.Log(string.Format("Level load requested for: {0}", name));
        Application.LoadLevel(name);
    }
    
    public void QuitRequest(){
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }    
}
