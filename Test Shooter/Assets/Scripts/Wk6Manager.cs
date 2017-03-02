using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Net;

public class Wk6Manager : MonoBehaviour {

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
		string content = client.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20astronomy.sunset%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22maui%2C%20hi%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
	
		JSONNode hawaii = JSON.Parse(content);
		string sunset = hawaii["query"]["results"]["channel"]["astronomy"]["sunset"];
		print(sunset);
	}
	
 
	// Update is called once per frame
	void Update () {
		
	}
}
