using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Pacman")
        {
            GameManager.SharedInstance.MakeInvicibleFor(15.0f);
            Destroy(this.gameObject);
        }
    }
}
