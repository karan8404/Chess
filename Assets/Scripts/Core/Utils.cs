using UnityEngine;

public static class Utils
{
    public static bool IsPosOnBoard(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > 7)
            return false;
        if (pos.y < 0 || pos.y > 7)
            return false;
        return true;
    }

    public static Vector2Int V2int(int x, int y)
    {
        return new Vector2Int(x, y);
    }

    public static Vector2Int Normalize(Vector2 pos)
    {
        return V2int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }
}
