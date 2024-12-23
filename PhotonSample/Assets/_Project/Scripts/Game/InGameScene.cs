using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        ChatManager.Instance.ConnectUsingSettings();
        ChatManager.Instance.ChatStart(GameManager.Instance.GetRoomName());
    }
}
