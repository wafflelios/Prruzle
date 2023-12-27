using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GridManager;

public class Cat2x2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public float offSetX = 0.5f;
    public float offSetY = 0.5f;

    public Sprite image1;
    public Sprite image2;
    public Sprite takenImage;
    public Vector2 startPosition;
    public Quaternion startDirection;
    public Direction currentDirection;

    public bool isOnDrag;
    public bool isOnGrid;
    public int counter;

    private void Awake()
    {
        startPosition = transform.position;
        startDirection = transform.rotation;
        currentDirection = Direction.Top;
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

    public void SentToTheStart()
    {
        transform.SetPositionAndRotation(startPosition, startDirection);
        currentDirection = Direction.Top;
        offSetX = 0.5f;
        offSetY = 0.5f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Tile"))
            isOnGrid = true;

        if (collision.gameObject.name == "Background")
            isOnGrid = false;
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
            mouseWorldPosition.x += offSetX;
            mouseWorldPosition.y += offSetY;
            mouseWorldPosition.z = 0f;

            if (isOnGrid)
            {
                transform.position = Instance.GetNearestPointOnGrid(mouseWorldPosition, offSetX, offSetY);
            }
            else transform.position = mouseWorldPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOnGrid)
            FreeOrOccupyCell(GridManager.TileState.Taken);
        isOnDrag = false;
    }

    public void FreeOrOccupyCell(GridManager.TileState tileState)
    {
        var positions = GetTilePosition();
        var wasSendToTheStart = false;
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
        var result = new Vector2[4];
        var index = 0;

        var currentPosition = transform.position;
        currentPosition.x -= offSetX;
        currentPosition.y -= offSetY;

        float maxX;
        float minX;
        float maxY;
        float minY;

        if (currentDirection == Direction.Top)
        {
            maxX = currentPosition.x + 1;
            minX = currentPosition.x;

            maxY = currentPosition.y + 1;
            minY = currentPosition.y;
        }
        else if (currentDirection == Direction.Bottom)
        {
            maxX = currentPosition.x;
            minX = currentPosition.x - 1;

            maxY = currentPosition.y;
            minY = currentPosition.y - 1;
        }
        else if (currentDirection == Direction.Left)
        {
            maxX = currentPosition.x;
            minX = currentPosition.x - 1;

            maxY = currentPosition.y + 1;
            minY = currentPosition.y;
        }
        else
        {
            maxX = currentPosition.x + 1;
            minX = currentPosition.x;

            maxY = currentPosition.y;
            minY = currentPosition.y - 1;
        }
        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                //Debug.Log($"{Mathf.Round(x)} {Mathf.Round(y)}");
                result[index] = new Vector2(Mathf.Round(x), Mathf.Round(y));
                index += 1;
            }
        }
        return result;
    }

    public Vector3 ChangeOffsetAndPosition(Direction direction, Vector3 position)
    {
        if (direction == Direction.Top)
        {
            offSetX = -offSetX;
            position.x -= 1;
        }
        else if (direction == Direction.Left)
        {
            offSetY = -offSetY;
            position.y -= 1;
        }
        else if (direction == Direction.Bottom)
        {
            offSetX = -offSetX;
            position.x += 1;
        }
        else
        {
            offSetY = -offSetY;
            position.y += 1;
        }
        return position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && CanBeRotated())
        {
            if (isOnGrid) FreeOrOccupyCell(TileState.NotTaken);
            var prevPosition = ChangeOffsetAndPosition(currentDirection, transform.position);
            currentDirection = currentDirection.Next();
            transform.Rotate(0, 0, 90);
            transform.position = prevPosition;
            if (isOnGrid) FreeOrOccupyCell(TileState.Taken);
        }
    }

    public bool CanBeRotated()
    {
        if (!isOnGrid) return true;
        else
        {
            var canBeRotated = true;

            var currentPosition = transform.position;
            currentPosition.x -= offSetX;
            currentPosition.y -= offSetY;

            float maxX;
            float minX;
            float maxY;
            float minY;

            if (currentDirection == Direction.Top)
            {
                maxX = currentPosition.x - 1;
                minX = currentPosition.x - 1;

                maxY = currentPosition.y + 1;
                minY = currentPosition.y;
            }
            else if (currentDirection == Direction.Left)
            {
                maxX = currentPosition.x;
                minX = currentPosition.x - 1;

                maxY = currentPosition.y - 1;
                minY = currentPosition.y - 1;
            }
            else if (currentDirection == Direction.Bottom)
            {
                maxX = currentPosition.x + 1;
                minX = currentPosition.x + 1;

                maxY = currentPosition.y;
                minY = currentPosition.y - 1;
            }
            else
            {
                maxX = currentPosition.x + 1;
                minX = currentPosition.x;

                maxY = currentPosition.y + 1;
                minY = currentPosition.y + 1;
            }
            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    //Debug.Log($"{x} {y}");
                    //Debug.Log($"{Mathf.Round(x)} {Mathf.Round(y)}");
                    if (Instance.grids[new Vector2(Mathf.Round(x), Mathf.Round(y))] != TileState.NotTaken)
                    {
                        canBeRotated = false;
                        break;
                    }
                }
                break;
            }
            return canBeRotated;
        }
    }
}
