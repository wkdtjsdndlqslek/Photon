using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerEntry : MonoBehaviour
{
	public ToggleGroup characterSelectToggleGroup;
	public Text playerNameText;
	public Toggle readyToggle;
	public GameObject ready;

	private List<Toggle> selectToggles = new List<Toggle>();

	public Player player;

	public bool isMine => player == PhotonNetwork.LocalPlayer;

	private void Awake()
	{
		foreach (Transform toggleTransform in characterSelectToggleGroup.transform)
		{
			selectToggles.Add(toggleTransform.GetComponent<Toggle>());
		}
	}

    private void Start()
    {
		Hashtable customProperties = player.CustomProperties;
		if(false == customProperties.ContainsKey("CharacterSelect"))
		{
			customProperties.Add("CharacterSelect", 0);
		}

		int select = (int)customProperties["CharacterSelect"];
		selectToggles[select].isOn = true;

		if(isMine)
		{
			for (int i = 0; i < selectToggles.Count; i++)
			{
				int index = i; //일부러 익명 메소드에 변수를 캡쳐하기 위해 로컬 변수를 새로 생성
				selectToggles[i].onValueChanged.AddListener(
					isOn =>
					{
						if (isOn)
						{
							Hashtable customProperties = player.CustomProperties;
							customProperties["CharacterSelect"] = index;
							player.SetCustomProperties(customProperties);
						}
					});
			}
		}
		else
		{
			for(int i= 0; i<selectToggles.Count; i++)
			{
				selectToggles[i].interactable = false;
			}
		}
    }

	public void SetSelection(int select)
	{
		if (isMine) return;
		selectToggles[select].isOn = true;
	}
}

