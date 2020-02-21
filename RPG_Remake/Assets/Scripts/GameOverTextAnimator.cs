using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTextAnimator : MonoBehaviour
{
    private void Start()
    {
        var transformCache = transform;
        var defaultPosition = transformCache.localPosition;
        transformCache.localPosition = new Vector3(0, 300f);
        transformCache.DOLocalMove(defaultPosition, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("GAME OVER!!");
                // シェイク
                transformCache.DOShakePosition(1.5f, 100);
            });
        DOVirtual.DelayedCall(10, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }
}