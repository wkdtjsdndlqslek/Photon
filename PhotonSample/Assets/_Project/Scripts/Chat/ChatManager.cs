using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Chat.Demo;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ChatAuthValues = Photon.Chat.AuthenticationValues;

//����ê�� ����ϱ� ���ؼ� 1. IChatClientListener �������̽� ����
public class ChatManager : MonoBehaviour, IChatClientListener
{
    public static ChatManager Instance { get; private set; }
    public JoinUI joinUI;
    public ChatUI chatUI;

    private ChatClient client;

    public ChatState state = 0;

    public string currentChannel;

    private void Awake()
    {
        Instance = this;
    }

    //2. ChatClient�� ����
    private void Start()
    {
        client = new ChatClient(this);
    }

    //3. update���� Service�� ȣ��
    private void Update()
    {
        client.Service();
    }

    public void SetNickname(string nickname)
    {
        //PhotonNetwork.NickName = nickname;
        client.AuthValues = new ChatAuthValues(nickname);
    }

    public void ConnectUsingSettings()
    {//PhotonServerSettings�� ����Ͽ� ������ ���
        AppSettings appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        ChatAppSettings chatSettings = appSettings.GetChatSettings();
            //new ChatAppSettings
        //{
        //    AppIdChat = appSettings.AppIdChat,
        //};
        client.ConnectUsingSettings(chatSettings);
    }
    
    public void ConnectUsingAppId()
    {//�⺻������ AppId�� ���� ������ ���
        string chatId = "62f839cd-5e8d-4d17-b424-4c935d66375e";
        client.Connect(chatId, "1.0", client.AuthValues);
    }

    //Ư�� ä�ù�(ä�� ä��)���� ä�� ����
    public void ChatStart(string roomName)
    {
        print($"{roomName}");
        client.Subscribe(new string[] { roomName });
    }

    //ä�� �޽��� ����
    public void SendChatMessage(string message)
    {
        client.PublishMessage(currentChannel, message);
    }

    public void OnChatStateChange(ChatState state)
    {
        if(this.state !=state)
        {
            print($"Chat state changed : {state}");
            this.state = state;
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        print("�����ߴ�");
        currentChannel = channels[0];
        //joinUI.gameObject.SetActive(false);
        chatUI.gameObject.SetActive(true);
        chatUI.roomNameLabel.text = channels[0];
        chatUI.ReceiveChatMessage("", $"<color=green>{currentChannel} ä�ù濡 �����Ͽ����ϴ�.</color>");
    }

    public void OnConnected()
    {
        print("OnConnected");
        //joinUI.OnJoinedServer();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if(channelName != currentChannel)
        {
            print($"�ٸ� ä���� �޽��� ������: {channelName}");
            return;
        }
        for (int i = 0; i < senders.Length; i++)
        {
            chatUI.ReceiveChatMessage(senders[i], messages[i].ToString());
        }
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnDisconnected()
    {
        
    }


    public void OnPrivateMessage(string sender, object message, string channelName)
    {
     
    }

    public void OnUnsubscribed(string[] channels)
    {
     
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
     
    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {
     
    }
}
