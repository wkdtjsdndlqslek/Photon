using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
	public RectTransform roomListRect;

	public GameObject roomButtonPrefab;

	public Button cancelButton;
	private List<RoomInfo> currentRoomList = new List<RoomInfo>();

	private void Awake()
	{
        cancelButton.onClick.AddListener(CancelButtonClick);
	}

    private void OnEnable()
    {
		foreach (RoomInfo roomInfo in currentRoomList)
		{
			AddRoomButton(roomInfo);
		}
    }

    private void OnDisable()
	{
		foreach(Transform child in roomListRect)
		{
			Destroy(child.gameObject);
		}
	}

	public void UpdateRoomList(List<RoomInfo> roomList)
	{
		print(roomList.Count);
        //���� RoomList���� �ִµ�, OnRoomListUpdate�� �Ķ���ͷ� �Ѿ��
        //RoomList���� ���� ���� ���� ��ư�� �����ؾ� ��.
        List<RoomInfo> destroyCandidate;//�ı� �ĺ�
		destroyCandidate = currentRoomList.FindAll(x=>false == roomList.Contains(x));
		
		//currentRoomList���� ���µ� roomList���� �ִ� �� ���� ��ư�����ϱ�
		foreach(RoomInfo roomInfo in roomList)
		{
            if (currentRoomList.Contains(roomInfo)) continue;
			AddRoomButton(roomInfo);
		}

		//destroyCandidate ����Ʈ�� �ִ� �� ���� ��ư ����
		foreach(Transform child in roomListRect)
		{
			if(destroyCandidate.Exists(x=>x.Name == child.name))
			{
				Destroy(child.gameObject);
			}
		}
		currentRoomList = roomList;
	}

	//�� �����Ͱ� ������, �� ���� ��ư�� ������ �޼���
	public void AddRoomButton(RoomInfo roomInfo)
	{
		print("addroomButton");
		GameObject joinButton = Instantiate(roomButtonPrefab, roomListRect, false);
		joinButton.name = roomInfo.Name;
		joinButton.GetComponent<Button>().onClick.AddListener(()=>PhotonNetwork.JoinRoom(roomInfo.Name));
		joinButton.GetComponentInChildren<Text>().text = roomInfo.Name;
	}

    private void CancelButtonClick()
    {
		PhotonNetwork.LeaveLobby();
    }
}

