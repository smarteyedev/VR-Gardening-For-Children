using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalItemBasketController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalTomatos;
    [SerializeField] private TextMeshProUGUI _totalOranges;

    private void Start() {
        if (_totalTomatos != null) _totalTomatos.text = 0.ToString();
        if (_totalOranges != null) _totalOranges.text = 0.ToString();
    }
    private void Update() {
        if(_totalTomatos != null) _totalTomatos.text = GameManager.Instance.GetTomatoCount().ToString();
        if(_totalOranges != null) _totalOranges.text = GameManager.Instance.GetOrangeCount().ToString();
    }
}
