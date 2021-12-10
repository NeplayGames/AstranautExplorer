using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
    [SerializeField] ScoreCounter scoreCounter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Astraunut"))
        {
            scoreCounter.GameWon();
        }
    }
}
