using UnityEngine;

public class OreStacker : MonoBehaviour
{
    private readonly Vector3 _initialPosition = new (0.375f, 1.33f, -0.375f);
    private readonly float _oreHorizontalSize = 0.25f;
    private readonly float _oreVerticalSize = 1.666667f;
    private readonly int _maxCountByX = 4;
    private readonly int _maxCountByZ = 4;

    private int _indexCounter;

    public Vector3 CalculatePosition(int placeIndex)
    {
        Vector3 newPosition = _initialPosition;
        int squareByXZ = _maxCountByX * _maxCountByZ;

        int countByY = placeIndex / squareByXZ;
        int countByZ = placeIndex % squareByXZ / _maxCountByX;
        int countByX = placeIndex % squareByXZ % _maxCountByX;

        newPosition.x -= countByX * _oreHorizontalSize;
        newPosition.z += countByZ * _oreHorizontalSize;
        newPosition.y += countByY * _oreVerticalSize;

        return newPosition;
    }

    public void Put(Ore ore)
    {
        ore.transform.localPosition = CalculatePosition(_indexCounter++);
    }
}
