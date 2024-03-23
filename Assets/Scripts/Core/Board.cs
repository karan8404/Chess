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

    public bool MovePiece(Piece piece, Move move)
    {
        if (!Utils.IsPosOnBoard(move.end))
        {
            piece.setPos(move.start);
            return false;
        }
        Piece captured = GetPiece(move.end);
        if (piece.color == captured.color && captured.hasPiece)
        {
            piece.setPos(move.start);
            return false;
        }

        //unity instance handling
        captured.destroyPiece(instantiater);
        piece.movePiece(move);

        //modifying array
        SetPiece(move.start, new Piece(move.start));
        SetPiece(move.end, piece);


        return true;
    }

    public Piece GetPiece(Vector2Int location)
    {
        return pieces[location.x, location.y];
    }

    public bool SetPiece(Vector2Int location, Piece piece)
    {
        pieces[location.x, location.y] = piece;
        return true;
    }
}
