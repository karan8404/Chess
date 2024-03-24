using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Piece[,] pieces;
    public Square[,] squares;
    public Instantiater instantiater;

    public Board(Instantiater instantiater)
    {
        pieces = new Piece[8, 8];
        squares = new Square[8, 8];
        this.instantiater = instantiater;
    }

    public void StartGame()
    {
        FenUtility.CreateBoard(FenUtility.startingPosition, this, instantiater);
    }

    public void PickPiece(Piece piece, Vector2Int originalPos)
    {
        MoveGuide.GenerateMoves(ref pieces, piece, originalPos, instantiater);
    }

    public void MovePiece(Move move)
    {
        if (Utils.IsPosOnBoard(move.end) &&MoveGuide.IsLegal(move))
        {
            Piece captured = GetPiece(move.end);
            //set original position to not have a piece
            //for final position, destroy instance, set piece position to it and modify it to have a piece. 
            captured.DestroyPiece(instantiater);
            move.piece.MovePiece(move);

            //modifying array
            SetPiece(move.start, new Piece(move.start));
            SetPiece(move.end, move.piece);
            Debug.Log("Placed");
        }
        else
        {
            move.piece.SetPos(move.start);
            Debug.Log("failed");
        }
        instantiater.RemoveMoves();
    }

    public Piece GetPiece(Vector2Int location)
    {
        return pieces[location.x, location.y];
    }

    public void SetPiece(Vector2Int location, Piece piece)
    {
        pieces[location.x, location.y] = piece;
    }
}
