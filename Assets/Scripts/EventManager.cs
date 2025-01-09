using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action StartingGame;

    public static event Action EndingGame;

    public static event Action BallCollision;

    public static event Action OnPause;

    public static event Action OffPause;

    public static event Action StartingAd;

    public static event Action EndingAd;

    public static EventManager Instance;

    private void Start()
    {
        Instance = this;
    }

    public void StartGame()
    {
        StartingGame?.Invoke();
    }

    public void EndGame()
    {
        EndingGame?.Invoke();
    }

    public void PauseOn()
    {
        OnPause?.Invoke();
    }

    public void PauseOff()
    {
        OffPause?.Invoke();
    }

    public void StartAd()
    {
        StartingAd?.Invoke();
    }

    public void EndAd()
    {
        EndingAd?.Invoke();
    }

    public void BallCollided()
    {
        BallCollision?.Invoke();
    }
}
