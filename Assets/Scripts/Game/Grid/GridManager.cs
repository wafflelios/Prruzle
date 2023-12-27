using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile UntouchableTilePrefab;
    [SerializeField] private Tile TouchableTilePrefab;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform parent;
    public GameObject Background;

    public Dictionary<Vector2, TileState> grids;

    public bool LevelWon;

    public int Counter;

    public static GridManager Instance;

    public enum TileState
    { 
        NotTaken,
        Taken,
        CantBeTaken
    }

    private void Awake()
    {
        Instance = this;
        Counter = 0;
        grids = new Dictionary<Vector2, TileState>();
        //Debug.Log(grids.Count);
    }

    public void GenerateGrid()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Tile spawnedTile;
                    if (x == 2 && y > 0 && y < 4)
                    {
                        grids[new Vector2(x, y)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector2(x, y)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            for (float x = 0; x < width; x++)
                for (float y = 0; y < height; y++)
                {
                    Tile spawnedTile;
                    if (y == 2 && x > 0 && x < 4)
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            for (float x = 0; x < width; x++)
                for (float y = 0; y < height; y++)
                {
                    Tile spawnedTile;
                    if (x > 0 && x < 4 && y > 0 && y < 4)
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            for (float x = 0; x < 6; x++)
                for (float y = 0; y < 6; y++)
                {
                    Tile spawnedTile;
                    if (x > 0 && x < 5 && y > 0 && y < 5)
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            for (float x = 0; x < 7; x++)
                for (float y = 0; y < 7; y++)
                {
                    Tile spawnedTile;
                    if ((x > 2 && x < 6 && y > 0 && y < 6) || ( x == 2 && y > 2 && y < 5))
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2 + 0.5f, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            for (float x = 0; x < 6; x++)
                for (float y = 0; y < 6; y++)
                {
                    Tile spawnedTile;
                    if ((x > 1 && x < 5 && y > 0 && y < 5) || ( x == 1 && y > 0 && y < 4))
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            for (float x = 0; x < 6; x++)
                for (float y = 0; y < 6; y++)
                {
                    Tile spawnedTile;
                    if ((x > 2 && x < 5 && y > 0 && y < 5) || (x > 0 && x < 3 && y > 0 && y < 3))
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            for (float x = 0; x < 7; x++)
                for (float y = 0; y < 7; y++)
                {
                    Tile spawnedTile;
                    if (x > 0 && x < 6 && y > 0 && y < 6)
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2 + 0.5f, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            for (float x = 0; x < 7; x++)
                for (float y = 0; y < 7; y++)
                {
                    Tile spawnedTile;
                    if ((x > 0 && x < 4 && y > 0 && y < 6) || ( x == 4 && (y == 1 || (y > 2 && y < 6))) || (x == 5 && y > 0 && y < 6))
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2 + 0.5f, (float)height / 2 - 0.5f, -10);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            for (float x = 0; x < 6; x++)
                for (float y = 0; y < 6; y++)
                {
                    Tile spawnedTile;
                    if (x > 0 && x < 5 && y > 0 && y < 5 && !(x == 2 && y == 3) && !(x == 4 && y > 2 && y < 5))
                    {
                        grids[new Vector3(x, y, 0)] = TileState.NotTaken;
                        spawnedTile = Instantiate(TouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    else
                    {
                        grids[new Vector3(x, y, 0)] = TileState.CantBeTaken;
                        spawnedTile = Instantiate(UntouchableTilePrefab, new Vector3(x, y), Quaternion.identity);
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.SetParent(parent);
                }
            cam.transform.position = new Vector3((float)width / 2, (float)height / 2 - 0.5f, -10);
        }
        //cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        Background.transform.SetAsFirstSibling();
        GameManager.Instance.ChangeState(GameManager.GameState.SpawnCats);
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position, float offSetX = 0, float offSetY = 0, float sizeX = 1f, float sizeY = 1f, float sizeZ = 1f)
    {
        var transPosition = transform.position;
        transPosition.x += offSetX;
        transPosition.y += offSetY;
        position -= transPosition;
        var pointX = Mathf.Round(position.x / sizeX);
        var pointY = Mathf.Round(position.y / sizeY);
        Vector3 result = new (pointX * sizeX, pointY * sizeY, 0);
        result += transPosition;
        result.z = 0;
        return result;
    }
}