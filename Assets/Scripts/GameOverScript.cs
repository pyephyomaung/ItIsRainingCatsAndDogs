using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using System.Text.RegularExpressions;

public class GameOverScript : MonoBehaviour {
	private bool scoreBoardLayout = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		if (scoreBoardLayout) { RenderScoreboardLayout(); } 
		else { RenderInitialLayout(); }
	}

	void RenderInitialLayout() {
		const int buttonWidth = 140;
		const int buttonHeight = 60;
		
		if (GUI.Button(
			// Center in X, 1/4 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
			"RETRY"
			))
		{
			// Reload the level
			Application.LoadLevel("Stage1");  
		}
		
		if (GUI.Button(
			// Center in X, 2/4 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
			"SCORES"
			))
		{
			// Load scores
			CallFBInit();
			scoreBoardLayout = true;
		}
		
		if (GUI.Button(
			// Center in X, 3/4 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight),
			"QUIT"
			))
		{
			Application.Quit();
		}
	}

	void RenderScoreboardLayout() {
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinWidth(mainWindowFullWidth));
		GUILayout.BeginVertical();
		GUI.enabled = !isInit;
		if (Button("Connect"))
		{
			CallFBInit();
		}
		
		
		GUI.enabled = FB.IsLoggedIn;
		GUILayout.BeginHorizontal();
		
		GUILayout.EndHorizontal();
		
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		GUI.enabled = true;
		
		if(FB.AccessToken !="" && app42){
			app42 = false;
			app42Connect.SocialConnectWithApp42(FB.AccessToken);
		}

		
		GUILayout.BeginHorizontal(GUI.skin.box);
		
		scrollVector = GUILayout.BeginScrollView(scrollVector, GUILayout.Height (300));
		GUI.enabled = isInit;
		GUILayout.BeginHorizontal();
		GUILayout.Space(130);
		GUILayout.Label(new GUIContent("Rank"));
		GUILayout.Label(new GUIContent("Name"));
		GUILayout.Label(new GUIContent("Score"));
		GUILayout.EndHorizontal();
		if(App42Console.GetFList().Count() !=0){
			for (int i = 0; i < App42Console.GetFList().Count(); i++)
				
			{   
				IList<string> list = (IList<string>)App42Console.GetFList()[i];
				GUILayout.BeginHorizontal();
				GUILayout.Space(140);
				GUILayout.Label(new GUIContent(list[0]),GUILayout.MaxWidth(200));
				GUILayout.Space(-30);
				GUILayout.Label(new GUIContent(list[1]),GUILayout.MaxWidth(200));
				GUILayout.Space(30);
				GUILayout.Label(new GUIContent(list[2]),GUILayout.MaxWidth(200));	
				//GUILayout.Space(30);
				GUILayout.EndHorizontal();
				
				if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
					
				{
					
					// Handle events here
					
				}
				
			}
		}else{
			GUILayout.Space(100);
			GUILayout.BeginHorizontal();
			GUILayout.Space(310);
			GUILayout.Label(new GUIContent(loadingData));	
			GUILayout.EndHorizontal();
		}
		
		GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
		score = GUILayout.TextField(score,4,GUILayout.MinHeight(buttonHeight));
		score = Regex.Replace(score, @"[^0-9]", "");
		GUI.enabled = save;
		if(GUILayout.Button("Save Your Score", GUILayout.MinHeight(buttonHeight))){
			print ("Save your score FB userId: " + FB.UserId);
			app42Connect.SaveUserScore(FB.UserId,score);
			save = false;
		}
	}



	#region FB.Init() example
	private string status = "";
	private bool isInit = false;
	private bool app42 = true;
	App42Console app42Connect = new App42Console();
	public string score;
	Vector2 scrollVector;
	private bool save = false;
	private string loadingData = "Click Connect ...";
	
	private void CallFBInit()
	{
		print ("FB init called with appId: " + FB.AppId);
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		isInit = true;
		GUI.enabled = isInit && !FB.IsLoggedIn;
		CallFBLogin();
		save = true;
		status = "Login called";
		loadingData = "Loading Data Please Wait ...";
		
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	#endregion
	
	#region FB.Login() example
	private void CallFBLogin()
	{
		FB.Login("email");
	}
	#endregion

	#region GUI
	
	private string rank,name,scoreValue;
	public GUIStyle textStyle = new GUIStyle();
	private Vector2 scrollPosition = Vector2.zero;
	#if UNITY_IOS || UNITY_ANDROID
	int buttonHeight = 60;
	int mainWindowWidth = 610;
	int mainWindowFullWidth = 640;
	#else
	int buttonHeight = 24;
	int mainWindowWidth = 500;
	int mainWindowFullWidth = 750;
	#endif

//	void Awake()
//	{
//		textStyle.alignment = TextAnchor.UpperLeft;
//		textStyle.wordWrap = true;
//		textStyle.stretchHeight = true;
//		textStyle.stretchWidth = false;
//	}

	private bool Button(string label)
	{
		return GUILayout.Button(label, GUILayout.MinHeight(buttonHeight));
	}
	
	private void LabelAndTextField(string label, ref string text)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(label, GUILayout.Width(150));
		text = GUILayout.TextField(text, GUILayout.MinWidth(300));
		GUILayout.EndHorizontal();
	}
	
	#endregion
}
