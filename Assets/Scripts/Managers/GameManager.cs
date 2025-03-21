using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gamePaused;
    public bool gameOver;
    public bool gameWin;
    public bool interacting;
    public bool dashing;
    public bool cannotMove;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ResetProperties()
    {
        gamePaused = false;
        gameOver = false;
        gamePaused = false;
        gameOver = false;
        gameWin = false;
        interacting = false;
        dashing = false;
        cannotMove = false;
}
}
