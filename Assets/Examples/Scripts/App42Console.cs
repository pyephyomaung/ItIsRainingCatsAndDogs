using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Security;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Security.Cryptography.X509Certificates;
using SimpleJSON;
using AssemblyCSharp;
using System.Threading;



public class App42Console : MonoBehaviour,App42CallBack {
	
	// Use this for initialization
	public static List<object> fList = new List<object> ();
	ServiceAPI sp =null;
	ScoreBoardService scoreService = null;
	SaveCallback callback = new SaveCallback();
	AppConstant constants = new AppConstant();
	public string name;
		
	
	void OnGUI()
    {
		if(AppConstant.GetSaved()){
			AppConstant.SetSaved(false);
			SocialConnectWithApp42(FB.AccessToken);
			fList = new List<object>();
		}
	}
	
	public void SaveUserScore(string userId, string score)
	{
		scoreService.SaveUserScore(constants.GameName, userId, Convert.ToDouble(score), callback);
	}
	
	public void SocialConnectWithApp42(string fbAccessToken)
	{
		print ("SocialConnectWithApp42 fbAccessToken: " + fbAccessToken);
	 sp = AppConstant.GetServce();
	 scoreService = AppConstant.GetScoreService(sp);
	 scoreService.GetTopNRankersFromFacebook(constants.GameName, fbAccessToken, 10, this);
	}
	
	public void OnSuccess (object response)
	{
		if (response is Game){
		Game gameObj = (Game)response;
		IList<Game.Score> scoreList = gameObj.GetScoreList();
			Debug.Log(scoreList);
			for (int i=0 ;i< scoreList.Count;i++)
			{
				string userName = scoreList[i].GetFacebookProfile().GetName();
				IList<string> list = new List<string>();
				string rank = (i+1).ToString();
				list.Add(rank);
				list.Add(userName);
				list.Add(scoreList[i].GetValue().ToString());
				fList.Add(list);
			}
		}
		
		
	}
	
	public static IList<object> GetFList(){
		return fList;
	}
	
	public void OnException (Exception e)
	{
			Debug.Log("Exception Occurred : " + e.ToString());
	}
}
