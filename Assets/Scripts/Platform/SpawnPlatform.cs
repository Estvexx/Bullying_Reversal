using UnityEngine;
using System.Collections.Generic;

public class SpawnPlatform : MonoBehaviour
{
    public List<GameObject> platformPrefabs = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();

    [SerializeField] private float distanciaEntrePlataformas = 100f;

    public float offset;

    private void Start()
    {
        currentPlatforms.Clear();
        offset = 0f;

        for (int i = 0; i < platformPrefabs.Count; i++)
        {
            GameObject prefab = platformPrefabs[i];
            Vector3 posicao = new Vector3(0, 0, i * distanciaEntrePlataformas);
            Transform platform = Instantiate(prefab, posicao, prefab.transform.rotation).transform;

            currentPlatforms.Add(platform);
            ConfigurarTriggers(platform);
            offset += distanciaEntrePlataformas;
        }
    }

    public void Recycle(GameObject platform)
    {
        platform.transform.position = new Vector3(0, 0, offset);
        offset += distanciaEntrePlataformas;
    }

    public void AjustarOrigem(float deslocamentoZ)
    {
        offset += deslocamentoZ;

        foreach (Transform platform in currentPlatforms)
        {
            if (platform == null) continue;

            Vector3 pos = platform.position;
            pos.z += deslocamentoZ;
            platform.position = pos;
        }
    }

    private void ConfigurarTriggers(Transform platform)
    {
        PlatformTrigger[] triggers = platform.GetComponentsInChildren<PlatformTrigger>(true);

        foreach (PlatformTrigger trigger in triggers)
        {
            trigger.DefinirSpawn(this, platform);
        }
    }
}
