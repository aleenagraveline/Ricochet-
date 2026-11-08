using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartManager : MonoBehaviour
{
    private int minPlayers = 2;
    [SerializeField] private List<PlayerShip> players = new List<PlayerShip>();
    [SerializeField] private EndManager end;
    public static event Action<List<PlayerShip>> OnStartGame;

    public void StartGame()
    {
        if (players.Count >= minPlayers && checkReadyPlayers())
        { 
            OnStartGame?.Invoke(players);
            end.gameObject.SetActive(true);
        }
    }

    public int GetMinPlayers()
    {
        return minPlayers;
    }

    public void AddPlayer(PlayerShip player)
    {
        players.Add(player);
    }

    private bool checkReadyPlayers()
    {
        for(int i=0; i<players.Count; i++)
        {
            if (!players[i].gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    public int GetNumPlayers()
    {
        return players.Count;
    }

    public List<PlayerShip> GetPlayers()
    {
        return players;
    }
}
