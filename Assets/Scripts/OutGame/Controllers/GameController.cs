using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Контроллер, ответственный за игровую фазу.
/// </summary>
public class GameController : SubController<UIGameRoot>
{
    private PlayerData playerData;
    
    [SerializeField]
    private AsteroidSpawner spawner;
    [SerializeField]
    private PlayerController player;

    [HideInInspector]
    public int playerHP;
    [HideInInspector]
    public int playerScore;
    [HideInInspector]
    public int scoreWinCondition;

    private int levelWinCondition;
    
    private void Update()
    {
        ui.GameView.UpdateHP(this);
        ui.GameView.UpdateScore(this);

        if(playerScore >= scoreWinCondition)
        {
            root.IsLastGameFinished = true;
            FinishGame();
        }
            
    }

    public void PlayerTakeDamage(PlayerController controller)
    {
        playerHP--;
        if(playerHP <= 0)
        {
            PlayerDie(controller);
        }
    }

    void PlayerDie(PlayerController controller)
    {
        root.IsLastGameFinished = false;
        FinishGame();
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        switch (levelWinCondition)
        {
            case 0:
                if (asteroid.size < 0.7f)
                {
                    SetScore(playerScore + 100); // Награда за малый астероид
                }
                else if (asteroid.size < 1.4f)
                {
                    SetScore(playerScore + 50); // Награда за средний астероид
                }
                else
                {
                    SetScore(playerScore + 25); // Награда за большой астероид
                }
                break;
            case 1:
                SetScore(playerScore + 1);
                break;
            default:
                break;
        }
        
    }
    
    public override void EngageController()
    {
        playerData = new PlayerData();
        SetScore(0);
        SetHP(3);
        SetGameData(root.gameData);
        switch (levelWinCondition)
        {
            case 0:
                scoreWinCondition = root.gameData.scoreToWin;
                break;
            case 1:
                scoreWinCondition = root.gameData.asteroidsToWin;
                break;
            default:
                break;
        }
        spawner.EnableSpawning();

        ui.GameView.OnMenuClicked += GoToMenu;

        base.EngageController();
    }

    public override void DisengageController()
    {
        base.DisengageController();
        player.CancleFire();
        ui.GameView.OnMenuClicked -= GoToMenu;
    }


    /// <summary>
    /// Метод, вызываемый вслучае смерти игрока или когда условия для победы выполнены.
    /// </summary>
    private void FinishGame()
    {
        spawner.DisableSpawning();
        for (int i = 0; i < spawner.asteroids.Count; i++)
        {
            Destroy(spawner.asteroids[i].gameObject);
        }
        spawner.asteroids.Clear();
        playerData.playerScore = playerScore;
        
        DataStorage.Instance.SaveData(Keys.PLAYER_DATA_KEY, playerData);

        root.ChangeController(RootController.ControllerTypeEnum.GameOver);
    }

    /// <summary>
    /// Метод, вызываемый вслучае выхода на карту.
    /// </summary>
    private void GoToMenu()
    {
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
    }
    private void SetScore(int score)
    {
        playerScore = score;
    }

    private void SetHP(int playerlives)
    {
        playerHP = playerlives;
    }

    private void SetGameData(GameData data)
    {
        spawner.amountPerSpawn = data.asteroidsPopulation;
        levelWinCondition = data.winCondition;
    }
}
