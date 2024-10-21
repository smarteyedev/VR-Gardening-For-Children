using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBasketController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    private void Start() {
        _scoreText.text = 0.ToString();
    }

    private void Update() {
        _scoreText.text = GameManager.Instance.GetScore().ToString();
    }
}
