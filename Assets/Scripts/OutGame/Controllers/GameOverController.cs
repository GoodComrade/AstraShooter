using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// Контроллер, ответственный за фазу конца игрового уровня.
/// </summary>
public class GameOverController : SubController<UIGameOverRoot>
{
    private PlayerData playerData;
    private GameData gameData;

    public override void EngageController()
    {
        ui.GameOverView.OnReplayClicked += ReplayGame;
        ui.GameOverView.OnMenuClicked += GoToMenu;

        playerData = DataStorage.Instance.GetData<PlayerData>(Keys.PLAYER_DATA_KEY);

        DataStorage.Instance.RemoveData(Keys.PLAYER_DATA_KEY);

        ui.GameOverView.ShowScore(playerData);
        IncreastLastOpened();

        base.EngageController();
    }

    public override void DisengageController()
    {
        base.DisengageController();

        ui.GameOverView.OnMenuClicked -= GoToMenu;
        ui.GameOverView.OnReplayClicked -= ReplayGame;
    }

    /// <summary>
    /// Содержит логику по нажатии на кнопку реплея.
    /// </summary>
    private void ReplayGame()
    {
        root.ChangeController(RootController.ControllerTypeEnum.Game);
    }

    /// <summary>
    /// Содержит логику по нажатии на кнопку возврата к карте.
    /// </summary>
    private void GoToMenu()
    {
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
    }

    private void IncreastLastOpened()
    {
        if (root.IsLastGameFinished && gameData.levelIndex == gameData.lastOpenedLevel)
        {
            gameData = DataStorage.Instance.GetData<GameData>(Keys.GAME_DATA_KEY);
            DataStorage.Instance.RemoveData(Keys.GAME_DATA_KEY);

            gameData.lastOpenedLevel++;
            root.gameData = gameData;
            DataStorage.Instance.SaveData(Keys.GAME_DATA_KEY, root.gameData);
        }
    }
}
