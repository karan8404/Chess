using UnityEngine;

public class Move
{
    public Vector2Int start;
    public Vector2Int end;
    public Piece piece;

    public Move(Vector2Int start, Vector2Int end, Piece piece)
    {
        this.start=start;
        this.end=end;
        this.piece=piece;
    }

    
}
