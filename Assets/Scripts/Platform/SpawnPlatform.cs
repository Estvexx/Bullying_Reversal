using UnityEngine;
using System.Collections.Generic;

public class SpawnPlatform : MonoBehaviour
{
    public static Transform obstaculoPrincipal;
    public static GameObject obstaculo_para_Ativar;
    public List<GameObject> platformPrefabs = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();

    public List<GameObject> obstacles = new List<GameObject>();

    public List<Transform> currentObstacles = new List<Transform>();


    public int offset;

    void Start()
    {
        for (int i = 0; i < platformPrefabs.Count; i++)
        {
            GameObject prefab = platformPrefabs[i];
            GameObject obstaclePrefab = obstacles[i];

            Transform platform = Instantiate(prefab, new Vector3(0, 0, i * 100), prefab.transform.rotation).transform;
            currentPlatforms.Add(platform);

            if (i == 0)
            {
                Transform InitialObstacle = Instantiate(obstacles[0], new Vector3(-2.45f, 0, i * 100 - 50), Quaternion.identity).transform;
                InitialObstacle.transform.SetParent(platform.transform);
                obstaculoPrincipal = InitialObstacle;
                currentObstacles.Add(InitialObstacle);

                Transform obstacle = Instantiate(obstacles[1], new Vector3(-2.45f, 0, i * 100 - 50), Quaternion.identity).transform;
                obstacle.transform.SetParent(platform.transform);
                obstaculo_para_Ativar = obstacle.gameObject;
                obstacle.gameObject.SetActive(false);
                currentObstacles.Add(obstacle);
            }
            else
            {
                Transform obstacle = Instantiate(obstacles[i + 1], new Vector3(-2.45f, 0, i * 100 - 50), Quaternion.identity).transform;
                obstacle.transform.SetParent(platform.transform);
                currentObstacles.Add(obstacle);
            }
            offset += 100;
        }
    }

    public void Recycle(GameObject platform)
    {
        platform.transform.position = new Vector3(0, 0, offset);
        offset += 100;
    }
}