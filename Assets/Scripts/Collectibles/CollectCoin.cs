using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


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
                collectibleController.CollectCoinLocally();
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
