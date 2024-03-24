using UnityEngine;

public class Square
{
    public GameObject instance;
    public Vector2Int location;
    public Color color;

    public Square(Vector2Int location, Color color, Instantiater instantiater)
    {
        this.location = location;
        this.color = color;
        instance = instantiater.CreateSquare(this);
    }

    public void DestroySquare(Instantiater instantiater)
    {
        instantiater.DestroySquare(this);
    }
}
