using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        mousePos.x += 3.5f;
        mousePos.y += 3.5f;
        Vector2Int boardPos = Utils.v2int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));

        if (Input.GetMouseButtonDown(0) && Utils.isPosOnBoard(boardPos))
        {
            pickedPiece = game.board.getPiece(boardPos);
            if (!pickedPiece.hasPiece){
                pickedPiece=getDummyPiece();
            }

            originalPos = boardPos;
            Debug.Log(pickedPiece.color+" "+pickedPiece.type+"  "+originalPos);
        }
        if(Input.GetMouseButton(0))
        {
            
        }
    }

    private Piece getDummyPiece()
    {
        dummy=new Piece(Utils.v2int(-5, -5));
        dummy.instance=new GameObject();
        return dummy;
    }
}