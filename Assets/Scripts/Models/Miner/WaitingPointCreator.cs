using UnityEngine;

public static class WaitingPointCreator
{
    public static Vector3[] Create(Vector3 centrePosition, int pointsCount, float areaRadius)
    {
        if (pointsCount == 0)
            return null;

        Vector3[] waitingPoints = new Vector3[pointsCount];
        int doubler = 2;
        float shift = -0.15f;

        for (int i = 0; i < waitingPoints.Length; i++)
        {
            float angle = i * Mathf.PI * doubler / waitingPoints.Length;
            float x = Mathf.Cos(angle) * areaRadius;
            float z = Mathf.Sin(angle) * areaRadius;
            Vector3 position = centrePosition + new Vector3(x, shift, z);
            waitingPoints[i] = position;
        }

        return waitingPoints;
    }
}
