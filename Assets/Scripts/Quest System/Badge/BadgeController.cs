using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeController : MonoBehaviour {
    [SerializeField] private BadgeItem[] badges;
    [SerializeField] private int[] badgeThresholds = { 1, 7, 16, 26 };

    [SerializeField] private BadgeItem[] badgesItemBasket;

    public static event Action<int> OnBadgeUnlocked;
    public void UnlockBadge(int badgeIndex) {
        if (badgeIndex >= 0 && badgeIndex < badges.Length && !badges[badgeIndex].GetIsBadgeUnlocked()) {
            badges[badgeIndex].SetBadgeUnlocked();
            OnBadgeUnlocked?.Invoke(badgeIndex);

        } else {
            Debug.LogWarning("Invalid badge index: " + badgeIndex);
        }
    }

    public void UpdateBadge() {
        int totalItems = GameManager.Instance.TotalItem();
        for (int i = 0; i < badgeThresholds.Length; i++) {
            if (totalItems >= badgeThresholds[i]) {
                UnlockBadge(i);
                if (i == 0) {
                 badgesItemBasket[0].SetBadgeUnlocked();
                }
                if (i == 1) {
                 badgesItemBasket[1].SetBadgeUnlocked();
                 badgesItemBasket[1].gameObject.SetActive(true);
                } 
            }
        }
    }
}
