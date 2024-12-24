using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

//포톤 콜백 리스너
public class PhotonManager : MonoBehaviourPunCallbacks
{
    public bool isTestMode;

    private void Start()
    {
        isTestMode = PhotonNetwork.IsConnected == false;
        if(isTestMode)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            GameManager.isGameReady = true;
        }
    }

    public override void OnConnectedToMaster()
    {
        if (!isTestMode) return;
        else
        {
            RoomOptions option = new()
            {
                IsVisible = true,
                MaxPlayers = 8
            };
            PhotonNetwork.JoinOrCreateRoom("TestRoom", option, TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        {
            if(isTestMode)
            {
                GameObject.Find("Canvas/DebugText").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;
                GameManager.isGameReady= true;
            }
        }
    }
}
