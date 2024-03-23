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

    public bool movePiece(Move move)
    {
        if (this != move.piece)
            return false;

        setPos(move.end);
        return true;
    }

    public void setPos(Vector2Int pos)
    {
        instance.transform.position = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0) + new Vector3(-3.5f, -3.5f, 0);
        location = pos;
    }

    public void destroyPiece(Instantiater instantiater)
    {
        if (!hasPiece)
            return;
        hasPiece = false;
        instantiater.DestroyPiece(this);
    }
}