using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] private List<PlayerShip> deadShips = new List<PlayerShip>();
    public static event Action<List<PlayerShip>> OnGameOver;
    [SerializeField] private StartManager start;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        PlayerShip.OnShipDeath += AddDeadShip;
    }

    public void Dispose()
    {
        PlayerShip.OnShipDeath -= AddDeadShip;
    }

    void Update()
    {
        if(deadShips.Count >= start.GetNumPlayers() - 1)
        {
            OnGameOver?.Invoke(start.GetPlayers());
            Dispose();
            Debug.Log("Game Over");
        }

        if(Input.GetButtonDown("P1Button1") || Input.GetButtonDown("P2Button1") || Input.GetButtonDown("P3Button1") || Input.GetButtonDown("P4Button1"))
        {
            SceneManager.LoadScene("Ricochet!");
        }
    }

    public void AddDeadShip(PlayerShip ship)
    {
        deadShips.Add(ship);
    }
}
