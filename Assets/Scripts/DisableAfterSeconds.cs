using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//:) This script is responsible for:
public class DisableAfterSeconds : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private IEnumerator fading;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fading = Fader();
        Invoke(nameof(StartTheFader), 2);
    }

    private void KillMe()
    {
        Destroy(gameObject);
    }

    private void StartTheFader()
    {
        StartCoroutine(fading);
    }

    private IEnumerator Fader()
    {
        float startAlpha = 1.0f;
        while (true)
        {
            startAlpha -= 0.05f;
            canvasGroup.alpha = startAlpha;

            if (startAlpha < 0.0f)
            {
                KillMe();
                StopCoroutine(fading);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
