using UnityEngine;
using System.Collections.Generic;

public class SpawnPlatform : MonoBehaviour
{
    public List<GameObject> platformPrefabs = new List<GameObject>(); 

    public int offset;

    void Start()
    {
       for(int i = 0; i < platformPrefabs.Count; i++)
       {
            GameObject prefab = platformPrefabs[i];
            Instantiate(prefab, new Vector3(0, 0, i * 42), prefab.transform.rotation);
            offset += 42;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recycle(GameObject platform)
    {
        platform.transform.position = new Vector3(0, 0, offset);
        offset += 42;
    }
}
