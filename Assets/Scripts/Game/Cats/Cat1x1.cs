using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GridManager;

public class Cat1x1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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

    private void Awake()
    {
        startPosition = transform.position;
        startDirection = transform.rotation;
        if (startDirection == Quaternion.Euler(new Vector3(0, 0, 0))) currentDirection = Direction.Top;
        else if (startDirection == Quaternion.Euler(new Vector3(0, 0, 90))) currentDirection = Direction.Left;
        else if (startDirection == Quaternion.Euler(new Vector3(0, 0, 180))) currentDirection = Direction.Bottom;
        else currentDirection = Direction.Right;
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
        var position = GetTilePosition();
        //Debug.Log(grids.Count);
        if (tileState == TileState.Taken)
        {
            if (!Instance.grids.ContainsKey(position) || Instance.grids[position] == TileState.Taken || Instance.grids[position] == TileState.CantBeTaken)
                SentToTheStart();
            else Instance.grids[position] = tileState;
        }
        else Instance.grids[position] = tileState;
    }

    public Vector3 GetTilePosition()
    {
        return new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !GameManager.Instance.GameWon)
        {
            transform.Rotate(0, 0, 90);
            currentDirection = currentDirection.Next();
        }
    }
}