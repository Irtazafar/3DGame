using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject distanceDisplay;
    public int disRun;
    public bool addingTotalDistance = false;

   

    // Update is called once per frame
    void Update()
    {
        if(addingTotalDistance==false)
        {
            addingTotalDistance = true;
            StartCoroutine(AddDistance());
        }
    }

    IEnumerator AddDistance()
    {
        disRun += 1;
        distanceDisplay.GetComponent<Text>().text = "" + disRun +"m";
        yield return new WaitForSeconds(0.35f);
        addingTotalDistance =false;
    }
}
