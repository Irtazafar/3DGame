using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float _horizontalMoveSpeed = 10f;
    public float
        _verticalMovementSpeed = 4f;

    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject playerObj;

    private string playerName = "";
    PhotonView photonView;


    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        playerName = photonView.Owner.NickName;
        gameObject.name = playerName;
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _horizontalMoveSpeed, Space.World);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.gameObject.transform.position.x > LevelControl.leftSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * _verticalMovementSpeed);
                }

            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < LevelControl.rightSide)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * _verticalMovementSpeed);
                }


            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {

                if (isJumping == false)
                {
                    isJumping = true;
                    playerObj.GetComponent<Animator>().Play("Jump");
                    StartCoroutine(JumpSequence());
                }
                transform.Translate(Vector3.right * Time.deltaTime * _verticalMovementSpeed);


            }

            /* if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
             {
                 transform.Translate(Vector3.forward * Time.DeltaTime * _horizontalMoveSpeed, Space.World);
             }
             else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
             {
                 transform.Translate(Vector3.back * Time.deltaTime * _horizontalMoveSpeed, Space.World);
             }*/

            if (isJumping == true)
            {
                if (comingDown == false)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 6, Space.World);
                }
                if (comingDown == true)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * -6, Space.World);
                }
            }

        }
    }

    #region Functions
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _horizontalMoveSpeed, Space.World);
    }

    private void LeftMove()
    {
        transform.Translate(Vector3.left * Time.deltaTime * _verticalMovementSpeed);
    }

    private void RightMove()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _verticalMovementSpeed);
    }
    #endregion


    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        comingDown = false;
        playerObj.GetComponent<Animator>().Play("Standard Run");
    }

}

