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
		ClientManager.singleton.Request("msg",CallBack);
	}

	void CallBack(){
		ClientManager.singleton.Request2("msg2",EndCallBack);
	}

	void EndCallBack(){
		Debug.Log("end");
	}

}
