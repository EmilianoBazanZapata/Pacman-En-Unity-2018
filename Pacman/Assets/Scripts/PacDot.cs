﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Pacman")
        {
            Destroy(this.gameObject);
            UIManager.SharedInstance.ScorePoints(100);
            GameManager.SharedInstance.FinishGame(1);
        }
    }
}