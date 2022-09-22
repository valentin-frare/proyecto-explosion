using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    
    public void SpawnAgain()
    {
        RespawnManager.instance.SpawnPlayer();
        GameManager.instance.SetGameState(GameState.Playing);
    }

}
