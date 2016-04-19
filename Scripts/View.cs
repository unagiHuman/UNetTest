using UnityEngine;
using System.Collections;
using System;

public class View : MonoBehaviour {

	Action m_callback;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(){
		var request = new RequestData();
		request.name = "name";
		request.x = 1;
		ClientManager.singleton.Request(request,CallBack);
	}

	void CallBack(RequestBase data){
		//Debug.Log("2");
		Debug.Log(data);
		ClientManager.singleton.Request(data,EndCallBack);
	}

	void EndCallBack(RequestBase data){
		Debug.Log(data);
		Debug.Log("end");
	}

}
