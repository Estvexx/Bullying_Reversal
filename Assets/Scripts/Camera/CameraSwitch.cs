using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {
    public Camera[] cameras;

    [SerializeField] private float shakeDuration = 0.18f;
    [SerializeField] private float shakeStrength = 0.15f;
    private int currentIndex;
    private Coroutine shakeCoroutine;

    private void OnEnable() {
        PlayerHealth.AnyCornerHit += ShakeCamera;
    }

    private void OnDisable() {
        PlayerHealth.AnyCornerHit -= ShakeCamera;
    }

    private void Start() {
        SetActiveCamera(0);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C))
            SetActiveCamera((currentIndex + 1) % cameras.Length);
    }

    private void SetActiveCamera(int index) {
        for (int i = 0; i < cameras.Length; i++)
            cameras[i].gameObject.SetActive(i == index);

        currentIndex = index;
    }

    private void ShakeCamera() {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(Shake(cameras[currentIndex].transform));
    }

    private IEnumerator Shake(Transform cam) {
        Vector3 startPos = cam.localPosition;
        float time = 0f;

        while (time < shakeDuration) {
            cam.localPosition = startPos + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f) * shakeStrength;
            time += Time.deltaTime;
            yield return null;
        }

        cam.localPosition = startPos;
        shakeCoroutine = null;
    }
}