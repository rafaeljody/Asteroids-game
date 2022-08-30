using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public Player player;
    public GameObject gameOverUI;

    public Text livesText;
    public int lives = 3;

    public Text scoreText;
    public int score = 0;

    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {

        SetScore();
        SetLives();

        // if game over, press enter to play again
        if (lives == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }


    // clear asteroid, setfalse the game over UI, set score and lives, call respawn
    public void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);

        this.lives = 3;
        this.score = 0;
        Respawn();

    }

    // set player back, set invuls, call invuls off
    public void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);

    }

    // invuls off
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }


    // point management
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        // explode, and set it within the pos of asteroid
        this.explosion.Play();
        this.explosion.transform.position = asteroid.transform.position;

        // increased score
        if (asteroid.size < 0.75f)
        {
            score += 100;
        } 
        else if (asteroid.size < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

    }

    // died management
    public void PlayerDied()
    {
        // explode, and set it within the pos of player
        this.explosion.Play();
        this.explosion.transform.position = this.player.transform.position;

        // minus lives
        this.lives--;
        
        // if lives 0 gameOver, if not respawn
        if(this.lives == 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }

    }

    // game over state
    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }
    
    // set score in UI
    private void SetScore()
    {
        scoreText.text = this.score.ToString();
    }

    //set lives in UI
    private void SetLives()
    {
        livesText.text = this.lives.ToString();
    }




}
