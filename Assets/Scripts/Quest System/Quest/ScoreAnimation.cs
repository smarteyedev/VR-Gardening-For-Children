using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    [SerializeField] private UpwardUIAnimation upwardAnim;
    [SerializeField] private TextMeshProUGUI scoreTextAnim;

    private void OnEnable() {
        GameManager.OnScoreChanged += SetText;
    }

    private void OnDisable() {
        GameManager.OnScoreChanged -= SetText;
    }

    private void SetText(int score) {
        scoreTextAnim.text = $"+{score}";
        upwardAnim.PlayAnimation();
    }
}
