using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public AudioSource crashSound;
    public GameObject _player;
    public GameObject charModel;
    void OnTriggerEnter(Collider other)
    {
        
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        _player.GetComponent<PlayerMovement>().enabled = false;
        charModel.GetComponent<Animator>().Play("Stumble Backwards");
        crashSound.Play();
    }
}
