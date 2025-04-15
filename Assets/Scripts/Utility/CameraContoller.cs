using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float moveSpeed = 5f; // 카메라 이동 속도
    public float verticalMinAngle = -90f; // 최소 수직 각도
    public float verticalMaxAngle = 90f; // 최대 수직 각도
    public float smoothing = 5f; // 카메라 이동 스무딩
    public LayerMask obstacleLayerMask = -1; // 장애물을 감지할 레이어 마스크, 기본값은 모든 레이어

    private Vector2 rotation = Vector2.zero;
    private Vector3 offset;
    private InputAction cameraInputAction;

    private void OnEnable()
    {
        // Camera Input Action 설정
        cameraInputAction = new InputAction(binding: "<Mouse>/delta");
        cameraInputAction.Enable();
    }

    private void OnDisable()
    {
        // Camera Input Action 비활성화
        cameraInputAction.Disable();
    }

    private void Start()
    {
        // 초기 위치 설정
        offset = transform.position - player.position;

        // 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // 마우스 입력으로부터 회전 값 가져오기
        Vector2 mouseDelta = cameraInputAction.ReadValue<Vector2>();
        rotation += mouseDelta * moveSpeed * Time.deltaTime;

        // 수직 회전 각도 제한
        rotation.y = Mathf.Clamp(rotation.y, verticalMinAngle, verticalMaxAngle);

        // 수평 회전은 카메라에 적용
        Quaternion targetRotation = Quaternion.Euler(-rotation.y, rotation.x, 0f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothing * Time.deltaTime);
        transform.rotation = targetRotation;

        // 카메라 위치 설정
        Vector3 targetPosition = player.position + offset;

        // 카메라와 플레이어 사이에 장애물이 있는지 확인
        //RaycastHit hit;
        //if (Physics.Linecast(player.position, targetPosition, out hit, obstacleLayerMask))
        //{
            // 장애물 앞으로 카메라를 이동시킵니다.
           // targetPosition = hit.point;
        //}

        //transform.position = targetPosition;
    }
}