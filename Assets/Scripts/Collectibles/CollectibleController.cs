using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour
{
    public int coinTotal = 0;

    public GameObject coinTextDisplay;
    public GameObject endcoinTextDisplay;

    private PhotonView photonView; // Initialize this in Start or Awake

    private void Start()
    {
        GameObject levelControl = GameObject.Find("LevelControl");
        photonView = levelControl.GetComponent<PhotonView>();
    }


    public void CollectCoinLocally()
    {
        coinTotal += 1;
        coinTextDisplay.GetComponent<Text>().text = "" + coinTotal;
        endcoinTextDisplay.GetComponent<Text>().text = "" + coinTotal;
    }
    public int GetPlayerCoins(GameObject player)
    {
        if (player != null)
        {
            CollectibleController collectibleController = player.GetComponent<CollectibleController>();
            if (collectibleController != null)
            {
                return coinTotal;
            }
        }
        return 0;
    }
}
