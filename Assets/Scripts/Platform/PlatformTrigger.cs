using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private SpawnPlatform spawn;
    [SerializeField] private float tempoAntesDeReciclar = 1f;

    public void DefinirSpawn(SpawnPlatform novoSpawn)
    {
        spawn = novoSpawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SpawnPlatform.obstaculoPrincipal != null)
            {
                Destroy(SpawnPlatform.obstaculoPrincipal.gameObject);
                SpawnPlatform.obstaculo_para_Ativar.SetActive(true);
            }

            StartCoroutine(RecycleDelay(transform.parent.gameObject));
        }
    }

    private System.Collections.IEnumerator RecycleDelay(GameObject platform)
    {
        yield return new WaitForSeconds(tempoAntesDeReciclar);
        spawn.Recycle(platform);
    }
}
