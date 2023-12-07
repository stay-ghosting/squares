using UnityEngine;
using System.Collections.Generic;

public class PlayerGrid : MonoBehaviour
{
    [SerializeField]
    Transform[] gridPositions = new Transform[9];

    /// <Summary>returns a pos as a vector3 in world space from direction</Summary>
    public Dictionary<Vector2, Vector3> vector2ToGridPosDict = new Dictionary<Vector2, Vector3>();

    void Awake()
    {
        vector2ToGridPosDict = setUpDictionary(gridPositions);
    }

    /// <Summary>returns a random edge pos as a vector3 in world space</Summary>
    public Vector3 RandomGridEdgePos()
    {
        // 5 is middle
        int index = 4;
        while (index == 4)
        {
            System.Random rng = new System.Random();
            index = rng.Next(gridPositions.Length - 1);
        }
        return gridPositions[index].position;
    }

    Dictionary<Vector2, Vector3> setUpDictionary(Transform[] gridPositions)
    {
        int x = -1;
        int y = -1;

        Dictionary<Vector2, Vector3> toReturn = new Dictionary<Vector2, Vector3>();

        foreach (Transform _gridPosition in gridPositions)
        {
            Vector2 key = new Vector2(x, y);

            toReturn.Add(key, _gridPosition.position);

            if (x == 1)
            {
                x = -1;
                y++;
            }
            else
            {
                x++;
            }
        }

        return toReturn;
    }
}