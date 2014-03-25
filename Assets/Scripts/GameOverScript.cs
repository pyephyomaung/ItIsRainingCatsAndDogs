using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class GameOverScript : MonoBehaviour {
	private bool scoreBoardLayout = false;
	private GUIStyle buttonStyle;
	public Texture2D image;
	private ScoreScript scoreScript;
	public readonly string leaderboardId = "CgkIhcilyYIbEAIQAg";	// QA
	// leaderboardId = "CgkIhcilyYIbEAIQAQ";
	
	// Use this for initialization
	void Start () {
		buttonStyle = new GUIStyle();
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = AppConstants.GetLargeFontSize();
		buttonStyle.fontStyle = FontStyle.Bold;
		buttonStyle.font = (Font)Resources.Load("Fonts/BadBlackCat");
		buttonStyle.normal.background = (Texture2D)Resources.Load ("Materials/catGoldSkin");
		scoreScript = GameObject.Find("Score").GetComponent<ScoreScript>();
		GooglePlayGames.PlayGamesPlatform.Activate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		print ("here");
		RenderInitialLayout ();
	}

	void RenderInitialLayout() {
		int buttonWidth = Screen.width / 2;
		int buttonHeight = Screen.height / 12;

		if (GUI.Button(
			// Center in X, 1/4 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
			"RETRY",
			buttonStyle))
		{
			// Reload the level
			Application.LoadLevel("Stage1");  
		}
		
		if (GUI.Button(
			// Center in X, 2/4 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
			"SCORES",
			buttonStyle))
		{
			LoadScores();
		}
		
		if (GUI.Button(
			// Center in X, 3/4 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
			"QUIT",
			buttonStyle))
		{
			Application.Quit();
		}
	}

	void LoadScores() {
//		CallFBInit();
//		scoreBoardLayout = true;

		CallGoogleInit ();
	}

	#region Google Leaderboard
	void CallGoogleInit() {
		// authenticate user:
		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate((bool success) => {
				if (success) {
					// post score to leaderboard
					Social.ReportScore(scoreScript.score, leaderboardId, (bool success2) => {
						// show leaderboard UI
						//Social.ShowLeaderboardUI();	// all leaderboards
						((GooglePlayGames.PlayGamesPlatform) Social.Active).ShowLeaderboardUI(leaderboardId);
					});
				}
			});		
		} else {
			// post score to leaderboard
			Social.ReportScore(scoreScript.score, leaderboardId, (bool success2) => {
				// show leaderboard UI
				//Social.ShowLeaderboardUI();	// all leaderboards
				((GooglePlayGames.PlayGamesPlatform) Social.Active).ShowLeaderboardUI(leaderboardId);
			});
		}
		
	}
	#endregion Google Leaderboard
}
