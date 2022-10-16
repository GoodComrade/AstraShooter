using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Root controller responsible for changing game phases with SubControllers.
/// </summary>
public class RootController : MonoBehaviour
{
    // SubControllers types.
    public enum ControllerTypeEnum
    {
        Menu,
        Game,
        GameOver
    }

    public enum LevelTypeEnum
    {
        Open,
        Closed,
        Completed
    }

    public enum WinCondition
    {
        ScoreWin,
        CountWin
    }

    public int lastOpenedLevel = 0;
    public bool IsLastGameFinished;

    // References to the subcontrollers.
    [Header("Controllers")]
    [SerializeField]
    private MenuController menuController;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameOverController gameOverController;

    // Unity method called on first frame.
    private void Start()
    {
        menuController.root = this;
        gameController.root = this;
        gameOverController.root = this;

        ChangeController(ControllerTypeEnum.Menu);
    }
    /// <summary>
    /// Method used by subcontrollers to change game phase.
    /// </summary>
    /// <param name="controller">Controller type.</param>
    public void ChangeController(ControllerTypeEnum controller)
    {
        // Reseting subcontrollers.
        DisengageControllers();

        // Enabling subcontroller based on type.
        switch (controller)
        {
            case ControllerTypeEnum.Menu:
                menuController.EngageController();
                break;
            case ControllerTypeEnum.Game:
                gameController.EngageController();
                break;
            case ControllerTypeEnum.GameOver:
                gameOverController.EngageController();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Method used by subcontrollers to change game phase.
    /// </summary>
    /// <param name="controller">Controller type.</param>
    public void ChangeLevelType(LevelTypeEnum controller, Button button)
    {
        switch (controller)
        {
            case LevelTypeEnum.Open:
                button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LevelTypeEnum.Open.ToString();
                button.interactable = true;
                break;
            case LevelTypeEnum.Closed:
                button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LevelTypeEnum.Closed.ToString();
                button.interactable = false;
                break;
            case LevelTypeEnum.Completed:
                button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LevelTypeEnum.Completed.ToString();
                button.interactable = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Method used to disable all attached subcontrollers.
    /// </summary>
    public void DisengageControllers()
    {
        menuController.DisengageController();
        gameController.DisengageController();
        gameOverController.DisengageController();
    }
}
