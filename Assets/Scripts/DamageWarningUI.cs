using UnityEngine;
using System.Collections;

public class DamageWarningUI : MonoBehaviour {
    [SerializeField] private CanvasGroup border;
    [SerializeField] private float duracao = 10f;
    [SerializeField] private float velocidadePulso = 8f;
    [SerializeField] private float alphaMin = 0.15f;
    [SerializeField] private float alphaMax = 0.65f;

    private Coroutine coroutine;

    private void OnEnable() {
        PlayerHealth.AnyCornerHit += AtivarAviso;
        PlayerHealth.AnyPlayerDied += DesativarAviso;
    }

    private void OnDisable() {
        PlayerHealth.AnyCornerHit -= AtivarAviso;
        PlayerHealth.AnyPlayerDied -= DesativarAviso;
    }

    private void Start() {
        border.alpha = 0f;
    }

    private void AtivarAviso() {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Pulsar());
    }

    private void DesativarAviso() {
        if (coroutine != null)
            StopCoroutine(coroutine);

        border.alpha = 0f;
        coroutine = null;
    }

    private IEnumerator Pulsar() {
        float tempo = 0f;

        while (tempo < duracao) {
            float pulso = (Mathf.Sin(Time.time * velocidadePulso) + 1f) / 2f;
            border.alpha = Mathf.Lerp(alphaMin, alphaMax, pulso);

            tempo += Time.deltaTime;
            yield return null;
        }

        border.alpha = 0f;
        coroutine = null;
    }
}