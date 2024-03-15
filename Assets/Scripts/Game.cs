using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Board board;
    public Instantiater instantiater;

    // Start is called before the first frame update
    void Start()
    {
        board = new Board(instantiater);
        board.StartGame();
    }
}
