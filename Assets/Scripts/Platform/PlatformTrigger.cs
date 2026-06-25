using UnityEngine;

public class PlatformTrigger : MonoBehaviour {
    [SerializeField] private SpawnPlatform spawn;
    [SerializeField] private Transform platformRoot;
    [SerializeField] private float tempoAntesDeReciclar = 1f;

    private bool aReciclar;

    public void DefinirSpawn(SpawnPlatform novoSpawn, Transform novaPlatformRoot) {
        spawn = novoSpawn;
        platformRoot = novaPlatformRoot;
    }

    private void OnTriggerEnter(Collider other) {
        if (aReciclar) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        aReciclar = true;
        StartCoroutine(RecycleDelay());
    }

    private System.Collections.IEnumerator RecycleDelay() {
        yield return new WaitForSeconds(tempoAntesDeReciclar);

        if (spawn != null && platformRoot != null)
            spawn.Recycle(platformRoot.gameObject);

        aReciclar = false;
    }
}
