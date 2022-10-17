using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Корневой контроллер, ответственный за смену игровых подконтроллеров.
/// </summary>
public class RootController : MonoBehaviour
{
    // Типы подконтроллеров.
    public enum ControllerTypeEnum
    {
        Menu,
        Game,
        GameOver
    }

    // Типы уровней.
    public enum LevelTypeEnum
    {
        Open,
        Closed,
        Completed
    }

    // Типы уcловий победы.
    public enum WinCondition
    {
        ScoreWin,
        CountWin
    }

    [HideInInspector]
    public bool IsLastGameFinished;

    [HideInInspector]
    public GameData gameData;

    // Ссылки на подконтроллеры.
    [Header("Controllers")]
    [SerializeField]
    private MenuController menuController;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameOverController gameOverController;

    private void Start()
    {
        menuController.root = this;
        gameController.root = this;
        gameOverController.root = this;

        if (!File.Exists("./Saves/SavedGame.bin"))
        {
            gameData = menuController.SetLevelData(gameData);
            Debug.Log("levelIndex:" + gameData.levelIndex +
            ", scoreToWin:" + gameData.scoreToWin +
            ", asteroidsPopulation:" + gameData.asteroidsPopulation +
            ", asteroidsToWin:" + gameData.asteroidsToWin +
            ", winCondition:" + gameData.winCondition);
        }
        else
        {
            gameData = LoadData(gameData);
        }
            
        ChangeController(ControllerTypeEnum.Menu);
    }

    /// <summary>
    /// Метод, используемый подконтроллером для смены игровой фазы.
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
    /// Метод, используемый для отключения всех контроллеров на сцене.
    /// </summary>
    public void DisengageControllers()
    {
        menuController.DisengageController();
        gameController.DisengageController();
        gameOverController.DisengageController();
    }

    public void SaveData(GameData _gameData)
    {
        if(!Directory.Exists(Keys.SAVE_DIRECTORY_KEY))
            Directory.CreateDirectory(Keys.SAVE_DIRECTORY_KEY);

        BinaryFormatter formatter = new BinaryFormatter();
        _gameData = DataStorage.Instance.GetData<GameData>(Keys.GAME_DATA_KEY);

        FileStream saveFile = File.Create(Keys.SAVE_DIRECTORY_KEY + "/" + Keys.SAVE_NAME_KEY + ".bin");
        formatter.Serialize(saveFile, _gameData);
        saveFile.Close();
    }

    public GameData LoadData(GameData _gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(Keys.SAVE_DIRECTORY_KEY + "/" + Keys.SAVE_NAME_KEY + ".bin", FileMode.Open);
        _gameData = (GameData)formatter.Deserialize(saveFile);
        saveFile.Close();

        DataStorage.Instance.SaveData(Keys.GAME_DATA_KEY, _gameData);

        return _gameData;
    }
}
