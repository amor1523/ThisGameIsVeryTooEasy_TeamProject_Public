using System.Collections;
using UnityEngine;

public class PadMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float firstWaitingSeconds;
    [SerializeField] private float waitingSeconds;

    private void Start()
    {
        StartCoroutine(CoInOut());
    }

    IEnumerator CoInOut()
    {
        yield return new WaitForSeconds(firstWaitingSeconds);
        while (true)
        {
            while (true)
            {
                gameObject.transform.localPosition -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
                yield return null;
                if(gameObject.transform.localPosition.z <= -7.0f) break;
            }
            yield return new WaitForSeconds(waitingSeconds);
            while (true)
            {
                gameObject.transform.localPosition += new Vector3(0, 0, moveSpeed * Time.deltaTime);
                yield return null;
                if (gameObject.transform.localPosition.z >= 0.0f) break;
            }
            yield return new WaitForSeconds(waitingSeconds);
        }
    }
}