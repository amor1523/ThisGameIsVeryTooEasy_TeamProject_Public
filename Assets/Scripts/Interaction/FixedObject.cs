using UnityEngine;

public class FixedObject : MonoBehaviour
{
    private bool isFixed = false;
    private Transform playerObject; // 플레이어 오브젝트
    private Vector3 offset; // 오브젝트를 플레이어 앞에 고정
    private float distance = 1f; // 플레이어로부터 오브젝트까지의 거리
    private float heightOffset = 1.2f; // 오브젝트의 높이
    private float smoothSpeed = 50f; // 부드러운 이동

    private Rigidbody rb;

    private void Start()
    {
        // 플레이어 오브젝트 찾기
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();

        offset = transform.position - playerObject.position;
    }

    private void Update()
    {
        if (isFixed)
        {
            // 플레이어의 회전 기준 오브젝트를 고정
            Vector3 targetPosition = playerObject.position + playerObject.forward * distance + Vector3.up * heightOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            transform.rotation = playerObject.rotation;

            // 그래비티를 비활성화
            rb.useGravity = false;
        }
        else
        {
            // 그래비티를 활성화
            rb.useGravity = true;
        }
    }

    public void Interact()
    {
        if (isFixed)
        {
            // 오브젝트 고정 해제
            isFixed = false;
            Debug.Log("Object is unfixed.");
        }
        else
        {
            // 오브젝트를 플레이어 앞에 고정
            isFixed = true;
            Debug.Log("Object is fixed in front of the player.");
        }
    }
}