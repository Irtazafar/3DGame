
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private CollectibleController collectibleController;

    private GameObject levelControl;

    public GameObject pauseScreen;
    public GameObject winnerScreen;
    public GameObject winnerBox;
    public GameObject loserBox;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI loserText;


    public GameObject player1SpawnPosition;
    public GameObject player2SpawnPosition;

    private GameObject player1;
    private GameObject player2;

    private bool gameEnded = false;
    private int masterNum = 1;
    private int clientNum = 2;
    private float waitTime = 30f;
    PhotonView photonView;

    // Start Method
    void Start()
    {
        Debug.developerConsoleVisible = true;
        if (!PhotonNetwork.IsConnected) // 1
        {
            //SceneManager.LoadScene("Launcher");
            return;
        }

        if (PhotonNetwork.IsMasterClient) // 2
        {
            Debug.Log("Instantiating Player 1");
            // 3
            player1 = PhotonNetwork.Instantiate("Player",
                player1SpawnPosition.transform.position,
                player1SpawnPosition.transform.rotation, 0);


        }
        else // 5
        {
            player2 = PhotonNetwork.Instantiate("Player",
                player2SpawnPosition.transform.position,
                player2SpawnPosition.transform.rotation, 0);

        }
        Time.timeScale = 1f;

        StartCoroutine(AutoDeclareWinner());
        levelControl = GameObject.Find("LevelControl");
        photonView = levelControl.GetComponent<PhotonView>();
    }



    // Update Method
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //1
        {
            Application.Quit();
            //EndGame();
            //DeclareWinner();
        }
    }

    private IEnumerator AutoDeclareWinner()
    {
        yield return new WaitForSeconds(waitTime);
        if (!gameEnded)
        {
            DeclareWinner();
            gameEnded = true;
        }
    }

    // Photon Methods
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("MainMenu");
        }
    }


    /*    public void EndGame()
        {
            levelControl = GameObject.Find("LevelControl");
            collectibleController = levelControl.GetComponent<CollectibleController>();
            Dictionary<int, int> playerCoinCounts = collectibleController.GetPlayerCoinCounts();

            int maxCoins = 0;
            int maxCoinsPlayerActorNumber = -1;

            foreach (KeyValuePair<int, int> playerCoins in playerCoinCounts)
            {
                Debug.Log("Player " + playerCoins.Key + " has collected " + playerCoins.Value + " coins.");
                Debug.LogError("Player " + playerCoins.Key + " has collected " + playerCoins.Value + " coins.");
                if (playerCoins.Value > maxCoins)
                {
                    maxCoins = playerCoins.Value;
                    maxCoinsPlayerActorNumber = playerCoins.Key;
                }
            }

            if (maxCoinsPlayerActorNumber != -1)
            {
                Debug.Log("Player " + maxCoinsPlayerActorNumber + " has collected the most coins: " + maxCoins);
                Debug.LogError("Player " + maxCoinsPlayerActorNumber + " has collected the most coins: " + maxCoins);
            }

        }*/

    #region WINNER
    public void DeclareWinner()
    {
        levelControl = GameObject.Find("LevelControl");
        collectibleController = levelControl.GetComponent<CollectibleController>();
        photonView = levelControl.GetComponent<PhotonView>();
        Dictionary<int, int> playerCoinCounts = collectibleController.GetPlayerCoinCounts();

        int maxCoins = int.MinValue;
        int winnerActorNumber = -1;

        int maxCoinsPlayer1 = -1;
        int maxCoinsPlayer2 = -1;

        int winnerActorNumberPlayer1 = -1;
        int winnerActorNumberPlayer2 = -1;

        foreach (var kvp in playerCoinCounts)
        {
            Debug.Log("Player " + kvp.Key + " has " + kvp.Value + " coins.");
            Debug.LogError("Player " + kvp.Key + " has " + kvp.Value + " coins.");
            if (kvp.Key == 1)
            {
                if (kvp.Value > maxCoinsPlayer1)
                {
                    Debug.Log("Player 1 coins: " + kvp.Value + " > maxCoinsPlayer1: " + maxCoinsPlayer1);
                    Debug.LogError("Player 1 coins: " + kvp.Value + " > maxCoinsPlayer1: " + maxCoinsPlayer1);
                    maxCoinsPlayer1 = kvp.Value;
                    winnerActorNumberPlayer1 = kvp.Key;
                }
            }
            else if (kvp.Key == 2)
            {
                if (kvp.Value > maxCoinsPlayer2)
                {
                    Debug.Log("Player 2 coins: " + kvp.Value + " > maxCoinsPlayer2: " + maxCoinsPlayer2);
                    Debug.LogError("Player 2 coins: " + kvp.Value + " > maxCoinsPlayer2: " + maxCoinsPlayer2);
                    maxCoinsPlayer2 = kvp.Value;
                    winnerActorNumberPlayer2 = kvp.Key;
                }
            }
        }

        /*foreach (var kvp in playerCoinCounts)
        {
            if (kvp.Value > maxCoins)
            {
                maxCoins = kvp.Value;
                winnerActorNumber = kvp.Key;
            }
        }*/

        /* if (winnerActorNumber != -1)
         {
             DeclareWinner(winnerActorNumber);
         }
         else
         {
             DeclareWinner(0);
         }*/
        Debug.Log("maxCoinsPlayer1: " + maxCoinsPlayer1);
        Debug.Log("maxCoinsPlayer2: " + maxCoinsPlayer2);

        if (maxCoinsPlayer1>maxCoinsPlayer2)
        {
            //Debug.Log("Player 1");
            //Debug.LogError("Player 1");
            DeclareWinner(winnerActorNumberPlayer1);
        }else if(maxCoinsPlayer1<maxCoinsPlayer2)
        {
            //Debug.Log("Player 2");
            //Debug.LogError("Player 2");

            DeclareWinnerTwo(winnerActorNumberPlayer2);
        }else
        {
            DeclareWinner(0);
        }
    }

    public void DeclareWinner(int winnerActorNumber)
    {
        photonView.RPC("DeclareWinnerRPC", RpcTarget.All, winnerActorNumber);
    }
    public void DeclareWinnerTwo(int winnerActorNumber)
    {
        photonView.RPC("DeclareLoserRPCTwo", RpcTarget.All, winnerActorNumber);
    }

    [PunRPC]
    public void DeclareWinnerRPC(int winnerActorNumber)
    {
        //Debug.Log("Player 1 RPC");
        winnerScreen.SetActive(true);
        if (winnerActorNumber == masterNum)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //Debug.Log("MASTER 1");
                //Debug.LogError("MASTER 1");
                winnerText.text = "You won maximum coins!";
                winnerBox.SetActive(true);
            }
            else
            {
                //Debug.Log("ClIENT 1");
                //Debug.LogError("ClIENT 1");
                loserText.text = "You Lost!";
                loserBox.SetActive(true);
            }
        }else if (winnerActorNumber == 0)
        {
            // Display text for a tied match
            winnerText.text = "MATCH TIED";
            winnerBox.SetActive(true);
        }



        Time.timeScale = 0f;
    }

    [PunRPC]
    public void DeclareLoserRPCTwo(int winnerActorNumber)
    {
        //Debug.Log("Player 2 RPC");
        winnerScreen.SetActive(true);
        if (winnerActorNumber == clientNum)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //Debug.Log("MASTER 2");
                //Debug.LogError("MASTER 2");
                loserText.text = "You Lost!";
                loserBox.SetActive(true);
            }
            else
            {
                //Debug.Log("ClIENT 2");
                //Debug.LogError("ClIENT 2");
                winnerText.text = "You won maximum coins!";
                winnerBox.SetActive(true);
            }

        }
        else if (winnerActorNumber == 0)
        {
            // Display text for a tied match
            winnerText.text = "MATCH TIED";
            winnerBox.SetActive(true);
        }



        Time.timeScale = 0f;
    }

    #endregion 

    #region PAUSE BUTTON
    public void PauseButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PauseAllPlayers();
        }
        else
        {
            PauseSinglePlayer();
        }
    }

    private void PauseAllPlayers()
    {
        photonView.RPC("PauseAllPlayerRPC", RpcTarget.All);
    }

    [PunRPC]
    public void PauseAllPlayerRPC()
    {
        levelControl = GameObject.Find("LevelControl");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        levelControl.GetComponent<GenerateLevel>().enabled = false;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    private void PauseSinglePlayer()
    {
        levelControl = GameObject.Find("LevelControl");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        levelControl.GetComponent<GenerateLevel>().enabled = false;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    #endregion


    #region Resume Button

    public void ResumeButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ResumeAllPlayers();
        }
        else
        {
            ResumeSinglePlayer();
        }
    }

    private void ResumeAllPlayers()
    {
        photonView.RPC("ResumeAllPlayerRPC", RpcTarget.All);
    }

    [PunRPC]
    public void ResumeAllPlayerRPC()
    {
        Time.timeScale = 1f;
        levelControl = GameObject.Find("LevelControl");
        levelControl.GetComponent<LevelDistance>().enabled = true;
        levelControl.GetComponent<GenerateLevel>().enabled = true;

        pauseScreen.SetActive(false);
    }

    private void ResumeSinglePlayer()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Time.timeScale = 1f;
            levelControl = GameObject.Find("LevelControl");
            levelControl.GetComponent<LevelDistance>().enabled = true;
            levelControl.GetComponent<GenerateLevel>().enabled = true;

            pauseScreen.SetActive(false);
        }
    }
    #endregion


    #region Exit Button 
    public void QuitRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion
}

