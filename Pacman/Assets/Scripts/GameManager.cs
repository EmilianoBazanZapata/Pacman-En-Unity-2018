using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public bool GameStarted = false;
    public bool GamePaused = false;
    public AudioClip PauseAudio;
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        StartCoroutine("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GamePaused = !GamePaused;
            if(GamePaused)
            {
                PlayPause();
            }
            else
            {
                PauseMusic();
            }
        }

    }
    //Coroutina para iniciar el juego
    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        GameStarted = true;
    }
    //ejecutar el tema de pausa
    private void PlayPause()
    {

        AudioSource source = GetComponent<AudioSource>();
        source.clip = PauseAudio;
        source.loop = true;
        source.Play();
    }
    //pausar musica
    private void PauseMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}
