using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Pacman")
        {
            GameManager.SharedInstance.MakeInvicibleFor(6.0f);
            UIManager.SharedInstance.ScorePoints(200);
            Destroy(this.gameObject);
        }
    }
}
