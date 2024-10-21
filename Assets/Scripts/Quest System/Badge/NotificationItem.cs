using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationItem : MonoBehaviour {
    public Image _icon;
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _description;

    // Method to update the notification's content
    public void UpdateNotification(Sprite icon, string title, string description) {
        if (_icon != null) _icon.sprite = icon;
        if (_title != null) _title.text = title;
        if (_description != null) _description.text = description;
    }
}
