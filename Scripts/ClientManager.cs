using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class ClientManager : NetworkBehaviour {

	public static ClientManager singleton {
		get; set;
	}

	[SerializeField] GameObject viewPrefab;

	Action	m_task;

	Action m_callback;

	View m_view;

	[ServerCallback]
	void Start () {
	
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		singleton = this;
		var obj = Instantiate(viewPrefab) as GameObject;
		m_view = obj.GetComponent<View>();
		m_view.Init();
	}
	
	[ServerCallback]
	void Update () {
		if(m_task!=null){
			m_task();
		}
	}
		
	public void Request(string msg, Action callback){
		m_callback = callback;
		CmdRequest(msg);
	}

	[Command]
	void CmdRequest(string msg){
		CreateData(msg);
	}

	[Server]
	void CreateData(string msg){
		RpcGetData(msg);
	}

	[ClientRpc]
	void RpcGetData(string msg){
		Debug.Log(msg);
		m_callback();
	}


	public void Request2(string msg, Action callback){
		m_callback = callback;
		CmdRequest2(msg);
	}


	[Command]
	void CmdRequest2(string msg){
		CreateData2(msg);
	}

	[Server]
	void CreateData2(string msg){

		RpcGetData2(msg);
	}

	[ClientRpc]
	void RpcGetData2(string msg){
		Debug.Log(msg);
		m_callback();
	}


}
