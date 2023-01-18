using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    //Para calcular la puntuacion final y guardar el highsscore

    public Text scoreText; //puntos por recoger patitos + matar enemigos
    public Text highscoreText; //guarda la puntuación más alta
    public Text timeScoreText; //al final se suma a la puntuacion bonus de puntos por tiempo
                               //cuanto menos tiempo tarde en salir del laberinto, se le suman más puntos
    private int score = 0;
    private int time = 0;
    private int timeScore = 200;
    private int finalScore = 0;
    private int highscore = 0;

    //Start is called before the first frame update
    void Start()
    {
        //highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString("0");
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        //time += (int)Time.deltaTime;
        //timeScore -= time;
        timeScore -= (int)Time.deltaTime;
        scoreText.text = score.ToString("0");
    }
    
    public void AddScore(int enemiesKilled, int collectedPuppets)
    {
        score += enemiesKilled + collectedPuppets;
       
    }
    public void FinalScore(int score, int time, int timeScore)
    {
        finalScore = score + timeScore;
        PlayerPrefs.SetInt("highscore", finalScore);
    }
}
