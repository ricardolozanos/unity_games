using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;
    public bool gameStarted = false;
    public bool gamePaused = true;
    public AudioClip pauseAudio,startAudio;
    public float invincibleTime = 0.0f;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        StartCoroutine("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                PlayPauseMusic();
            }
            else
            {
                StopPauseMusic();
            }
        }
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            Debug.Log(invincibleTime);
        }
    }

    void PlayPauseMusic()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = pauseAudio;
        source.loop = true;
        source.Play();
    }
    void StopPauseMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(4.5f);
        gameStarted = true;
        gamePaused = false;
    }

    public void MakeInvincibleFor(float numberOfSeconds)
    {
        this.invincibleTime += numberOfSeconds;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}

