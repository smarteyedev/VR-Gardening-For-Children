using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [Header("Depedency")]
    [SerializeField] private BadgeController _badgeController;


    [Header("Score")]
    [SerializeField] private int score = 0;
    [SerializeField] private int scorePerItem = 10;

    [Header("Item Counts")]
    [SerializeField] private int tomatoCount = 0;
    [SerializeField] private int orangeCount = 0;

    public static event Action<int> OnScoreChanged;
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
    }

    //void Update() {
    //    if (Input.GetKeyDown(KeyCode.T)) {
    //        AddTomato();
    //    }
    //    if (Input.GetKeyDown(KeyCode.O)) {
    //        AddOrange();
    //    }
    //}

    public void AddScore(int points) {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public int GetScore() {
        return score;
    }

    public int TotalItem() {
        return tomatoCount + orangeCount;
    }

    public void AddTomato() {
        AddScore(scorePerItem);
        tomatoCount++;
        _badgeController.UpdateBadge();

        Debug.Log("Tomato Added, total tomato: " + tomatoCount + "|| total score: " + score + "|| total item: " + TotalItem());
    }

    public void AddOrange() {
        AddScore(scorePerItem);
        orangeCount++;
        _badgeController.UpdateBadge();

        Debug.Log("Orange Added, total Ora: " + orangeCount + "|| total score: " + score + "|| total item: " + TotalItem());
    }

    public void AddTomatoWithoutScore() {
        tomatoCount++;
        Debug.Log("Tomato Added, total tomato: " + tomatoCount + "|| total score: " + score + "|| total item: " + TotalItem());
    }

    public void AddOrangeWithoutScore() {
        orangeCount++;
        Debug.Log("Orange Added, total Ora: " + orangeCount + "|| total score: " + score + "|| total item: " + TotalItem());
    }
    public int GetTomatoCount() {
        return tomatoCount;
    }

    public int GetOrangeCount() {
        return orangeCount;
    }

    public void ReduceTomato() {
        tomatoCount--;
        Debug.Log("Tomato Added, total tomato: " + tomatoCount + "|| total score: " + score + "|| total item: " + TotalItem());
    }

    public void ReduceOrange() {
        orangeCount--;
        Debug.Log("Orange Added, total Ora: " + orangeCount + "|| total score: " + score + "|| total item: " + TotalItem());
    }
}
