using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour {

	public static GameManager singleton {
		get; set;
	}

	[SerializeField] GameObject viewPrefab;

	Action	m_task;

	Action<RequestBase> m_callback;

	View m_view;

	int m_count;


	void Awake(){
		singleton = this;
	}


	[ServerCallback]
	private void Start () {
		m_count = 0;
		//m_task = ServerTask;
		StartCoroutine(CreateView());
	}


	IEnumerator CreateView(){
		///ClientRpc使うには待つ必要があるみたい。理由は不明。
		yield return new WaitForSeconds(2.0f);

		RpcCreateView();
		yield return null;
	}
		
	[ClientRpc]
	void RpcCreateView(){
		Debug.Log("aa");
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

}
