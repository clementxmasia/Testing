using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomSwitcher : MonoBehaviour
{
    public string[] roomNames; // Define an array of room names
    public int targetRoomIndex = 1; // Set the target room index

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonNetwork.LeaveRoom(); // Leave the current room
        }
    }

    public void OnLeftRoom()
    {
        if (targetRoomIndex >= 0 && targetRoomIndex < roomNames.Length)
        {
            string targetRoomName = roomNames[targetRoomIndex];
            PhotonNetwork.JoinOrCreateRoom(targetRoomName, new RoomOptions(), TypedLobby.Default);
        }
        else
        {
            Debug.LogWarning("Invalid target room index.");
        }
    }
}
