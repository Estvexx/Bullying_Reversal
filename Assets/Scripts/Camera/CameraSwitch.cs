using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras;
    private int currentIndex;

    void Start()
    {
        SetActiveCamera(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int nextIndex = (currentIndex + 1) % cameras.Length;
            SetActiveCamera(nextIndex);
        }
    }

    void SetActiveCamera(int index)
    {
        cameras[currentIndex].gameObject.SetActive(false);
        cameras[index].gameObject.SetActive(true);
        currentIndex = index;
    }
}