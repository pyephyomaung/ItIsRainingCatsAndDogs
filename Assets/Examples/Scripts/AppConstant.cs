using UnityEngine;
using System.Collections;
using SimpleJSON;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Security.Cryptography.X509Certificates;
using AssemblyCSharp;


public class AppConstant : MonoBehaviour {
	
	public static string API_KEY = "dc4e50151c390e15fbdd702e9973e38be9a604ee46c8e5a8b04801bbbdf8fccf";
	public static string SECRET_KEY = "5e8b984cb8c5d861249354225dc69ea4e42de022c1209897834202ff81c6041e";
	public string GameName = "itisrainingcatsanddogs";
	private static string userName;
	public static bool isSaved = false;
	
	public static bool GetSaved(){
		return isSaved;
	}
	
	public static void SetSaved(bool saved){
		isSaved = saved;
	}
	
	public static ServiceAPI GetServce(){
		return new ServiceAPI(API_KEY,SECRET_KEY);
	}
	
	public static ScoreBoardService GetScoreService(ServiceAPI sp){
		return sp.BuildScoreBoardService();
	}
	
}
