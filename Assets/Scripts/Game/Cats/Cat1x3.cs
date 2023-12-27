using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GridManager;

public class Cat1x3 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Sprite image1;
    public Sprite image2;
    public Sprite takenImage;
    public Vector2 startPosition;
    public Quaternion startDirection;
    public Direction currentDirection;

    public bool isOnDrag;
    public bool isOnGrid;
    public int counter;

    public void SentToTheStart()
    {
        transform.SetPositionAndRotation(startPosition, startDirection);
        currentDirection = FindDirection(startDirection);
    }

    public void Start()
    {
        InvokeRepeating("Change", 1f, 0.5f);
    }

    void Change()
    {
        if (!isOnDrag)
        {
            if (counter % 2 == 0)
                GetComponent<Image>().sprite = image1;
            else
                GetComponent<Image>().sprite = image2;
            counter += 1;
        }
    }

    public Direction FindDirection(Quaternion quaternion)
    {
        if (startDirection == Quaternion.Euler(new Vector3(0, 0, 0))) return Direction.Top;
        else if (startDirection == Quaternion.Euler(new Vector3(0, 0, 90))) return Direction.Left;
        else if (startDirection == Quaternion.Euler(new Vector3(0, 0, 180))) return Direction.Bottom;
        else return Direction.Right;
    }

    private void Awake()
    {
        startPosition = transform.position;
        startDirection = transform.rotation;
        currentDirection = FindDirection(startDirection);
        //Debug.Log(Instance.grids[new Vector2(0, 0)]);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Untouchable") && !isOnDrag)
        //    SentToTheStart();
        if (collision.gameObject.name.Contains("Tile"))
            isOnGrid = true;

        if (collision.gameObject.name == "Background")
            isOnGrid = false;
        //Debug.Log(collision.gameObject.name);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isOnGrid)
            FreeOrOccupyCell(GridManager.TileState.NotTaken);
        isOnDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = takenImage;
        if (Input.GetMouseButton(0) && !GameManager.Instance.GameWon)
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            if (isOnGrid)
            {
                transform.position = Instance.GetNearestPointOnGrid(mouseWorldPosition);
            }
            else transform.position = mouseWorldPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOnGrid)
            FreeOrOccupyCell(TileState.Taken);
        isOnDrag = false;
    }

    public void FreeOrOccupyCell(GridManager.TileState tileState)
    {
        var positions = GetTilePosition();
        var wasSendToTheStart = false;
        //Debug.Log(grids.Count);
        if (tileState == TileState.Taken)
        {
            foreach (var position in positions)
            {
                if (!Instance.grids.ContainsKey(position) || Instance.grids[position] == TileState.Taken || Instance.grids[position] == TileState.CantBeTaken)
                {
                    SentToTheStart();
                    wasSendToTheStart = true;
                    break;
                }
            }
            if (!wasSendToTheStart) ChangeTileState(positions, tileState);
        }
        else ChangeTileState(positions, tileState);
    }

    public void ChangeTileState(Vector2[] positions, GridManager.TileState tileState)
    {
        foreach (var position in positions)
            Instance.grids[position] = tileState;
    }

    public Vector2[] GetTilePosition()
    {
        var result = new Vector2[3];
        var index = 0;

        var currentPosition = transform.position;

        float maxX;
        float minX;
        float maxY;
        float minY;

        if (currentDirection == Direction.Top || currentDirection == Direction.Bottom)
        {
            maxX = currentPosition.x;
            minX = currentPosition.x;

            maxY = currentPosition.y + 1;
            minY = currentPosition.y - 1;
        }
        else
        {
            maxX = currentPosition.x + 1;
            minX = currentPosition.x - 1;

            maxY = currentPosition.y;
            minY = currentPosition.y;
        }
        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                //Debug.Log($"{x} {y}");
                //Debug.Log($"{Mathf.Round(x)} {Mathf.Round(y)}");
                result[index] = new Vector2(Mathf.Round(x), Mathf.Round(y));
                index += 1;
            }
        }
        return result;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(CanBeRotated());
        //Debug.Log(GameManager.Instance.GameWon);
        if (eventData.button == PointerEventData.InputButton.Right && CanBeRotated() && !GameManager.Instance.GameWon)
        {
            if (isOnGrid) FreeOrOccupyCell(TileState.NotTaken);
            currentDirection = currentDirection.Next();
            transform.Rotate(0, 0, 90);
            if (isOnGrid) FreeOrOccupyCell(TileState.Taken);
        }
    }

    public bool CanBeRotated()
    {
        if (!isOnGrid) return true;
        else
        {
            var middlePOsition = GetTilePosition()[1];

            float firstX;
            float firstY;
            float secondX;
            float secondY;

            if (currentDirection == Direction.Top || currentDirection == Direction.Bottom)
            {
                firstX = middlePOsition.x - 1f;
                firstY = middlePOsition.y;

                secondX = middlePOsition.x + 1f;
                secondY = middlePOsition.y;
            }
            else
            {
                firstX = middlePOsition.x;
                firstY = middlePOsition.y + 1f;

                secondX = middlePOsition.x;
                secondY = middlePOsition.y - 1f;
            }
            var firstTile = Instance.grids[new Vector2(firstX, firstY)];
            var secondTile = Instance.grids[new Vector2(secondX, secondY)];
            if (firstTile == TileState.NotTaken && secondTile == TileState.NotTaken)
                return true;
            else return false;
        }
    }
}
