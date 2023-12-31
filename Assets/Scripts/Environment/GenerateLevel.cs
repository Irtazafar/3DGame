using Photon.Pun;
using System.Collections;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public int zPos = 49;
    public bool creatingSection = false;
    public int secNum;

    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, 3);

        // Instantiate the section using Photon's network instantiation
        PhotonNetwork.Instantiate(sections[secNum].name, new Vector3(0, 0, zPos), Quaternion.identity);

        zPos += 49;
        yield return new WaitForSeconds(3);
        creatingSection = false;
    }
}
