using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour
{
    public static int coinTotal = 0;

    public GameObject coinTextDisplay;
    public GameObject endcoinTextDisplay;

    // Update is called once per frame
    void Update()
    {
        coinTextDisplay.GetComponent<Text>().text = "" + coinTotal;
        endcoinTextDisplay.GetComponent<Text>().text = "" + coinTotal;
    }
}
