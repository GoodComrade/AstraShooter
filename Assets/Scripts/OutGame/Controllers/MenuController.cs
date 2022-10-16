using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RootController;

/// <summary>
/// Controller responsible for menu phase.
/// </summary>
public class MenuController : SubController<UIMenuRoot>
{
    public List<Button> levelButtons = new List<Button>();
    Array values = Enum.GetValues(typeof(WinCondition));

    public override void EngageController()
    {
        Debug.Log(root.gameData.lastOpenedLevel);
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i < root.gameData.lastOpenedLevel)
                root.ChangeLevelType(LevelTypeEnum.Completed, levelButtons[i]);
            else if (i == root.gameData.lastOpenedLevel)
                root.ChangeLevelType(LevelTypeEnum.Open, levelButtons[i]);
            else if(i > root.gameData.lastOpenedLevel)
                root.ChangeLevelType(LevelTypeEnum.Closed, levelButtons[i]);
        }

        // Attaching UI events.
        ui.MenuView.OnPlayClicked += StartGame;
        ui.MenuView.OnQuitClicked += QuitGame;

        base.EngageController();
    }

    public override void DisengageController()
    {
        base.DisengageController();

        // Detaching UI events.
        ui.MenuView.OnQuitClicked -= QuitGame;
        ui.MenuView.OnPlayClicked -= StartGame;
    }

    /// <summary>
    /// Handling UI Start Button Click.
    /// </summary>
    private void StartGame()
    {
        
        if (ui.MenuView.currentLevelIndex == root.gameData.levelIndex)
        {
            root.ChangeController(ControllerTypeEnum.Game);
        }
        else
        {
            DataStorage.Instance.RemoveData(Keys.GAME_DATA_KEY);
            root.gameData = SetLevelData(root.gameData);
            root.ChangeController(ControllerTypeEnum.Game);
        }
        Debug.Log(root.gameData.levelIndex);
    }

    /// <summary>
    /// Handling UI Quit Button Click.
    /// </summary>
    private void QuitGame()
    {
        // Save game data and
        // Closing the game.
        root.SaveData(root.gameData);
        Application.Quit();
    }

    public GameData SetLevelData(GameData _data)
    {
        _data.levelIndex = ui.MenuView.currentLevelIndex;
        _data.asteroidsPopulation = UnityEngine.Random.Range(1, 5);
        _data.winCondition = (int)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        _data.scoreToWin = UnityEngine.Random.Range(500, 1500);
        _data.asteroidsToWin = UnityEngine.Random.Range(15, 30);
        DataStorage.Instance.SaveData(Keys.GAME_DATA_KEY, _data);

        return _data;
    }
}
