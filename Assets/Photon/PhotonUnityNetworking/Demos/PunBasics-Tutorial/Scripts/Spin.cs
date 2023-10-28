using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class Spin : MonoBehaviour
{
private bool isSpinning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSpinning)
        {
            // Check if the entering collider is a player and the object is not already spinning.
            isSpinning = true;
            // Start spinning (you can adjust the spinning logic here).
        }
    }

    private void Update()
    {
        if (isSpinning)
        {
            // Perform the spinning logic here (e.g., rotating the object).
            transform.Rotate(Vector3.up, Time.deltaTime * 100f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data to other players.
            stream.SendNext(isSpinning);
        }
        else
        {
            // Receiving data from the network.
            isSpinning = (bool)stream.ReceiveNext();
        }
    }
}