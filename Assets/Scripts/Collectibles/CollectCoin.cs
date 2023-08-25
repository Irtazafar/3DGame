using Photon.Pun;
using UnityEngine;


public class CollectCoin : MonoBehaviourPunCallbacks
{
    //public AudioSource coinFX;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            GameObject levelControl = GameObject.Find("LevelControl");
            CollectibleController collectibleController = levelControl.GetComponent<CollectibleController>();
            if (collectibleController != null)
            {
                int playerActorNumber = photonView.OwnerActorNr;
                collectibleController.CollectCoinLocally(playerActorNumber);
            }
        }
        photonView.RPC("DeactivateCoinRPC", RpcTarget.All);
    }

    [PunRPC]
    private void DeactivateCoinRPC()
    {
        photonView.gameObject.SetActive(false);
    }

}
