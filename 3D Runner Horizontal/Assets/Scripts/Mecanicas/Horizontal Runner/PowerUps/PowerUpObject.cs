using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    public PowerUp powerUp;
    public void Active(PlayerManager playerManager)
    {
        if (powerUp != null) { 
            powerUp.Active(playerManager);
        }
        this.gameObject.SetActive(false);
    }


}
