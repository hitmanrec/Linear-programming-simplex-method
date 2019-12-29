using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapScreensBtn : MonoBehaviour
{
    public GameObject screen1, screen2;

    public void activate()
    {
        screen1.SetActive(!screen1.activeSelf);
        screen2.SetActive(!screen2.activeSelf);
    }
}
