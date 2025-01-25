using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OreStacker : MonoBehaviour
{
    private readonly List<Ore> _ores = new();
    private readonly Vector3 _initialPosition = new(0.75f, 0.25f, -0.75f);
    private readonly float _oreHorizontalSize = 0.5f;
    private readonly float _oreVerticalSize = 0.5f;
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
        _ores.Add(ore);
        ore.transform.rotation = Quaternion.Euler(Vector3.zero);
        ore.transform.localPosition = CalculatePosition(_indexCounter++);
    }

    public void Remove(KeyValuePair<string, int> removableOre)
    {
        var oresOfRequiredType = _ores.Where(ore => ore.Name == removableOre.Key).ToArray();

        for (int i = 0; i < removableOre.Value; i++)
        {
            _ores.Remove(oresOfRequiredType[i]);

            Destroy(oresOfRequiredType[i].gameObject);
        }
    }

    public void UpdateOresPositions()
    {
        for (int i = 0; i < _ores.Count; i++)
            _ores[i].transform.position = CalculatePosition(i);
    }
}
