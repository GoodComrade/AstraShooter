using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RootController;

/// <summary>
/// Котроллер, ответственный за меню.
/// </summary>
public class MenuController : SubController<UIMenuRoot>
{
    public List<Button> levelButtons = new List<Button>();
    Array values = Enum.GetValues(typeof(WinCondition));

    public override void EngageController()
    {
        // Установка отображения и активности кнопок 
        // в зависимости от текущего прогресса.
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
    /// Содержит действие нажатия на кнопку уровня.
    /// </summary>
    private void StartGame()
    {
        
        if (root.gameData.levelIndex != ui.MenuView.currentLevelIndex)
        {
            DataStorage.Instance.RemoveData(Keys.GAME_DATA_KEY);
            root.gameData = SetLevelData(root.gameData);
        }
        root.ChangeController(ControllerTypeEnum.Game);
    }

    /// <summary>
    /// Содержит действие нажатия на кнопку выхода.
    /// </summary>
    private void QuitGame()
    {
        // Сохраняем данные и
        // закрываем игру.
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
