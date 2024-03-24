using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    public GameObject ChessBoard;
    public GameObject pieceContainer;
    public GameObject squareContainer;
    public GameObject[] piecePrefabs;
    public GameObject[] squarePrefabs;
    public GameObject moveIndicatorPrefab;
    public GameObject moveIndicatorParent;

    public GameObject CreatePiece(Piece piece)
    {
        return Instantiate(piecePrefabs[((int)piece.color * 6) + (int)piece.type], V2IntTov3(piece.location), Quaternion.identity, pieceContainer.transform);
    }

    public void DestroyPiece(Piece piece)
    {
        Destroy(piece.instance);
    }

    public GameObject CreateSquare(Square square)
    {
        return Instantiate(squarePrefabs[(int)square.color], V2IntTov3(square.location), Quaternion.identity, squareContainer.transform);
    }

    public void DestroySquare(Square square)
    {
        Destroy(square.instance);
    }

    public Vector3 V2IntTov3(Vector2Int v2)
    {
        return ChessBoard.transform.position + new Vector3(v2.x, v2.y, 0);
    }

    /// <summary>Stores move indicators instances</summary>
    List<GameObject> moveIndicators = new();

    /// <summary>Creates move indicator instances given list of possible moves of picked piece</summary>
    public void ShowMoves(List<Move> moves)
    {
        foreach (Move move in moves)
        {
            moveIndicators.Add(Instantiate(moveIndicatorPrefab, move.end - Vector2.one * 3.5f, Quaternion.identity,moveIndicatorParent.transform));
        }
    }

    /// <summary>Creates a singular move indicator instance given location</summary>
    public void ShowMove(Move move)
    {
        moveIndicators.Add(Instantiate(moveIndicatorPrefab, move.end - Vector2.one * 3.5f, Quaternion.identity,moveIndicatorParent.transform));
    }

    /// <summary>Removes all move indicator instances</summary>
    public void RemoveMoves()
    {
        foreach (GameObject indicator in moveIndicators)
        {
            Destroy(indicator);
        }
        moveIndicators.Clear();
    }
}
