using System;
using UnityEngine;

public class Move
{
    public Vector2Int start;
    public Vector2Int end;
    public Piece piece;

    public Move(Vector2Int start, Vector2Int end, Piece piece)
    {
        this.start = start;
        this.end = end;
        this.piece = piece;
    }

    public Move()
    {

    }

    public override String ToString()
    {
        return "Start: " + start + " End: " + end + " Piece: " + piece.ToString();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Move other = (Move)obj;
        return piece == other.piece && start == other.start && end == other.end;
    }

    // GetHashCode method
    public override int GetHashCode()
    {
        return piece.GetHashCode() ^ start.GetHashCode() ^ end.GetHashCode();
    }
}
