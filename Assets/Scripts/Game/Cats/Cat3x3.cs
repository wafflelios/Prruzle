using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GridManager;

public class Cat3x3 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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
        currentDirection = FindDirection(startDirection);
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
        var result = new Vector2[9];
        var index = 0;

        var currentPosition = transform.position;

        var maxX = currentPosition.x + 1;
        var minX = currentPosition.x - 1;
        var maxY = currentPosition.y + 1;
        var minY = currentPosition.y - 1;

        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                result[index] = new Vector2(Mathf.Round(x), Mathf.Round(y));
                index += 1;
            }
        }
        return result;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !GameManager.Instance.GameWon)
        {
            currentDirection = currentDirection.Next();
            transform.Rotate(0, 0, 90);
        }
    }
}
