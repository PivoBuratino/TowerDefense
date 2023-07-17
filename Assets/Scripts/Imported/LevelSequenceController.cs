﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSequenceController : SingletonBase<LevelSequenceController>
{
    public const string MainMenuSceneNickname = "MainMenu";
    public const string LevelMapSceneNickname = "LevelMap";

    /// <summary>
    /// Текущий эпизод. Выставляется контроллером выбора эпизода перед началом игры.
    /// </summary>
    public Episode CurrentEpisode { get; private set; }

    /// <summary>
    /// Текущий уровень эпизода. Идшник относительно текущего выставленного эпизода.
    /// </summary>
    public int CurrentLevel { get; private set; }

    /// <summary>
    /// Метод запуска первого уровня эпизода.
    /// </summary>
    /// <param name="e"></param>
    public void StartEpisode(Episode e)
    {
        CurrentEpisode = e;
        CurrentLevel = 0;

        // сбрасываем статы перед началом эпизода.
        LevelResultController.ResetPlayerStats();

        // запускаем первый уровень эпизода.
        SceneManager.LoadScene(e.Levels[CurrentLevel]);
    }

    /// <summary>
    /// Принудительный рестарт уровня.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Завершаем уровень. В зависимости от результата будет показано окошко результатов.
    /// </summary>
    /// <param name="success">успешность или поражение</param>
    public void FinishCurrentLevel(bool success)
    {
        // после организации переходов
        LevelResultController.Instance.Show(success);
    }

    /// <summary>
    /// Запускаем следующий уровень или выходим в главное меню если больше уровней нету.
    /// </summary>
    public void AdvanceLevel()
    {
        CurrentLevel++;

        // конец эпизода вываливаемся в главное меню.
        if (CurrentEpisode.Levels.Length <= CurrentLevel)
        {
            SceneManager.LoadScene(LevelMapSceneNickname);
        }
        else
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }
    }

    #region Ship select

    /// <summary>
    /// Выбранный игроком корабль для прохождения.
    /// </summary>
    public static SpaceShip PlayerShipPrefab { get; set; }

    #endregion
}
