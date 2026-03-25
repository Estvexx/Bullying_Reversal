using UnityEngine;
using System.Collections.Generic;

public class SpawnPlatform : MonoBehaviour
{
    public List<GameObject> platformPrefabs = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();

    public int offset;

    void Start()
    {
        for (int i = 0; i < platformPrefabs.Count; i++)
        {
            GameObject prefab = platformPrefabs[i];
            Transform p = Instantiate(prefab, new Vector3(0, 0, i * 42), prefab.transform.rotation).transform;
            currentPlatforms.Add(p);
            offset += 42;
        }
    }

    public void Recycle(GameObject platform)
    {
        platform.transform.position = new Vector3(0, 0, offset);
        offset += 42;
    }
}