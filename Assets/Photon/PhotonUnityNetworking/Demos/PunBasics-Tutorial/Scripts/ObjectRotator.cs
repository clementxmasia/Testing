using UnityEngine;
using Photon.Pun;

public class ObjectRotator : MonoBehaviourPun, IPunObservable
{
    public float rotationSpeed = 30.0f; // Adjust the rotation speed as needed
    public Vector3 rotationAxis = Vector3.up; // Change the axis of rotation as needed

    private bool isGreen = false; // Tracks if the object is green

    private Renderer rend; // Reference to the Renderer component

    private void Start()
    {
        rend = GetComponent<Renderer>(); // Get the Renderer component once during initialization
    }

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

                if (rend != null)
                {
                    rend.material.color = Color.green;
                }
                else
                {
                    Debug.LogError("Renderer component not found on the object.");
                }

                photonView.RPC("SyncColor", RpcTarget.Others, isGreen);
            }
        }
    }

    [PunRPC]
    void SyncColor(bool green)
    {
        isGreen = green;
        if (rend != null)
        {
            rend.material.color = isGreen ? Color.green : Color.white;
        }
        else
        {
            Debug.LogError("Renderer component not found on the object.");
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
                if (rend != null)
                {
                    rend.material.color = isGreen ? Color.green : Color.white;
                }
                else
                {
                    Debug.LogError("Renderer component not found on the object.");
                }
            }
        }
    }
}
