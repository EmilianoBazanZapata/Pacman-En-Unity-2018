using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] WayPoints;
    int CurrentWayPoint = 0; public float Speed = 5.8f;
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public GameObject Home;
    public bool ShouldWaitHome = false;
    private void Update()
    {
        if (GameManager.SharedInstance.InvicibleTime > 0)
        {
            animator.SetBool("Death", true);
        }
        else
        {
            animator.SetBool("Death", false);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.SharedInstance.GameStarted && !GameManager.SharedInstance.GamePaused)
        {
            GetComponent<AudioSource>().volume = 0.3f;
            animator.enabled = true;
            if (!ShouldWaitHome)
            {
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
            if (GameManager.SharedInstance.InvicibleTime <= 0)
            {
                //Debug.Log("pacman morira");
                Destroy(other.gameObject);
                StartCoroutine("RestartGame");
            }
            else
            {
                //enviar al centro del mapa
                this.transform.position = Home.transform.position;
                this.CurrentWayPoint = 0;
                this.ShouldWaitHome = true;
                UIManager.SharedInstance.ScorePoints(1500);
                StartCoroutine("AwakeFromHome");
            }
        }
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        GameManager.SharedInstance.RestartGame();
    }
    IEnumerator AwakeFromHome()
    {
        
        yield return new WaitForSecondsRealtime(3.0f);
        this.ShouldWaitHome = false;
        this.Speed += 0.01f;
    }
}
