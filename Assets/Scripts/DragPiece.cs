using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragPiece : MonoBehaviour
{
    public Game game;
    Vector2Int originalPos;
    Vector3 mousePos;
    Piece pickedPiece;
    Piece dummy;
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int boardPos = Utils.V2int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
        Vector2Int arrayPos = Utils.V2int(Mathf.RoundToInt(mousePos.x + 3.5f), Mathf.RoundToInt(mousePos.y + 3.5f));

        //to pick up a piece
        if (Input.GetMouseButtonDown(0) && Utils.IsPosOnBoard(arrayPos))
        {
            pickedPiece = game.board.GetPiece(arrayPos);
            originalPos = arrayPos;
            if (!pickedPiece.hasPiece)
            {
                pickedPiece = GetDummyPiece();
                return;
            }

            game.board.PickPiece(pickedPiece, originalPos);
        }

        if (Input.GetMouseButton(0))
        {
            pickedPiece.instance.transform.position = mousePos + Vector3.forward;
        }

        //to drop a piece.
        if (Input.GetMouseButtonUp(0))
        {
            if (pickedPiece == dummy)
            {
                pickedPiece.SetPos(Utils.V2int(-5, -5));
                return;
            }
            Move move = new(originalPos, arrayPos, pickedPiece);

            game.board.MovePiece(move);
        }
    }

    private Piece GetDummyPiece()
    {
        dummy = new Piece(Utils.V2int(-5, -5))
        {
            instance = new GameObject()
        };
        return dummy;
    }
}