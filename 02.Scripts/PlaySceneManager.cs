using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameOver()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
