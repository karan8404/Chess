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

    public GameObject createPiece(Piece piece)
    {
        return Instantiate(piecePrefabs[((int)piece.color * 6) + (int)piece.type], v2IntTov3(piece.location), Quaternion.identity, pieceContainer.transform);
    }

    public void destroyPiece(Piece piece)
    {
        Destroy(piece.instance);
    }

    public GameObject createSquare(Square square)
    {
        return Instantiate(squarePrefabs[(int)square.color], v2IntTov3(square.location), Quaternion.identity, squareContainer.transform);
    }

    public void destroySquare(Square square)
    {
        Destroy(square.instance);
    }

    public Vector3 v2IntTov3(Vector2Int v2)
    {
        return (ChessBoard.transform.position + new Vector3(v2.x, v2.y, 0));
    }
}
