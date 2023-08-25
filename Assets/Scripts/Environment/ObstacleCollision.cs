using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public AudioSource crashSound;
    public GameObject _player;
    public GameObject charModel;
    public GameObject levelControl;
    void OnTriggerEnter(Collider other)
    {

        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        _player.GetComponent<PlayerMovement>().enabled = false;
        charModel.GetComponent<Animator>().Play("Stumble Backwards");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        crashSound.Play();
        levelControl.GetComponent<EndGame>().enabled = true;
    }
}
