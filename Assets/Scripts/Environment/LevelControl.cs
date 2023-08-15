using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static float leftSide = -4.5f;
    public static float rightSide = 4.5f;
    public float internalLeft;
    public float internelRight;

    void Update()
    {
        internalLeft = leftSide;
        internelRight = rightSide;
    }
}
