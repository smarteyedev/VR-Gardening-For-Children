using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeAnimation : MonoBehaviour
{
    [SerializeField] private UpwardUIAnimation upwardAnim;
    [SerializeField] private Image badgeImage;
    [SerializeField] private Sprite[] _badgeSprite;

    private void OnEnable() {
        BadgeController.OnBadgeUnlocked += SetSprite;
    }

    private void OnDisable() {
        BadgeController.OnBadgeUnlocked -= SetSprite;
    }
    private void SetSprite(int array) {
        badgeImage.sprite = _badgeSprite[array];
        upwardAnim.PlayAnimation();
    }
}
