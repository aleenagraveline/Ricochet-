using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using System;

public class PlayerConnection : Player
{
    [SerializeField] private StartManager startManager;

    private PlayerShip ship;
    private bool gameStarted = false;
    private enum PlayerStatus
    {
        NotConnected,
        NotReady,
        Ready,
    }
    private PlayerStatus status;

    public static event Action<string, string> OnStatusChange;

    void Start()
    {
        Initialize();
        status = PlayerStatus.NotConnected;
        ship = GetComponent<PlayerShip>();
    }

    public void Initialize()
    {
        StartManager.OnStartGame += SetGameStarted;
        EndManager.OnGameOver += Dispose;
    }

    public void Dispose()
    {
        StartManager.OnStartGame -= SetGameStarted;
        EndManager.OnGameOver -= Dispose;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(GetInputPrefix() + "Button1") && !gameStarted)
        {
            switch(status)
            {
                case PlayerStatus.NotConnected:
                    status = PlayerStatus.NotReady;
                    startManager.AddPlayer(ship);
                    OnStatusChange?.Invoke(GetInputPrefix(), "Not Ready");
                    break;
                case PlayerStatus.NotReady:
                    status = PlayerStatus.Ready;
                    OnStatusChange?.Invoke(GetInputPrefix(), "Ready");
                    GetComponent<SpriteRenderer>().enabled = true;
                    break;
                case PlayerStatus.Ready:
                    startManager.StartGame();
                    break;
            }
        }
    }

    public void SetGameStarted(List<PlayerShip> players)
    {
        gameStarted = true;
    }
}
