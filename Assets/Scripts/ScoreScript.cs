using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	public int score = 0;
	
	// Use this for initialization
	void Start () {
		guiText.fontSize = AppConstants.GetFontSize ();
	}
	
	// Update is called once per frame
	void Update () {
		// Set the score text.
		guiText.text = "Score: " + score;
	}
}
