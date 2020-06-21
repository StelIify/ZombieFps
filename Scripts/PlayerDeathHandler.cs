using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    void Awake()
    {
        gameOverCanvas.enabled = false;
    }

   public void HandleDeath()
    {
        Time.timeScale = 0;
        gameOverCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
