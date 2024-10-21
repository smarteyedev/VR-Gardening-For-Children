using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BadgeItem : MonoBehaviour
{
    [SerializeField] private Sprite badgeUnlocked;
    private bool isBadgeUnlocked;


    public void SetBadgeUnlocked() {
        GetComponent<Image>().sprite = badgeUnlocked;
        isBadgeUnlocked = true;
    }

    public bool GetIsBadgeUnlocked() {
        return isBadgeUnlocked;
    }
}
