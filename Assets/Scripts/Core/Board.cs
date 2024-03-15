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
        Piece captured = GetPiece(move.end);
        if (piece.color == captured.color)
            return false;

        //unity instance handling
        captured.destroyPiece(instantiater);
        piece.movePiece(move);

        //modifying array
        pieces[move.start.x, move.start.y] = new Piece(move.start);
        pieces[move.end.x, move.end.y] = piece;


        return true;
    }

    public Piece GetPiece(Vector2Int location)
    {
        return pieces[location.x, location.y];
    }
}
