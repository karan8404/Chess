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

    public void startGame()
    {
        FenUtility.createBoard(FenUtility.startingPosition, this, instantiater);
    }

    public bool movePiece(Piece piece, Move move)
    {
        Piece captured=getPiece(move.end);
        if(piece.color == captured.color)
            return false;

        //unity instance handling
        captured.destroyPiece(instantiater);
        piece.movePiece(move);

        //modifying array
        pieces[move.start.x,move.start.y]=new Piece(move.start);
        pieces[move.end.x,move.end.y]=piece;

        
        return true;
    }

    public Piece getPiece(Vector2Int location)
    {
        return pieces[location.x,location.y];
    }
}
