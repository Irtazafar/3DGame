using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _horizontalMoveSpeed = 3f;
    public float _verticalMovementSpeed = 4f;

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
        
       /* if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _horizontalMoveSpeed, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _horizontalMoveSpeed, Space.World);
        }*/


    }
}
