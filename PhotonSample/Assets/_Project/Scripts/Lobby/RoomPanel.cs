using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public enum Difficulty
{
	Easy = 0,
	Normal,
	Hard,
}

public class RoomPanel : MonoBehaviour
{
	public Text roomTitleText;

	public Difficulty roomDifficulty;

	public Dropdown difficultyDropdown;

	public Text difficultyText;

	public RectTransform playerList;
	public GameObject playerTextPrefab;

	public Dictionary<int, PlayerEntry> playerListDic = new Dictionary<int, PlayerEntry>();

	public Button startButton;
	public Button cancelButton;

	private Dictionary<int, bool> playersReady;

	private void Awake()
	{
        startButton.onClick.AddListener(StartButtonClick);
        cancelButton.onClick.AddListener(CancelButtonClick);
		difficultyDropdown.ClearOptions();
		foreach (object difficulty in Enum.GetValues(typeof(Difficulty)))
		{
			Dropdown.OptionData option = new Dropdown.OptionData(difficulty.ToString());
			difficultyDropdown.options.Add(option);
		}
        difficultyDropdown.onValueChanged.AddListener(DifficultyValueChange);
	}

    private void OnEnable()
    {
        foreach (Transform child in playerList)
		{//플레이어 리스트에 다른 객체가 있으면 일단 모두 삭제
			Destroy(child.gameObject);
		}
		if (false == PhotonNetwork.InRoom) return;

		roomTitleText.text = PhotonNetwork.CurrentRoom.Name;

		foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
		{//플레이어 정보 객체 생성
			JoinPlayer(player);
		}
        //방장인 지 여부를 확인해서 활성화/비활성화
        difficultyDropdown.gameObject.SetActive(PhotonNetwork.IsMasterClient); 
		startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
		PhotonNetwork.AutomaticallySyncScene=true;

        ChatManager.Instance.ConnectUsingSettings();
		Invoke("ForInvoke",2f);
    }
	private void ForInvoke()
	{
		ChatManager.Instance.ChatStart(roomTitleText.text);

    }	
    private void CancelButtonClick()
    {
		PhotonNetwork.LeaveRoom();
    }

    private void StartButtonClick()
    {
		PhotonNetwork.LoadLevel("GameScene");
    }

    private void DifficultyValueChange(int value)
    {
		Hashtable customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
		customProperties["Difficulty"] = value;
		PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
    }

	public void OnDifficultyChange(Difficulty value)
	{
		roomDifficulty = value;
		difficultyText.text = value.ToString();
	}


	public void JoinPlayer(Player newPlayer)
	{
		PlayerEntry playerEntry = Instantiate(playerTextPrefab, playerList, false).GetComponent<PlayerEntry>();
		playerEntry.playerNameText.text = newPlayer.NickName;
		playerEntry.player = newPlayer;

		if(PhotonNetwork.LocalPlayer.ActorNumber != newPlayer.ActorNumber)
		{
			playerEntry.readyToggle.gameObject.SetActive(false);
		}
	}

	public void LeavePlayer(Player gonePlayer)
	{
		foreach(Transform child in playerList)
		{
			Player player = child.GetComponent<PlayerEntry>().player;
			if (player.ActorNumber == gonePlayer.ActorNumber)
			{
				Destroy(child.gameObject);
			}
		}
		SortPlayer();
	}

	private void SortPlayer()
	{
		foreach(Transform player in playerList)
		{
            Player playerInfo = player.GetComponent<PlayerEntry>().player;
			player.SetSiblingIndex(playerInfo.ActorNumber);
		}
	}

	public void OnCharacterSelectChange(Player target, Hashtable changes)
	{
		foreach(Transform child in playerList)
		{
			PlayerEntry entry = child.GetComponent<PlayerEntry>();
			if(entry.player == target)
			{
				int selection = (int)changes["CharacterSelect"];
				entry.SetSelection(selection);	
			}
		}
	}
}

