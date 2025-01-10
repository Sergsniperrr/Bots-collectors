using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MinerSearcher
{
    public static Miner FindNearestFreeMiner(Miner[] miners, Ore ore)
    {
        var freeMiners = miners.Where(miner => miner.IsFree);

        return GetNearestMiner(ore, freeMiners);
    }

    private static Miner GetNearestMiner(Ore ore, IEnumerable<Miner> freeMiners)
    {
        if (freeMiners.Count() == 0)
            return null;

        Miner nearestMiner = null;
        float minSqrDistance = float.MaxValue;
        float sqrDistance;

        foreach (Miner miner in freeMiners)
        {
            sqrDistance = CalculateSquareOfDistance(miner, ore);

            if (minSqrDistance > sqrDistance)
            {
                minSqrDistance = sqrDistance;
                nearestMiner = miner;
            }
        }

        return nearestMiner;
    }

    private static float CalculateSquareOfDistance(Miner miner, Ore ore)
    {
        return Vector3.SqrMagnitude(miner.transform.position - ore.transform.position);
    }
}
