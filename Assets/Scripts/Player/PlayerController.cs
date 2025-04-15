using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float originalMoveSpeed;
    public float moveSpeed = 5f;
    public float RunSpeed = 10f; // 추가 달리기 속도
    public float jumpForce = 5f;
    public LayerMask groundLayerMask;
    public Transform rotationTarget; // 회전 대상
    public Transform cameraTransform; // 카메라의 Transform
    public bool useCameraDirection = true; // 카메라 방향 사용 여부
    public Animator animator; // 애니메이터 컴포넌트 참조
    public AudioSource walkAudioSource; // 걷기 효과음 AudioSource
    public AudioClip walkClip; // 걷기 효과음 Clip

    private Vector2 curMovementInput;
    private bool Run;
    private Rigidbody _rigidbody;
    public float gravityForce;
    private Animator _animator;
    private Quaternion targetRotation; // 목표 회전
    public float rotationSpeed = 5f; // 회전 속도

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();

        // 초기 이동 속도를 originalMoveSpeed 변수에 저장
        originalMoveSpeed = moveSpeed;

        // AudioSource 초기화
        walkAudioSource = GetComponent<AudioSource>();
        walkAudioSource.clip = walkClip;
        walkAudioSource.loop = true;
    }

    private void Update()
    {
        bool Walk = curMovementInput != Vector2.zero && IsGrounded();
        _animator.SetBool("Walk", Walk);
        _animator.SetBool("Run", Run);

        // Walk SoundClip 재생
        if (Walk && !walkAudioSource.isPlaying)
        {
            walkAudioSource.Play();
        }
        else if (!Walk&& walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
        }

        // 마우스 클릭을 감지하고 상호작용을 수행
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PerformInteraction();
        }
    }

    private void FixedUpdate()
    {
        Move();
        _rigidbody.AddForce(Vector3.down * gravityForce);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            Run = true;
            walkAudioSource.pitch = 1.5f; // 달릴 때 재생 속도를 1.5배로 설정

            // 달리기 상태일 때 이동 속도를 더 빠른 값으로 설정
            if (useCameraDirection)
            {
                moveSpeed *= RunSpeed;
            }
            else
            {
                moveSpeed = RunSpeed;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Run = false;
            walkAudioSource.pitch = 1f; // 걷기 상태로 돌아오면 재생 속도를 원래대로 설정
            // 달리기가 종료되면 이동 속도를 원래 값으로 복원
            moveSpeed = originalMoveSpeed;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _animator.SetTrigger("Jump");
            }
        }
    }

    private void Move()
    {
        Vector3 direction;

        if (useCameraDirection)
        {
            // 카메라의 전방 벡터를 사용하여 이동 방향을 계산
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();
            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            direction = curMovementInput.y * cameraForward + curMovementInput.x * cameraRight;
        }
        else
        {
            // 플레이어의 전방 벡터를 사용하여 이동 방향을 계산
            Vector3 playerForward = rotationTarget.forward;
            playerForward.y = 0f;
            playerForward.Normalize();
            Vector3 playerRight = rotationTarget.right;
            playerRight.y = 0f;
            playerRight.Normalize();

            direction = curMovementInput.y * playerForward + curMovementInput.x * playerRight;
        }

        Vector3 velocity = direction * moveSpeed;
        velocity.y = _rigidbody.velocity.y; // 유지 중력 및 기타 Y축 속도

        _rigidbody.velocity = velocity;

        // 이동 방향으로 플레이어 회전
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // 이동 방향으로 회전
            rotationTarget.rotation = Quaternion.Lerp(rotationTarget.rotation, targetRotation, rotationSpeed * Time.deltaTime); // 부드러운 회전 보간
        }
    }

    private void PerformInteraction()
    {
        // 카메라의 중심을 기준으로 레이를 쏩니다.
        Vector3 rayOrigin = cameraTransform.position + cameraTransform.forward * cameraTransform.GetComponent<Camera>().nearClipPlane;

        // 카메라의 중심에서 레이를 쏘아 충돌한 오브젝트를 검출합니다.
        Ray ray = new Ray(rayOrigin, cameraTransform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            // 충돌한 오브젝트가 ButtonObject 컴포넌트를 포함하는지 확인합니다.
            ButtonObject buttonObject = hitInfo.collider.GetComponent<ButtonObject>();
            if (buttonObject != null)
            {
                // 버튼형 오브젝트일 경우, 버튼을 트리거합니다.
                buttonObject.Trigger();
                return; // 상호작용 종료
            }

            // 충돌한 오브젝트가 FixedObject 컴포넌트를 포함하는지 확인합니다.
            FixedObject fixedObject = hitInfo.collider.GetComponent<FixedObject>();
            if (fixedObject != null)
            {
                // 물체형 오브젝트일 경우, 상호작용을 수행합니다.
                fixedObject.Interact();
                return; // 상호작용 종료
            }
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, groundLayerMask))
            {
                Debug.Log("true");
                return true;
            }
        }
        Debug.Log("false");
        return false;
    }
}