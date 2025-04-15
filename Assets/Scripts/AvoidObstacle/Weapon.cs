using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AvoidObstacleObjectPool aoObjectPool;
    private string obstacleType;
    private Rigidbody rb;
    private float shootSpeed;

    private void Start()
    {
        aoObjectPool = FindObjectOfType<AvoidObstacleObjectPool>();

        // 정확한 Object의 이름을 얻기 위해 사용
        // gameObject.name : 현재 게임 오브젝트의 이름을 가져옵니다.
        // Replace("(Clone)", "") : 문자열에서 (Clone)을 제거합니다.
        // Trime() 문자열의 앞뒤 공백을 제거합니다.
        obstacleType = gameObject.name.Replace("(Clone)", "").Trim();

        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float speed)
    {
        shootSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 레이어충돌(CompareTag보다 성능상 좋음)
        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            aoObjectPool.ReturnToPool(obstacleType, gameObject);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                rb.velocity = transform.forward * shootSpeed;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                rb.velocity = transform.forward * shootSpeed;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                rb.velocity = transform.forward * shootSpeed;
            }
        }
    }
}