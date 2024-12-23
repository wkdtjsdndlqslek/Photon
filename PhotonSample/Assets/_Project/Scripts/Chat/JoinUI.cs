using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinUI : MonoBehaviour
{
	public InputField nicknameInput;
	public InputField roomnameInput;
	public Button nicknameChangeButton;

	public Button connectButton;

	public Button joinRoomButton;
	public Text logText;

    private void Awake()
    {
        nicknameInput.onValueChanged.AddListener(NicknameInputEdit);
        nicknameChangeButton.onClick.AddListener(NicknameChangeButtonClick);
        connectButton.onClick.AddListener(ConnectButtonClick);
        joinRoomButton.onClick.AddListener(JoinRoomButtonClick);
    }

    //�г��� �Է¶��� �Է��� �� ������ ���ڿ� ����
    private void NicknameInputEdit(string input)
    {
        nicknameInput.SetTextWithoutNotify(input.ToValidString());
        logText.text = "";
    }

    //��ȿ�� �г����� �� ������ �ž�.
    //�̤��Ϥ��� <- ��ȿ���� ���� �г���
    private void NicknameChangeButtonClick()
    {
        string nickname = nicknameInput.text;

        if(nickname.NicknameValidate())
        {
            ChatManager.Instance.SetNickname(nickname);
        }
        else
        {
            logText.text = "�г����� ��Ģ���� ����ϴ�.";
        }
        
    }

    private void ConnectButtonClick()
    {
        ChatManager.Instance.ConnectUsingSettings();
        connectButton.interactable = false;
    }

    private void JoinRoomButtonClick()
    {
        ChatManager.Instance.ChatStart(roomnameInput.text);
        roomnameInput.interactable = false;
        joinRoomButton.interactable=false;
    }

    public void OnJoinedServer()
    {
        connectButton.GetComponentInChildren <Text>().text = "ä�� ���� ���ӵ�";
    }
}

