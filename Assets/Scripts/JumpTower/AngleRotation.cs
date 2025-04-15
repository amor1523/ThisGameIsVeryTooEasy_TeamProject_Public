using System.Collections;
using UnityEngine;

public class AngleRotation : RotateFloor
{
    [SerializeField] private float rotationAngle;
    [SerializeField] private float waitingSeconds;

    public override void Rotation()
    {
        StartCoroutine(CoAngleRotation());
    }

    IEnumerator CoAngleRotation()
    {
        while (true)
        {
            float angleAmount = rotationAngle;
            while (true)
            {
                floor.localEulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                angleAmount -= Mathf.Abs(rotationSpeed * Time.deltaTime);
                if (angleAmount <= 0)
                {
                    break;
                }
                yield return null;
            }
            yield return new WaitForSeconds(waitingSeconds);
        }   
    }
}