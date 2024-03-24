using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class FenUtility
{
    public static string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    public static void CreateBoard(string fen, Board board, Instantiater instantiater)
    {
        int row, column;
        //creating squares first
        for (row = 0; row < 8; row++)
        {
            for (column = 0; column < 8; column++)
            {
                board.squares[column, row] = new Square(Utils.V2int(column, row), (Color)((row + column + 1) % 2), instantiater);
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
                    board.pieces[column++, row] = new Piece(Utils.V2int(column, row));

            }
            else if (char.IsLetter(c))
            {
                Type type;
                Color color;
                CharPiece(c, out type, out color);

                board.pieces[column, row] = new Piece(Utils.V2int(column, row), type, color, instantiater);
                column++;
            }
            else if (c == '/')
            {
                row--;
                column = 0;
            }
        }

    }

    public static void CharPiece(char c, out Type type, out Color color)
    {
        if (char.IsUpper(c))
            color = Color.White;
        else
            color = Color.Black;

        type = char.ToLower(c) switch
        {
            'k' => Type.King,
            'q' => Type.Queen,
            'r' => Type.Rook,
            'b' => Type.Bishop,
            'n' => Type.Knight,
            'p' => Type.Pawn,
            _ => Type.King,
        };
    }
}