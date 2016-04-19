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

	Action<RequestBase> m_callback;

	View m_view;

	int m_count;

	[ServerCallback]
	void Start () {
		m_count = 0;
		m_task = ServerTask;
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
		
	public void Request(RequestBase data, Action<RequestBase> callback){
		m_callback = callback;
		CmdRequest(data);
	}

	[Command]
	void CmdRequest(RequestBase data){
		CreateData(data);
	}

	[Server]
	void CreateData(RequestBase data){
		Debug.Log(m_count);
		RpcGetData(data);
	}

	[ClientRpc]
	void RpcGetData(RequestBase data){
		Debug.Log("clientRPC");
		Debug.Log(data);
		m_callback(data);
	}


	[Server]
	void ServerTask(){
		m_count++;
	}

	/*
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
	*/


}
