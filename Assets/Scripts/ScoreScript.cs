using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	public int score = 0;
	public GUISkin guiSkin;
	
	// Use this for initialization
	void Start () {
		guiText.fontSize = AppConstants.GetDefaultFontSize ();
		//guiText.font = (Font)Resources.Load("Fonts/BadBlackCat");
	}
	
	// Update is called once per frame
	void Update () {
		// Set the score text.
		guiText.text = "Score: " + score;

	}
}
