using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] WayPoints;
    int CurrentWayPoint = 0; public float Speed = 5.8f;
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    private void FixedUpdate()
    {
        if (GameManager.SharedInstance.GameStarted && !GameManager.SharedInstance.GamePaused)
        {
            GetComponent<AudioSource>().volume = 0.3f;
            animator.enabled = true;
            //distancia entre el fantasme y el punto del destino
            float DistanceToWayPoint = Vector2.Distance((Vector2)this.transform.position,
                                                        (Vector2)WayPoints[CurrentWayPoint].position);

            if (DistanceToWayPoint < 0.1)
            {
                CurrentWayPoint = (CurrentWayPoint + 1) % WayPoints.Length;
                Vector2 NewDirection = WayPoints[CurrentWayPoint].position - this.transform.position;
                animator.SetFloat("DirX", NewDirection.x);
                animator.SetFloat("DirY", NewDirection.y);
            }
            else
            {
                Vector2 NewPos = Vector2.MoveTowards(this.transform.position,
                                                     WayPoints[CurrentWayPoint].position,
                                                     Speed);
                rigidbody2D.MovePosition(NewPos);
            }
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
            animator.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pacman")
        {
            Debug.Log("pacman morira");
            //Destroy(other.gameObject);
        }
    }
}
