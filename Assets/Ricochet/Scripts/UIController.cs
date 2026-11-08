using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI p1HealthText;
    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p2HealthText;
    [SerializeField] private TextMeshProUGUI p2ScoreText;
    [SerializeField] private TextMeshProUGUI p3HealthText;
    [SerializeField] private TextMeshProUGUI p3ScoreText;
    [SerializeField] private TextMeshProUGUI p4HealthText;
    [SerializeField] private TextMeshProUGUI p4ScoreText;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject hud;

    [SerializeField] private TextMeshProUGUI p1StatusText;
    [SerializeField] private TextMeshProUGUI p2StatusText;
    [SerializeField] private TextMeshProUGUI p3StatusText;
    [SerializeField] private TextMeshProUGUI p4StatusText;

    [SerializeField] private TextMeshProUGUI finalScores;
    [SerializeField] private TextMeshProUGUI winner;
    


    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        PlayerShip.OnTakeDamage += UpdateHealthText;
        PlayerShip.OnIncreaseScore += UpdateScoreText;
        StartManager.OnStartGame += DisplayHUD;
        PlayerConnection.OnStatusChange += UpdateStartScreen;
        EndManager.OnGameOver += DisplayEndScreen;
    }

    public void Dispose()
    {
        PlayerShip.OnTakeDamage -= UpdateHealthText;
        PlayerShip.OnIncreaseScore -= UpdateScoreText;
        StartManager.OnStartGame -= DisplayHUD;
        PlayerConnection.OnStatusChange -= UpdateStartScreen;
        EndManager.OnGameOver -= Dispose;
    }

    public void UpdateHealthText(string playerPrefix, int newHealth)
    {
        if(playerPrefix.Equals("P1"))
        {
            p1HealthText.SetText("Health: {0}", newHealth);
        }
        else if(playerPrefix.Equals("P2"))
        {
            p2HealthText.SetText("Health: {0}", newHealth);
        }
        else if(playerPrefix.Equals("P3"))
        {
            p3HealthText.SetText("Health: {0}", newHealth);
        }
        else
        {
            p4HealthText.SetText("Health: {0}", newHealth);
        }
    }

    public void UpdateScoreText(string playerPrefix, int newScore)
    {
        if (playerPrefix.Equals("P1"))
        {
            p1ScoreText.SetText("Score: {0}", newScore);
        }
        else if (playerPrefix.Equals("P2"))
        {
            p2ScoreText.SetText("Score: {0}", newScore);
        }
        else if (playerPrefix.Equals("P3"))
        {
            p3ScoreText.SetText("Score: {0}", newScore);
        }
        else
        {
            p4ScoreText.SetText("Score: {0}", newScore);
        }
    }

    public void DisplayHUD(List<PlayerShip> players)
    {
        hud.SetActive(true);
        for(int i=0; i<players.Count; i++)
        {
            players[i].GetHUDPanel().SetActive(true);
        }
        startScreen.SetActive(false);
    }

    public void UpdateStartScreen(string playerPrefix, string status)
    {
        if (playerPrefix.Equals("P1"))
        {
            p1StatusText.SetText(status);
        }
        else if (playerPrefix.Equals("P2"))
        {
            p2StatusText.SetText(status);
        }
        else if (playerPrefix.Equals("P3"))
        {
            p3StatusText.SetText(status);
        }
        else
        {
            p4StatusText.SetText(status);
        }
    }

    public void DisplayEndScreen(List<PlayerShip> players)
    {
        hud.SetActive(false);
        endScreen.SetActive(true);
        List<int> scores = new List<int>();
        finalScores.SetText("Final Scores: ");
        foreach (PlayerShip player in players)
        {
            scores.Add(player.GetScore());
            scores.Sort();
            scores.Reverse();
            //finalScores.SetText(finalScores.text + "\nPlayer: " + player.GetPlayerID() + " ")
        }
    }
}