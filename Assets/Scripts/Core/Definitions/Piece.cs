using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;
public class Piece
{
    public GameObject instance;
    public Vector2Int location;
    public Type type;
    public Color color;
    public bool hasPiece;

    public Piece(Vector2Int location, Type type, Color color, Instantiater instantiater)
    {
        hasPiece = true;
        this.location = location;
        this.type = type;
        this.color = color;
        this.instance = instantiater.CreatePiece(this);
    }

    public Piece(Vector2Int location)
    {
        hasPiece = false;
        this.location = location;
    }

    public bool MovePiece(Move move)
    {
        if (this != move.piece)
        {
            Debug.Log("Move piece is not referenced piece");
            return false;
        }

        SetPos(move.end);
        return true;
    }

    public void SetPos(Vector2Int pos)
    {
        instance.transform.position = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0) + new Vector3(-3.5f, -3.5f, 0);
        location = pos;
    }

    public void DestroyPiece(Instantiater instantiater)
    {
        if (!hasPiece)
            return;
        hasPiece = false;
        instantiater.DestroyPiece(this);
    }

    public override String ToString()
    {
        return color + " " + type;
    }
}