using UnityEngine;
using Photon.Pun;

public class ObjectRotator : MonoBehaviourPun, IPunObservable
{
    public float rotationSpeed = 30.0f; // Adjust the rotation speed as needed
    public Vector3 rotationAxis = Vector3.up; // Change the axis of rotation as needed

    private bool isGreen = false; // Tracks if the object is green

    void Update()
    {
        // Rotate the object around the specified axis at a constant speed
        if (photonView.IsMine)
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine && !isGreen)
        {
            // Check if a player has collided with the object
            if (collision.gameObject.CompareTag("Player"))
            {
                // Change the color to green
                isGreen = true;
                GetComponent<Renderer>().material.color = Color.green;
                photonView.RPC("SyncColor", RpcTarget.Others, isGreen);
            }
        }
    }

    [PunRPC]
    void SyncColor(bool green)
    {
        isGreen = green;
        if (green)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending the rotation data to other players
            stream.SendNext(transform.rotation);
            stream.SendNext(isGreen);
        }
        else
        {
            // Receiving the rotation data from the owner
            transform.rotation = (Quaternion)stream.ReceiveNext();
            bool receivedIsGreen = (bool)stream.ReceiveNext();

            if (receivedIsGreen != isGreen)
            {
                // Synchronize the color
                isGreen = receivedIsGreen;
                GetComponent<Renderer>().material.color = isGreen ? Color.green : Color.white;
            }
        }
    }
}
