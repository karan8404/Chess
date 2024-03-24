using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public static class MoveGuide
{
    public static List<Move> moves = new();
    static Piece[,] pieces;
    static Move move;
    static Instantiater instantiater;
    static bool WhiteToMove = true;
    static Vector3Int enPassantMove = new(-10, -10, -1);

    static Vector2Int WhiteKingPos = Utils.V2int(4, 0);
    static Vector2Int BlackKingPos = Utils.V2int(4, 7);

    public static void GenerateMoves(ref Piece[,] pieces, Piece piece, Vector2Int originalPos, Instantiater instantiater)
    {
        moves.Clear();
        MoveGuide.pieces = pieces;
        move = new Move()
        {
            piece = piece,
            start = originalPos
        };
        MoveGuide.instantiater = instantiater;

        if (!IsCorrectTurn())
        {
            Debug.Log("Not correct turn");
            return;
        }
        switch (piece.type)
        {
            case Type.King:
                GenKingMoves();
                break;
            case Type.Queen:
                GenQueenMoves();
                break;
            case Type.Rook:
                GenRookMoves();
                break;
            case Type.Bishop:
                GenBishopMoves();
                break;
            case Type.Knight:
                GenKnightMoves();
                break;
            case Type.Pawn:
                GenPawnMoves();
                //add code to show enpassant move prefab
                break;
        }

        instantiater.ShowMoves(moves);
        if (move.piece.type == Type.Pawn && enPassantMove.z > 0 && Mathf.Abs(move.start.x - enPassantMove.x) == 1 && Mathf.Abs(move.start.y - enPassantMove.y) == 1)
        {

            instantiater.ShowMove(CreateMove(Utils.V2int(enPassantMove.x, enPassantMove.y)));
        }
    }

    public static bool IsCorrectTurn()
    {
        if (WhiteToMove && move.piece.color == Color.Black)
        {
            return false;
        }
        else if (!WhiteToMove && move.piece.color == Color.White)
        {
            return false;
        }
        return true;
    }

    public static bool IsLegal(Move move)
    {
        MoveGuide.move = move;
        if (!Checks())
        {
            return false;
        }

        //Things to do after the move is confirmed.
        WhiteToMove = !WhiteToMove;
        enPassantMove.z--;
        if (enPassantMove.z == 0)
        {
            enPassantMove = new Vector3Int(-10, -10, -1);
        }
        if (move.piece.type == Type.Pawn && Mathf.Abs(move.start.y - move.end.y) == 2)
        {
            enPassantMove = new Vector3Int(move.start.x, ((int)move.piece.color) * 3 + 2, 1);
        }
        if (move.piece.type == Type.King)
        {
            if (move.piece.color == Color.White) WhiteKingPos = move.end;
            else if (move.piece.color == Color.Black) BlackKingPos = move.end;
        }
        return true;
    }

    /// <summary>Helper function to isLegal function</summary>
    public static bool Checks()
    {
        if (IsEnpassantMove())
        {
            Vector2Int captured = Utils.V2int(enPassantMove.x, enPassantMove.y + ((int)move.piece.color) * 2 - 1);
            instantiater.DestroyPiece(PieceAt(captured));
            pieces[captured.x, captured.y] = new Piece(captured);
            return true;
        }
        if (moves.Contains(move))
        {
            return true;
        }
        return false;
    }

    /// <summary>Checks if the current function was an enpassant move</summary>
    /// <remarks>Helper function to checks</remarks>
    public static bool IsEnpassantMove()
    {
        if (move.piece.type != Type.Pawn)
        {
            return false;
        }
        if (move.end.x != enPassantMove.x || move.end.y != enPassantMove.y)
        {
            return false;
        }
        return true;

    }


    /// <summary>Generates moves for sliding pieces</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenSlidingMoves(Vector2Int[] offsets)
    {
        Vector2Int curr;
        foreach (Vector2Int dir in offsets)
        {
            for (int i = 1; ; i++)
            {
                curr = move.start + dir * i;

                if (!Utils.IsPosOnBoard(curr) || (PieceAt(curr).color == move.piece.color && PieceAt(curr).hasPiece))
                    break;

                moves.Add(CreateMove(curr));

                if (PieceAt(curr).hasPiece && PieceAt(curr).color != move.piece.color)
                    break;
            }
        }
    }

    /// <summary>Generate moves for non sliding pieces [knight,pawn]</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenPosMoves(Vector2Int[] offsets)
    {
        Vector2Int curr;

        foreach (Vector2Int dir in offsets)
        {
            curr = move.start + dir;

            if (!Utils.IsPosOnBoard(curr) || (PieceAt(curr).color == move.piece.color && PieceAt(curr).hasPiece))
                continue;

            moves.Add(CreateMove(curr));
        }
    }

    /// <summary>Generates Rook moves from start position</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenRookMoves()
    {
        Vector2Int[] rookOffsets = { Utils.V2int(0, 1), Utils.V2int(-1, 0), Utils.V2int(0, -1), Utils.V2int(1, 0) };

        GenSlidingMoves(rookOffsets);
    }

    /// <summary>Generates Bishop moves from start position</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenBishopMoves()
    {
        Vector2Int[] bishopOffsets = { Utils.V2int(1, 1), Utils.V2int(1, -1), Utils.V2int(-1, 1), Utils.V2int(-1, -1) };

        GenSlidingMoves(bishopOffsets);
    }

    /// <summary>Generates Queen moves from start position</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenQueenMoves()
    {
        Vector2Int[] queenOffsets = { Utils.V2int(0, 1), Utils.V2int(-1, 0), Utils.V2int(0, -1), Utils.V2int(1, 0), Utils.V2int(1, 1), Utils.V2int(1, -1), Utils.V2int(-1, 1), Utils.V2int(-1, -1) };

        GenSlidingMoves(queenOffsets);
    }

    /// <summary>Generates King moves from start position</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenKingMoves()
    {
        Vector2Int[] kingOffsets = { Utils.V2int(0, 1), Utils.V2int(-1, 0), Utils.V2int(0, -1), Utils.V2int(1, 0), Utils.V2int(1, 1), Utils.V2int(1, -1), Utils.V2int(-1, 1), Utils.V2int(-1, -1) };

        GenPosMoves(kingOffsets);
    }

    /// <summary>Generates Knight moves from start position</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenKnightMoves()
    {
        Vector2Int[] knightOffsets = { Utils.V2int(1, 2), Utils.V2int(1, -2), Utils.V2int(-1, 2), Utils.V2int(-1, -2), Utils.V2int(2, 1), Utils.V2int(2, -1), Utils.V2int(-2, 1), Utils.V2int(-2, -1) };

        GenPosMoves(knightOffsets);
    }

    /// <summary>Generates Pawn moves from start position</summary>
    /// <remarks>Helper function to generateMoves</remarks>
    static void GenPawnMoves()
    {
        Vector2Int curr;
        int type = ((int)move.piece.color) * (-2) + 1;
        Vector2Int[] pawnOffsets = { Utils.V2int(0, 1), Utils.V2int(0, 2), Utils.V2int(-1, 1), Utils.V2int(1, 1) };

        for (int i = 0; i < pawnOffsets.Length; i++) { pawnOffsets[i] = pawnOffsets[i] * type; }

        curr = move.start + pawnOffsets[0];
        if (Utils.IsPosOnBoard(curr) && !PieceAt(curr).hasPiece)
        {
            moves.Add(CreateMove(curr));//one square move
            curr = move.start + pawnOffsets[1];
            if (Utils.IsPosOnBoard(curr) && (move.start.y == ((int)move.piece.color) * 7 + type) && !PieceAt(curr).hasPiece)//two square move
                moves.Add(CreateMove(curr));
        }

        for (int i = 2; i < 4; i++)
        {//two diagonal moves
            curr = move.start + pawnOffsets[i];
            if (Utils.IsPosOnBoard(curr) && PieceAt(curr).hasPiece && PieceAt(curr).color != move.piece.color)
                moves.Add(CreateMove(curr));
        }
    }

    //Utility functions
    /// <summary>Returns the piece at given coords</summary>
    /// <remarks>Utility function</remarks>
    static Piece PieceAt(Vector2Int loc)
    {
        return pieces[loc.x, loc.y];
    }

    /// <summary>Returns the piece at given coords</summary>
    /// <remarks>Utility function</remarks>
    static Piece PieceAt(int x, int y)
    {
        return pieces[x, y];
    }

    static Move CreateMove(Vector2Int end)
    {
        return new Move(move.start, end, move.piece);
    }
}