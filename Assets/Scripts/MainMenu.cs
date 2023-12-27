using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(11);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void BackToLevelSelector()
    {
        SceneManager.LoadSceneAsync(11);
    }

    public void LoadNextScene()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(index + 1);
        GameManager.Instance.GameWon = false;
        GameManager.Instance.ChangeState(GameManager.GameState.GenerateGrid);
    }

    public void LoadLevel()
    {
        var sceneName = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeleteProgress()
    {
        YandexGame.savesData.complitedLevels = 1;
        YandexGame.NewLeaderboardScores("TOPPlayerScore", 0);
        YandexGame.SaveProgress();
    }

    public void EnableAchievementsPlane(GameObject gameName, GameObject[] achievements)
    {
        gameName.SetActive(false);
        foreach (var achievement in achievements)
            achievement.SetActive(true);
    }
}
