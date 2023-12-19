using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public PlayerStats playerStats;
    public PowerUpObject powerUp;

    public Vector3 startPositionPlayer;

    public int playerLife = 3;
    public int playerPoints = 0;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
        playerMovement.SetStartPositionPlayer(startPositionPlayer);
    }

    private void Update()
    {
        if (playerMovement != null)
        {
            if (
              (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
              && playerMovement.GetCurrentLane() != Lane.Left
              )
            {
                Lane aux_lane = playerMovement.GetCurrentLane();
                playerMovement.ChangeLane(aux_lane - 1);
            }
            if(
                (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                && playerMovement.GetCurrentLane() != Lane.Right
            ) {
                Lane aux_lane = playerMovement.GetCurrentLane();
                playerMovement.ChangeLane(aux_lane + 1);
            }
            if (
                Input.GetKeyUp(KeyCode.Space)
                && playerMovement.GetCurrentYPosition() != playerStats.GetForceJump()
            )
            {
                playerMovement.Jump(playerStats.GetForceJump());
            }
            this.GetComponent<Transform>().position = playerMovement.GetCurrentPosition(
                                                                    playerStats.GetVelocity()
                                                                   );
        }
    }


    /// <summary>
    /// Usado para fazer interacoes com o personagem e os objetos de cena.
    /// </summary>
    /// <param name="other">Objetos colididos em cena</param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PowerUp":
                powerUp = other.GetComponent<PowerUpObject>();
                powerUp.Active(this);
                break;
            case "ObjectCollisior":
                this.playerLife -= 1;
                break;
            case "Points":
                this.playerPoints += 1;
                break;
            default: break;
        }
    }
}
