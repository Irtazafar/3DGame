using System.Collections;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject liveCoins;
    public GameObject liveDis;
    public GameObject endScreen;

    private void Start()
    {
        StartCoroutine(EndGameScreen());
    }

    IEnumerator EndGameScreen()
    {
        yield return new WaitForSeconds(2);
        liveCoins.SetActive(false);
        liveDis.SetActive(false);
        endScreen.SetActive(true);
        yield return new WaitForSeconds(5);
    }
}
