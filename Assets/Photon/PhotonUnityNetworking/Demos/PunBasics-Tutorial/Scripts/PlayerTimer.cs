using UnityEngine;
using Photon.Pun;

public class PlayerTimer : MonoBehaviourPunCallbacks
{
    public int kickTime = 60; // Set the kick time in seconds
    public bool isTimerRunning = false;
    
    private float timer;

    void Start()
    {
        if (photonView.IsMine)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                // Kick out the player
                photonView.RPC("KickPlayer", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void KickPlayer()
    {
        PhotonNetwork.Disconnect();
    }

    void StartTimer()
    {
        isTimerRunning = true;
        timer = kickTime;
    }
}
