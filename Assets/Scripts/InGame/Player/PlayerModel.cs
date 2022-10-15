using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerModel
{
    public int playerHP { get; set; }
    public int playerScore { get; set; }
    public float playerThrust { get; set; }
    public float playerRotationSpeed { get; set; }
    public float playerMaxSpeed { get; set; }

    public void SetCharacterData(float thrust, float rotationSpeed, float maxSpeed, int health, int score)
    {
        playerThrust = thrust;
        playerRotationSpeed = rotationSpeed;
        playerMaxSpeed = maxSpeed;
        playerHP = health;
        playerScore = score;
    }
}
