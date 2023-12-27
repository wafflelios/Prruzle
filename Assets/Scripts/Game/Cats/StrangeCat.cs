using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GridManager;

public class StrangeCat : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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
        if (startDirection == Quaternion.Euler(new Vector3(0, 0, 0)))
        {
            offSetX = 0.5f;
            offSetY = 0.5f;
            return Direction.Top;
        }
        else if (startDirection == Quaternion.Euler(new Vector3(0, 0, 90)))
        {
            offSetX = -0.5f;
            offSetY = 0.5f;
            return Direction.Left;
        }
        else if (startDirection == Quaternion.Euler(new Vector3(0, 0, 180)))
        {
            offSetX = -0.5f;
            offSetY = -0.5f;
            return Direction.Bottom;
        }
        else
        {
            offSetX = 0.5f;
            offSetY = -0.5f;
            return Direction.Right;
        }
    }

    private void Awake()
    {
        startPosition = transform.position;
        startDirection = transform.rotation;
        currentDirection = FindDirection(startDirection);
    }

    public void SentToTheStart()
    {
        transform.SetPositionAndRotation(startPosition, startDirection);
        currentDirection = FindDirection(startDirection);
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
        var result = new Vector2[3];

        var currentPosition = transform.position;
        currentPosition.x -= offSetX;
        currentPosition.y -= offSetY;

        result[0] = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y));
        if (currentDirection == Direction.Top)
        {
            result[1] = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y + 1));
            result[2] = new Vector2(Mathf.Round(currentPosition.x + 1), Mathf.Round(currentPosition.y));
        }
        else if (currentDirection == Direction.Left)
        {
            result[1] = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y + 1));
            result[2] = new Vector2(Mathf.Round(currentPosition.x - 1), Mathf.Round(currentPosition.y));
        }
        else if (currentDirection == Direction.Bottom)
        {
            result[1] = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y - 1));
            result[2] = new Vector2(Mathf.Round(currentPosition.x - 1), Mathf.Round(currentPosition.y));
        }
        else
        {
            result[1] = new Vector2(Mathf.Round(currentPosition.x + 1), Mathf.Round(currentPosition.y));
            result[2] = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y - 1));
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
            var currentPosition = transform.position;
            currentPosition.x -= offSetX;
            currentPosition.y -= offSetY;

            float x;
            float y;

            if (currentDirection == Direction.Top)
            {
                x = currentPosition.x - 1;
                y = currentPosition.y;
            }
            else if (currentDirection == Direction.Left)
            {
                x = currentPosition.x;
                y = currentPosition.y - 1;
            }
            else if (currentDirection == Direction.Bottom)
            {
                x = currentPosition.x + 1;
                y = currentPosition.y;
            }
            else
            {
                x = currentPosition.x;
                y = currentPosition.y + 1;
            }
            if (Instance.grids[new Vector2(Mathf.Round(x), Mathf.Round(y))] == TileState.NotTaken)
                return true;
            else return false;
        }
    }
}
