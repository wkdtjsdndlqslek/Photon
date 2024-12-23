using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
	public InputField idInput;
	public InputField pwInput;
	public Button createButton;
	public Button loginButton;
    public Button chatButton;

    private void Awake()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
        chatButton.onClick.AddListener(OnChatButtonClick);
    }

    private void OnChatButtonClick()
    {
        PhotonNetwork.LoadLevel("ChatScene");
    }

    private void OnLoginButtonClick()
    {
        string userNickname = idInput.text;
        PhotonNetwork.NickName = userNickname;
        PhotonNetwork.ConnectUsingSettings();
    }
}

