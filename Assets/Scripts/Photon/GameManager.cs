
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using Photon.Pun;


    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject winnerUI;

        public GameObject player1SpawnPosition;
        public GameObject player2SpawnPosition;


        private GameObject player1;
        private GameObject player2;

        // Start Method
        void Start()
        {
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
            
        }


       
        // Update Method
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //1
            {
                //Application.Quit();
                EndGame();
            }
        }

        // Photon Methods
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Launcher");
                EndGame();
            }
        }

        //Helper Methods
        public void QuitRoom()
        {
            Application.Quit();
        }

        public void EndGame()
        {
            /*int player1Coins = CollectibleController.GetPlayerCoins(photonView);
            int player2Coins = CollectibleController.GetPlayerCoins(player2);

            Debug.Log("Player 1 Coins: " + player1Coins);
            Debug.Log("Player 2 Coins: " + player2Coins);

            if (player1Coins > player2Coins)
            {
                Debug.Log("Player 1 wins!");
            }
            else if (player2Coins > player1Coins)
            {
                Debug.Log("Player 2 wins!");
            }
            else
            {
                Debug.Log("It's a tie!");
            }*/
        }
    }

