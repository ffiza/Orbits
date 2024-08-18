using UnityEngine;

public class Manager : MonoBehaviour
{
    private void Awake()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }
}
