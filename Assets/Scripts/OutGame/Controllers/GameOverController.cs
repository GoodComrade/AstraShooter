using UnityEngine;

/// <summary>
/// Controller responsible for game over phase.
/// </summary>
public class GameOverController : SubController<UIGameOverRoot>
{
    // Reference to current game data.
    private PlayerData playerData;

    public override void EngageController()
    {
        // Getting game data from data storage.
        playerData = DataStorage.Instance.GetData<PlayerData>(Keys.PLAYER_DATA_KEY);
        // Removing game data from data storage as it is no longer needed there.
        DataStorage.Instance.RemoveData(Keys.PLAYER_DATA_KEY);

        // Showing game data in UI.
        ui.GameOverView.ShowScore(playerData);

        // Attaching UI events.
        ui.GameOverView.OnReplayClicked += ReplayGame;
        ui.GameOverView.OnMenuClicked += GoToMenu;

        base.EngageController();
    }

    public override void DisengageController()
    {
        base.DisengageController();

        // Detaching UI events.
        ui.GameOverView.OnMenuClicked -= GoToMenu;
        ui.GameOverView.OnReplayClicked -= ReplayGame;
    }

    /// <summary>
    /// Handling UI Replay Button Click.
    /// </summary>
    private void ReplayGame()
    {
        // Changing controller to Game Controller.
        root.ChangeController(RootController.ControllerTypeEnum.Game);
    }

    /// <summary>
    /// Handling UI Menu Button Click.
    /// </summary>
    private void GoToMenu()
    {
        // Removing game data from data storage as it is no longer needed there.
        //DataStorage.Instance.RemoveData(Keys.GAME_DATA_KEY);

        // Changing controller to Menu Controller.
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
    }
}
