using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class GameOverScript : MonoBehaviour {
	private GUIStyle buttonStyle;
	private GUIStyle labelStyle;
	public Texture2D image;
	public bool stageCleared;
	private int finalScore;
	//public readonly string leaderboardId = "CgkIhcilyYIbEAIQAg";	// QA
	public readonly string leaderboardId = "CgkIhcilyYIbEAIQCQ";

	public static GameOverScript CreateComponent(GameObject where, int finalScoreArg, bool stageClearedArg) 
	{
		GameOverScript created = null;
		if (where != null) {
			created = where.AddComponent<GameOverScript> ();
			created.finalScore = finalScoreArg;
			created.stageCleared = stageClearedArg;
		}

		return created;
	}
	
	// Use this for initialization
	void Start () {
		buttonStyle = new GUIStyle();
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = AppConstants.GetLargeFontSize();
		buttonStyle.fontStyle = FontStyle.Bold;
		buttonStyle.font = (Font)Resources.Load("Fonts/BadBlackCat");
		buttonStyle.normal.background = (Texture2D)Resources.Load ("Materials/catGoldSkin");

		labelStyle = new GUIStyle();
		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.fontSize = AppConstants.GetLargeFontSize();
		labelStyle.fontStyle = FontStyle.Normal;

		GooglePlayGames.PlayGamesPlatform.Activate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		RenderInitialLayout ();
	}

	void RenderInitialLayout() {
		int buttonWidth = Screen.width / 2;
		int buttonHeight = Screen.height / 12;


		if (this.stageCleared) {
			GUI.Label(
				// Center in X, 1/4 of the height in Y
				new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
				"CONGRATULATION!\nYOU WIN!",
				labelStyle);
		} else {
			if (GUI.Button(
				// Center in X, 1/4 of the height in Y
				new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
				"RETRY",
				buttonStyle))
			{
				// Reload the level
				Application.LoadLevel("Stage1");  
			}
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
					Social.ReportScore(finalScore, leaderboardId, (bool success2) => {
						// show leaderboard UI
						//Social.ShowLeaderboardUI();	// all leaderboards
						((GooglePlayGames.PlayGamesPlatform) Social.Active).ShowLeaderboardUI(leaderboardId);
					});
				}
			});		
		} else {
			// post score to leaderboard
			Social.ReportScore(finalScore, leaderboardId, (bool success2) => {
				// show leaderboard UI
				//Social.ShowLeaderboardUI();	// all leaderboards
				((GooglePlayGames.PlayGamesPlatform) Social.Active).ShowLeaderboardUI(leaderboardId);
			});
		}
	}
	#endregion Google Leaderboard
}
