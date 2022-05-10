using UnityEngine;

public class Resolution : MonoBehaviour
{
    void Start()
    {
        // Switch to 1920 x 1080 full-screen
        Screen.SetResolution(1920, 1080, true);
    }
}