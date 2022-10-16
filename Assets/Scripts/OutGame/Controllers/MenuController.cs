using Newtonsoft.Json.Linq;
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
    GameData data;
    public List<Button> levelButtons = new List<Button>();
    Array values = Enum.GetValues(typeof(WinCondition));

    private void Start()
    {
        Array values = Enum.GetValues(typeof(WinCondition));
        data = new GameData();
        SetLevelData(data);
    }
    public override void EngageController()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i < root.lastOpenedLevel)
                root.ChangeLevelType(LevelTypeEnum.Completed, levelButtons[i]);
            else if (i == root.lastOpenedLevel)
                root.ChangeLevelType(LevelTypeEnum.Open, levelButtons[i]);
            else if(i > root.lastOpenedLevel)
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

        if (ui.MenuView.currentLevelIndex == data.levelIndex)
        {
            // Changing controller to Game Controller.
            root.ChangeController(ControllerTypeEnum.Game);
        }
        else
        {
            DataStorage.Instance.RemoveData(Keys.GAME_DATA_KEY);
            SetLevelData(data);
            root.ChangeController(ControllerTypeEnum.Game);
        }
    }

    /// <summary>
    /// Handling UI Quit Button Click.
    /// </summary>
    private void QuitGame()
    {
        // Closing the game.
        Application.Quit();
    }

    private void SetLevelData(GameData data)
    {
        data.levelIndex = ui.MenuView.currentLevelIndex;
        data.asteroidsPopulation = UnityEngine.Random.Range(1, 5);
        data.winCondition = (int)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        data.scoreToWin = UnityEngine.Random.Range(500, 1500);
        data.asteroidsToWin = UnityEngine.Random.Range(15, 30);

        DataStorage.Instance.SaveData(Keys.GAME_DATA_KEY, data);
    }
}
