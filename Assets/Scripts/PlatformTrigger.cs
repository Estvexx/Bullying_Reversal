using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private SpawnPlatform Spawn;

    void Start()
    {
        Spawn = FindFirstObjectByType<SpawnPlatform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RecycleDelay(transform.parent.gameObject));
        }
    }

    private System.Collections.IEnumerator RecycleDelay(GameObject platform)
    {
        yield return new WaitForSeconds(1f); 
        Spawn.Recycle(platform);
    }
}