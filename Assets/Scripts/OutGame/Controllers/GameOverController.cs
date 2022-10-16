using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Контроллер, ответственный за фазу конца игрового уровня.
/// </summary>
public class GameOverController : SubController<UIGameOverRoot>
{
    private PlayerData playerData;

    public override void EngageController()
    {
        playerData = DataStorage.Instance.GetData<PlayerData>(Keys.PLAYER_DATA_KEY);

        DataStorage.Instance.RemoveData(Keys.PLAYER_DATA_KEY);

        ui.GameOverView.ShowScore(playerData);

        root.IncreaseLastOpened();

        ui.GameOverView.OnReplayClicked += ReplayGame;
        ui.GameOverView.OnMenuClicked += GoToMenu;

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
}
