using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;


public class Wk6Manager : MonoBehaviour {


	public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	    {
	        bool isOk = true;
	        // If there are errors in the certificate chain, look at each error to determine the cause.
	        if (sslPolicyErrors != SslPolicyErrors.None)
	        {
	            for (int i = 0; i < chain.ChainStatus.Length; i++)
	            {
	                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
	                {
	                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
	                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
	                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
	                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
	                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
	                    if (!chainIsValid)
	                    {
	                        isOk = false;
	                    }
	                }
	            }
	        }
	        return isOk;
	    }
	// Use this for initialization
	void Start () {
		UtilScript.WriteStringToFile(Application.dataPath, "hello.txt", "hi!");
	
		transform.position = UtilScript.CLoneModVector3(transform.position, 0, 1, 0);	
	
		Vector3 pos = UtilScript.CloneVector3(transform.position);
	
		JSONClass subClass = new JSONClass();

		subClass["test"] = "value";

		JSONClass json = new JSONClass();
	
		json["x"].AsFloat = 7;
		json["y"].AsFloat = 0;
		json["z"].AsFloat = 2;
		json["name"] = "Matt";
		json["Alt Facts"].AsBool = false; 
		json["sub"] = subClass;
//		json["somethingElse"].AsObject
		
		UtilScript.WriteStringToFile(Application.dataPath, "file.json", json.ToString());
	
		Debug.Log(json);

//		JSONClass readJSON = 		
		
		string result = UtilScript.ReadStringFromFile(Application.dataPath, "file.json");

		JSONNode readJSON = JSON.Parse(result);

		Debug.Log(readJSON["z"].AsFloat);
		WebClient client = new WebClient();
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
		string content = client.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20astronomy.sunset%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22maui%2C%20hi%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
		Debug.Log(content);

		JSONNode hawaii = JSON.Parse(content);
		string sunset = hawaii["query"]["results"]["channel"]["astronomy"]["sunset"];
		print(sunset);
	}
	
 
	// Update is called once per frame
	void Update () {
		
	}
}
