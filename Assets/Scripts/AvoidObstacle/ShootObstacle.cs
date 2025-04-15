using UnityEngine;

public class ShootObstacle : MonoBehaviour
{
    public float shootSpeed = 15f;
    public AvoidObstacleObjectPool aoObjectPool;

    //private void Start()
    //{
    //    aoObjectPool = FindObjectOfType<AvoidObstacleObjectPool>();
    //}

    public void Shoot(string obstacleType)
    {
        GameObject obstacle = aoObjectPool.GetPooledObject(obstacleType);

        if (obstacle != null)
        {
            obstacle.transform.position = transform.position;
            obstacle.SetActive(true);

            Rigidbody rb = obstacle.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = transform.forward * shootSpeed;
            }

            // Weapon스크립트로 shootSpeed 전달
            Weapon weapon = obstacle.GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.Initialize(shootSpeed);
            }

        }
    }
}