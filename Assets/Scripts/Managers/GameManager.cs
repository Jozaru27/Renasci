using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRelicSlots;
    public bool gamePaused;
    public bool gameOver;
    public bool gameWin;
    public bool interacting;
    public bool dashing;
    public bool playerCannotMove;
    public bool onInventory;
    public bool inInfo;
    public bool infoShowed;
    public GameObject[] enemies;

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

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void ResetProperties()
    {
        currentRelicSlots = -1;
        gamePaused = false;
        gameOver = false;
        gameWin = false;
        interacting = false;
        dashing = false;
        playerCannotMove = false;
        onInventory = false;
        inInfo = false;
        infoShowed = false;
    }
}
