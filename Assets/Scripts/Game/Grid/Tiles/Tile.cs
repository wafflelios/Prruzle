using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Image tileRenderer;
    public GridManager gridManager;

    public int X;
    public int Y;
}
