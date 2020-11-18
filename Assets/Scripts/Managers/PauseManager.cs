using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;

    public static bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }
}