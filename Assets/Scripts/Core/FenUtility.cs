using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class FenUtility
{
    public static string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    public static void createBoard(string fen, Board board, Instantiater instantiater)
    {
        int row, column;
        //creating squares first
        for (row = 0; row < 8; row++)
        {
            for (column = 0; column < 8; column++)
            {
                board.squares[column, row] = new Square(Utils.v2int(column, row), (Color)((row + column + 1) % 2), instantiater);
            }
        }

        //creating pieces
        row = 7; column = 0;
        foreach (char c in fen)
        {
            if (char.IsDigit(c))
            {
                int count = (int)char.GetNumericValue(c);
                while (count-- > 0)
                    board.pieces[column++, row] = new Piece(Utils.v2int(column, row));

            }
            else if (char.IsLetter(c))
            {
                Type type;
                Color color;
                charPiece(c, out type, out color);

                board.pieces[column, row] = new Piece(Utils.v2int(column, row), type, color, instantiater);
                column = column + 1;
            }
            else if (c == '/')
            {
                row = row - 1;
                column = 0;
            }
        }

    }

    public static void charPiece(char c, out Type type, out Color color)
    {
        if (char.IsUpper(c))
            color = Color.White;
        else
            color = Color.Black;

        switch (char.ToLower(c))
        {
            case 'k':
                type = Type.King;
                break;
            case 'q':
                type = Type.Queen;
                break;
            case 'r':
                type = Type.Rook;
                break;
            case 'b':
                type = Type.Bishop;
                break;
            case 'n':
                type = Type.Knight;
                break;
            case 'p':
                type = Type.Pawn;
                break;
            default:
                type = Type.King;
                break;
        }
    }
}