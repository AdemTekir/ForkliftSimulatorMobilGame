using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartonsStateConroller : MonoBehaviour
{
    public InGameMenuEdit gameOverMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            gameOverMenu.GameLoseMenu("YÜKÜNÜ DÜÞÜRDÜN!");
        }
        if (other.CompareTag("Platform"))
        {
            gameOverMenu.GameLoseMenu("PLATFORMA VURDUN!");
        }
    }
}
