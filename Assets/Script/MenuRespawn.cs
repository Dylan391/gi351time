using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRespawn : MonoBehaviour
{
    public PlayerManager manager;
    
    public void OnRespawnButton()
    {
        if (manager.lives > 0)
        {
            manager.Respawn();
        }
    }
    
    public void OnMenuButton()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
