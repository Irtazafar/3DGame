using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour
{
    public int coinTotal = 0;

    private Dictionary<int, int> playerCoinCounts = new Dictionary<int, int>();

    public GameObject coinTextDisplay;
    public GameObject endcoinTextDisplay;

    private PhotonView photonView;

    private void Start()
    {
        GameObject levelControl = GameObject.Find("LevelControl");
        photonView = levelControl.GetComponent<PhotonView>();
    }


    public void CollectCoinLocally(int playerActorNumber)
    {
        if (!playerCoinCounts.ContainsKey(playerActorNumber))
        {
            playerCoinCounts[playerActorNumber] = 0;
        }

        playerCoinCounts[playerActorNumber] += 1;
        UpdateCoinUI(playerActorNumber);
    }

    private void UpdateCoinUI(int playerActorNumber)
    {
        if (playerCoinCounts.ContainsKey(playerActorNumber))
        {
            int coins = playerCoinCounts[playerActorNumber];
            coinTextDisplay.GetComponent<Text>().text = "" + coins;
            endcoinTextDisplay.GetComponent<Text>().text = "" + coins;
        }
    }

    public Dictionary<int, int> GetPlayerCoinCounts()
    {
        return playerCoinCounts;
    }

    // IPunObservable implementation for synchronization
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data to the network
            stream.SendNext(playerCoinCounts);
        }
        else
        {
            // Receiving data from the network
            playerCoinCounts = (Dictionary<int, int>)stream.ReceiveNext();
        }
    }
}
