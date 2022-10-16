using System;
using UnityEngine;

// Game data model.
[Serializable]
public struct GameData
{
    // Index of selected level
    public int levelIndex;

    // Generated population of asteroids
    public int asteroidsPopulation;

    // Generated win condition
    public int winCondition;

    //if win condition is score amount
    public int scoreToWin;

    //If win condition is destroyed asteroids amount
    public int asteroidsToWin;

    public int lastOpenedLevel;
}
