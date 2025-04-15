using System.Collections;
using UnityEngine;

public class ContinuousRotation : RotateFloor
{
    public override void Rotation()
    {
        StartCoroutine(CoContinuousRotation());
    }

    IEnumerator CoContinuousRotation()
    {
        while (true)
        {
            floor.localEulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}