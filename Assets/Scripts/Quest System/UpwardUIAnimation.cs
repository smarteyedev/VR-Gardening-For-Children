using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpwardUIAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private Ease ease;

    private Vector3 originalPosition;
    private Vector3 originalScale;

    private void Start() {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }
    public void PlayAnimation() {
        LookAtPlayer(gameObject);
        transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 0, 1f)
        .OnComplete(() => {
            transform.localScale = originalScale;
        });
        transform.DOMoveY(originalPosition.y + distance, duration).SetEase(ease).From(originalPosition.y);
        GetComponent<CanvasGroup>().DOFade(0f,duration*1.5f).From(1f);
    }

    private void LookAtPlayer(GameObject panel) {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        panel.transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
