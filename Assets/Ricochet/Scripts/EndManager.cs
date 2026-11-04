using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndManager : MonoBehaviour
{
    [SerializeField] private List<PlayerShip> deadShips = new List<PlayerShip>();
    public static event Action OnGameOver;
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
            OnGameOver?.Invoke();
            Dispose();
            Debug.Log("Game Over");
        }
    }

    public void AddDeadShip(PlayerShip ship)
    {
        deadShips.Add(ship);
    }
}
