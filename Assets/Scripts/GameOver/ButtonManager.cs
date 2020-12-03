using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
   

    public void menuButton()
    {
        SceneManager.LoadScene(0);

    }

    public void retryButton()
    {
        SceneManager.LoadScene(1);

    }
}
