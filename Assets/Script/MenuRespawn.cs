using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRespawn : MonoBehaviour
{
    public PlayerManager manager;
    public GameObject respawnMenu;
    
    public void OnRespawnButton()
    {
        if (manager.lives > 0)
        {
            manager.Respawn();
        }
    }
    
    public void OnMenuButton()
    {
        respawnMenu.SetActive(false);
        SceneManager.LoadSceneAsync(2);
    }
}
