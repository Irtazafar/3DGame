using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _horizontalMoveSpeed = 10f;
    public float 
        _verticalMovementSpeed = 4f;

    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject playerObj;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _horizontalMoveSpeed, Space.World);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(this.gameObject.transform.position.x > LevelControl.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * _verticalMovementSpeed);
            }
           
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(this.gameObject.transform.position.x < LevelControl.rightSide)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _verticalMovementSpeed);
            }
           

        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            
            if(isJumping==false)
            {
                isJumping = true;
                playerObj.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());
            }
                transform.Translate(Vector3.right * Time.deltaTime * _verticalMovementSpeed);


        }

        /* if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
         {
             transform.Translate(Vector3.forward * Time.deltaTime * _horizontalMoveSpeed, Space.World);
         }
         else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
         {
             transform.Translate(Vector3.back * Time.deltaTime * _horizontalMoveSpeed, Space.World);
         }*/

        if(isJumping==true)
        {
            if(comingDown==false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 6, Space.World);
            }
            if (comingDown == true)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -6, Space.World);
            }
        }
    }

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

