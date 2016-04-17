using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public static int Score = 0;
    private Text _text;
    private void Start(){
        _text = GetComponent<Text>(); 
        Reset();       
        _text.text = Score.ToString();
    }
    
	public void AddScore(int points){
        Score += points;
        _text.text = Score.ToString();
    }
    
    public static void Reset(){
        Score = 0;       
    }
}
