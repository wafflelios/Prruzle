using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public Button button;

    [SerializeField] public GameObject cat1X1;
    [SerializeField] public GameObject cat1X3;
    [SerializeField] public GameObject cat2X2;
    [SerializeField] public GameObject cat2X3;
    [SerializeField] public GameObject cat3X3;
    [SerializeField] public GameObject strangeCatWhite;
    [SerializeField] public GameObject strangeCatBlack;

    [SerializeField] private Transform parent;
    public static GameManager Instance;
    public GameState State;
    public bool GameWon;

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            var firstTile = GridManager.Instance.grids[new Vector2(2, 1)] == GridManager.TileState.Taken;
            var secondTile = GridManager.Instance.grids[new Vector2(2, 2)] == GridManager.TileState.Taken;
            var thirdTile = GridManager.Instance.grids[new Vector2(2, 3)] == GridManager.TileState.Taken;
            if (firstTile && secondTile && thirdTile)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            var firstTile = GridManager.Instance.grids[new Vector2(1, 2)] == GridManager.TileState.Taken;
            var secondTile = GridManager.Instance.grids[new Vector2(2, 2)] == GridManager.TileState.Taken;
            var thirdTile = GridManager.Instance.grids[new Vector2(3, 2)] == GridManager.TileState.Taken;
            if (firstTile && secondTile && thirdTile)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            var tiles = new bool[9];
            var index = 0;
            for (var x = 1; x < 4; x++)
                for (var y = 1; y < 4; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            if (tiles.Count(x => x == true) == 9)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            var tiles = new bool[16];
            var index = 0;
            for (var x = 1; x < 5; x++)
                for (var y = 1; y < 5; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            if (tiles.Count(x => x == true) == 16)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            var tiles = new bool[17];
            var index = 0;
            for (var x = 3; x < 6; x++)
                for (var y = 1; y < 6; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            tiles[index] = GridManager.Instance.grids[new Vector2(2, 3)] == GridManager.TileState.Taken;
            tiles[index + 1] = GridManager.Instance.grids[new Vector2(2, 4)] == GridManager.TileState.Taken;
            if (tiles.Count(x => x == true) == 17)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            var tiles = new bool[15];
            var index = 0;
            for (var x = 1; x < 5; x++)
                for (var y = 1; y < 5; y++)
                {
                    if (x != 1 || y != 4)
                    {
                        tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                        index += 1;
                    }
                }
            if (tiles.Count(x => x == true) == 15)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            var tiles = new bool[12];
            var index = 0;
            for (var x = 1; x < 3; x++)
                for (var y = 1; y < 3; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            for (var x = 3; x < 5; x++)
                for (var y = 1; y < 5; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            if (tiles.Count(x => x == true) == 12)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            var tiles = new bool[25];
            var index = 0;
            for (var x = 1; x < 6; x++)
                for (var y = 1; y < 6; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            if (tiles.Count(x => x == true) == 25)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            var tiles = new bool[24];
            var index = 0;
            for (var x = 1; x < 6; x++)
                for (var y = 1; y < 6; y++)
                {
                    if (x != 4 || y != 2)
                    {
                        tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                        index += 1;
                    }
                }
            if (tiles.Count(x => x == true) == 24)
            {
                GameWon = true;
                ChangeState(GameState.LevelComplited);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            var tiles = new bool[13];
            var index = 0;
            for (var x = 1; x < 5; x++)
                for (var y = 1; y < 3; y++)
                {
                    tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                    index += 1;
                }
            for (var x = 1; x < 4; x++)
                for (var y = 3; y < 5; y++)
                {
                    if (x != 2 || y != 3)
                    {
                        tiles[index] = GridManager.Instance.grids[new Vector2(x, y)] == GridManager.TileState.Taken;
                        index += 1;
                    }
                }
            if (tiles.Count(x => x == true) == 13)
            {
                GameWon = true;
                Debug.Log("Level 10 complited");
                ChangeState(GameState.LevelComplited);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnCats:
                SpawnCats();
                break;
            case GameState.LevelComplited:
                SpawnNextLevelButton();
                break;
        }
    }

    public void SpawnCats()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            var spawnedCat = Instantiate(cat1X3, new Vector2(9, 2), Quaternion.identity);
            spawnedCat.name = $"Cat1x3";
            spawnedCat.transform.SetParent(parent);
            spawnedCat.transform.localScale = new Vector3(1, 1, 1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            var spawnedCat = Instantiate(cat1X3, new Vector2(9, 2), Quaternion.identity);
            spawnedCat.name = $"Cat1x3";
            spawnedCat.transform.SetParent(parent);
            spawnedCat.transform.localScale = new Vector3(1, 1, 1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            var spawnedCat1x3 = Instantiate(cat1X3, new Vector2(7, 2), Quaternion.Euler(new Vector3(0, 0, -90)));
            spawnedCat1x3.name = $"Cat1x3";
            spawnedCat1x3.transform.SetParent(parent);
            spawnedCat1x3.transform.localScale = new Vector3(1, 1, 1);

            var spawnedCat2x3 = Instantiate(cat2X3, new Vector2(-3.5f, 2), Quaternion.Euler(new Vector3(0, 0, 90)));
            spawnedCat2x3.name = $"Cat2x3";
            spawnedCat2x3.transform.SetParent(parent);
            spawnedCat2x3.transform.localScale = new Vector3(1, 1, 1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            var cats = new GameObject[] {cat1X1, cat1X3, cat2X3, cat2X3 };
            var positions = new Vector2[] { new Vector2(8, 4), new Vector2(-3, 4), new Vector2(-3, 1), new Vector2(8, 1), };
            var catNames = new string[] { "cat1x1", "cat1x3", "cat2x3 one", "cat2x3 two" };
            var quaternions = new Quaternion[] { Quaternion.Euler(new Vector3(0, 0, -90)), Quaternion.Euler(new Vector3(0, 0, 180)), Quaternion.Euler(new Vector3(0, 0, 90)), Quaternion.identity };
            for (var i = 0; i < 4; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            var cats = new GameObject[] { cat1X1, cat1X1, cat1X3, cat2X3, cat2X3 };
            var positions = new Vector2[] { new Vector2(-2, 5), new Vector2(-3, 3), new Vector2(9, 0.5f), new Vector2(-3, 0.5f), new Vector2(8.5f, 4) };
            var catNames = new string[] { "cat1x1 one", "cat1x1 two", "cat1x3", "cat2x3 one", "cat2x3 two" };
            var quaternions = new Quaternion[] { Quaternion.identity, Quaternion.Euler(new Vector3(0, 0, 180)), Quaternion.Euler(new Vector3(0, 0, -90)), Quaternion.Euler(new Vector3(0, 0, 90)), Quaternion.identity };
            for (var i = 0; i < 5; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            var cats = new GameObject[] { cat1X3, cat1X3, cat3X3 };
            var positions = new Vector2[] { new Vector2(-2, 4), new Vector2(-3, 1), new Vector2(8, 2.5f) };
            var catNames = new string[] { "cat1x3 one", "cat1x3 two", "cat3x3" };
            var quaternions = new Quaternion[] { Quaternion.identity, Quaternion.Euler(new Vector3(0, 0, 90)), Quaternion.identity };
            for (var i = 0; i < 3; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            var cats = new GameObject[] { cat1X3, cat2X3, strangeCatWhite };
            var positions = new Vector2[] { new Vector2(8, 1), new Vector2(-3, 2.5f), new Vector2(7.5f, 4) };
            var catNames = new string[] { "cat1x3", "cat2x3", "strangeCatWhite" };
            var quaternions = new Quaternion[] { Quaternion.Euler(new Vector3(0, 0, -90)), Quaternion.Euler(new Vector3(0, 0, 90)), Quaternion.Euler(new Vector3(0, 0, -90)) };
            for (var i = 0; i < 3; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            var cats = new GameObject[] { cat1X1, cat1X3, cat1X3, cat2X3, strangeCatWhite, strangeCatWhite, strangeCatBlack, strangeCatBlack };
            var positions = new Vector2[] { new Vector2(-4, 6), new Vector2(-2, 4), new Vector2(10, 0), new Vector2(8.5f, 3), 
                new Vector2(-4.5f, 2.5f), new Vector2(9.5f,6), new Vector2(-2.5f, 0.5f), new Vector2(11.5f, 3) };
            var catNames = new string[] { "cat1x1", "cat1x3 one", "cat1x3 two", "cat2x3", "strangeCatWhite one", "strangeCatWhite two", "strangeCatBlack one", "strangeCatBlack two" };
            var quaternions = new Quaternion[] { Quaternion.Euler(new Vector3(0, 0, 180)), Quaternion.identity, Quaternion.Euler(new Vector3(0, 0, 90)), Quaternion.Euler(new Vector3(0, 0, 180)),
                Quaternion.Euler(new Vector3(0, 0, -90)), Quaternion.Euler(new Vector3(0, 0, 180)), Quaternion.Euler(new Vector3(0, 0, 180)), Quaternion.identity };
            for (var i = 0; i < 8; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            var cats = new GameObject[] { cat1X3, cat1X3, cat2X3, cat3X3, strangeCatBlack };
            var positions = new Vector2[] { new Vector2(-3, 2.5f), new Vector2(9, 1.5f), new Vector2(-3, 5), new Vector2(9, 5), new Vector2(-3, 0.5f) };
            var catNames = new string[] { "cat1x3 one", "cat1x3 two", "cat2x3", "cat3x3", "strangeCatBlack" };
            var quaternions = new Quaternion[] { Quaternion.Euler(new Vector3(0, 0, -90)), Quaternion.identity, Quaternion.identity, Quaternion.Euler(new Vector3(0, 0, -90)),
                Quaternion.Euler(new Vector3(0, 0, 90)) };
            for (var i = 0; i < 5; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            var cats = new GameObject[] { cat1X3, cat2X2, strangeCatWhite, strangeCatBlack };
            var positions = new Vector2[] { new Vector2(-3, 4), new Vector2(7.5f, 1), new Vector2(-2.5f, 1), new Vector2(7.5f, 4) };
            var catNames = new string[] { "cat1x3", "cat2x2", "strangeCatWhite", "strangeCatBlack" };
            var quaternions = new Quaternion[] { Quaternion.Euler(new Vector3(0, 0, 90)), Quaternion.identity, Quaternion.identity, Quaternion.Euler(new Vector3(0, 0, 180)) };
            for (var i = 0; i < 4; i++)
            {
                var spawnedCat = Instantiate(cats[i], positions[i], quaternions[i]);
                spawnedCat.name = catNames[i];
                spawnedCat.transform.SetParent(parent);
                spawnedCat.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void SpawnNextLevelButton()
    {
        YandexGame.NewLeaderboardScores("TOPPlayerScore", SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex != 10)
        {
            var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene > YandexGame.savesData.complitedLevels)
            {
                YandexGame.savesData.complitedLevels = nextScene;
            }
            button.gameObject.SetActive(true);
        }
        YandexGame.SaveProgress();
    }

    public enum GameState
    {
        GenerateGrid,
        SpawnCats, 
        LevelComplited
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
