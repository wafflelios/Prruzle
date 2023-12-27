using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Cat1x2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static float offSetX = 0;
    public static float offSetY = 0.5f;
    public static GridManager gridManager;
    //public static InputHandler inputHandler;
    public Vector3 startPosition;
    public static float counter = 0;
    public float forNewPosition = 0.5f;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        startPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckIfOnTheGrid(GetTilePositions()))
            FreeOrOccupyCell(GridManager.TileState.NotTaken);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.x += offSetX;
            mouseWorldPosition.y += offSetY;
            mouseWorldPosition.z = 0f;
            var positions = GetTilePositions();
            Debug.Log($"{positions[0]} {positions[1]}");
            if (CheckIfOnTheGrid(positions))
            {
                transform.position = gridManager.GetNearestPointOnGrid(mouseWorldPosition, offSetX, offSetY);
            }
            else transform.position = mouseWorldPosition;
        }
    }

    public List<Vector3> GetTilePositions()
    {
        List<Vector3> tilePositions = new();
        tilePositions.Add(new Vector3(Mathf.Round(transform.position.x), Mathf.Floor(transform.position.y), 0));
        if (counter % 2 == 0)
        {
            tilePositions.Add(new Vector3(Mathf.Round(transform.position.x), Mathf.Floor(transform.position.y) + 1, 0));
        }
        else
        {
            tilePositions.Add(new Vector3(Mathf.Round(transform.position.x) + 1, Mathf.Floor(transform.position.y), 0));
        }
        return tilePositions;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var positions = GetTilePositions();
        if (CheckIfOnTheGrid(positions))
        {
            var sentToStart = false;
            foreach (var position in positions)
            {
                Debug.Log($"{positions[0]} {positions[1]}");
                if (gridManager.grids[position] == GridManager.TileState.Taken || gridManager.grids[position] == GridManager.TileState.CantBeTaken)
                {
                    transform.position = startPosition;
                    sentToStart = true;
                    break;
                }
            }
            if (!sentToStart)
                FreeOrOccupyCell(GridManager.TileState.Taken);
        }
    }

    public void FreeOrOccupyCell(GridManager.TileState tileState)
    {
        foreach (var position in GetTilePositions())
            gridManager.grids[position] = tileState;
    }

    public void ChangeOffSet(int sign)
    {
        if (offSetX == 0)
        {
            offSetX = sign * offSetY;
            offSetY = 0;
        }
        else
        {
            offSetY = sign * offSetX;
            offSetX = 0;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (CanBeRotated() && eventData.button == PointerEventData.InputButton.Right)
        {
            var prevPosition = transform.position;
            if (counter == 0)
            {
                ChangeOffSet(-1);
                prevPosition.x -= forNewPosition;
                prevPosition.y -= forNewPosition;
            }
            else if (counter == 1)
            {
                ChangeOffSet(1);
                prevPosition.x += forNewPosition;
                prevPosition.y -= forNewPosition;
            }
            else if (counter == 2)
            {
                ChangeOffSet(-1);
                prevPosition.x += forNewPosition;
                prevPosition.y += forNewPosition;
            }
            else
            {
                ChangeOffSet(1);
                prevPosition.x -= forNewPosition;
                prevPosition.y += forNewPosition;
            }
            counter += 1;
            counter %= 4;
            transform.Rotate(0, 0, 90);
            transform.position = prevPosition;
        }
    }

    public bool CanBeRotated()
    {
        Vector3 tile;
        if (counter == 0)
        {
            tile = new Vector3(Mathf.Round(transform.position.x) - 1, Mathf.Floor(transform.position.y));
        }
        else if (counter == 1)
        {
            tile = new Vector3(Mathf.Round(transform.position.x), Mathf.Floor(transform.position.y) - 1);
        }
        else if (counter == 2)
        {
            tile = new Vector3(Mathf.Round(transform.position.x) + 1, Mathf.Floor(transform.position.y));
        }
        else
        {
            tile = new Vector3(Mathf.Round(transform.position.x), Mathf.Floor(transform.position.y) + 1);
        }
        if (CheckIfOnTheGrid(new List<Vector3>() { tile }) && (gridManager.grids[tile] == GridManager.TileState.Taken || gridManager.grids[tile] == GridManager.TileState.CantBeTaken))
            return false;
        else return true;
    }

    public bool CheckIfOnTheGrid(List<Vector3> positions)
    {
        foreach (var position in positions) 
        {
            try
            {
                var check = gridManager.grids[position];
            }
            catch
            {
                return false;
            }
        }
        return true;
    }
}