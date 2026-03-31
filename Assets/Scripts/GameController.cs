using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject GameOver;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScreen()
    {
        GameOver.SetActive(true);
    }
}
