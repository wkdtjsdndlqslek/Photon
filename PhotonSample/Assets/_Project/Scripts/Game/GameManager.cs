using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int roomCount=0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { DestroyImmediate(this); }
    }

    public string GetRoomName()
    {
        string roomName =roomCount.ToString();
        roomCount++;
        return roomName;
    }
}
