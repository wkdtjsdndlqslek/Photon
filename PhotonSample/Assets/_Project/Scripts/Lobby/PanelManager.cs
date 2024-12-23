using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PanelManager : MonoBehaviourPunCallbacks
{
	public static PanelManager Instance;

	public LoginPanel login;
	public MenuPanel menu;
	public LobbyPanel lobby;
	public RoomPanel room;

	
	Dictionary<string, GameObject> panelDic;

	private void Awake() {
		Instance = this;
		panelDic = new Dictionary<string, GameObject>()
		{
			{ "Login", login.gameObject},
			{ "Menu", menu.gameObject},
			{ "Lobby", lobby.gameObject},
			{ "Room", room.gameObject }
		};

		PanelOpen("Login");
	}

	public void PanelOpen(string panelName) {
		foreach (var row in panelDic) {
			row.Value.SetActive(row.Key == panelName);
		}
	}

    public override void OnEnable()
    {
		base.OnEnable(); 
		//MonoBehaviourPunCallbacks�� ����� Ŭ������ OnEnable��
		//������ �� �� �� �θ��� OnEnable�� ȣ���ؾ� ��
		//print("����");
    }

    public override void OnConnected() //���� ������ ���� �Ǿ��� �� ȣ��
    {
		PanelOpen("Menu");

    }


    public override void OnCreatedRoom() //���� �����Ͽ��� �� ȣ��
    {
		PanelOpen("Room");
    }

    public override void OnLeftRoom() //�濡�� ������ �� ȣ��
    {
		PanelOpen("Menu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
		LogManager.Log($"�α׾ƿ� ��: {cause}");
		PanelOpen("Login");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        room.JoinPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        room.LeavePlayer(otherPlayer);
    }

    public override void OnJoinedLobby()
    {
		PanelOpen("Lobby");
    }

    public override void OnJoinedRoom() //�濡 ����
    {
		PanelOpen("Room");
    }

    public override void OnLeftLobby()
    {
		PanelOpen("Menu");
    }

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		lobby.UpdateRoomList(roomList);
	}
}

