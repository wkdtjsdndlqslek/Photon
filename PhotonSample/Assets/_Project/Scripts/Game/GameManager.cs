using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public Transform playerPositions;
    public static bool isGameReady;

    private void Awake()
    {
        Instance = this;
    }

    //Photon���� ��Ʈ�� ����ȭ �ϴ� ���
    //1.�����տ� PhotonView ������Ʈ�� ���̰�, PhotonNetwork.instantiate�� ���� ���� Ŭ���̾�Ʈ �鿡�Ե� ����ȭ�� ������Ʈ�� �����ϵ��� ��.
    //2.PhotonView�� Observing �� �� �ֵ��� View ������Ʈ�� ����.
    //3.�� View�� �������� ���� ������Ʈ�� ���� �������� �ʵ��� ����ó���� �� ��.
    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>isGameReady);
        yield return new WaitForSeconds(1f);

        //GetPlayerNumber Ȯ���Լ� : ���� ��Ʈ��ũ�� ����� �ٸ� �÷��̾�� ���̿��� ����ȭ �� �÷��̾� ��ȣ.
        //Acter Number�� �ٸ�. (Scene���� ���������� 0~�÷��̾� ����ŭ �ο���)
        //GetPlayerNumber Ȯ���Լ��� �����ϱ� ���ؼ��� ���� PlayerNumbering ������Ʈ�� �ʿ��ϴ�.
        int playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        Vector3 playerPos = playerPositions.GetChild(playerNumber).position;
        GameObject playerObj = PhotonNetwork.Instantiate("Player", playerPos, Quaternion.identity);
        playerObj.name = $"Player {playerNumber}";

        //�� �ؿ����� ���� MasterClient�� �ƴϸ� �������� ����
        if(false == PhotonNetwork.IsMasterClient)
        {
            yield break;
        }

        //Master Client�� 5�ʸ��� Pill�� PhotonNetwork�� ���� Instantiate.
        while(true)
        {
            //PhotonNetwork.Instantiate�� ���� ������ ���, position�� rotation�� �ݵ�� �ʿ�.
            Vector3 spawnPos = Random.insideUnitSphere * 15;
            spawnPos.y = 0;
            Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 180f), 0);

            //�� pill���� random color(color)�� random healAmount(float)�� �����ϰ� ������?
            Vector3 color = new Vector3(Random.value, Random.value, Random.value);
            float healAmount = Random.Range(10f, 30f);
            
            PhotonNetwork.Instantiate("Pill", spawnPos, spawnRot, data:new object[] {color,healAmount});

            yield return new WaitForSeconds(5f);
        }

    }
}
