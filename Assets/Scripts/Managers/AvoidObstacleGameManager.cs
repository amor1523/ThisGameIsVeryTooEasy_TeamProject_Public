using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacleGameManager : MonoBehaviour
{
    public List<ShootObstacle> shooters; // ShootObstacle 스크립트가 붙은 Gizmo 오브젝트들 등록
    public List<string> obstacleTypes; // 장애물 종류 List
    private AvoidObstacleObjectPool aoObjectPool;
    private string currentObstacleType;

    public bool clearAvoidObstacle = false;

    private void Start()
    {
        aoObjectPool = FindObjectOfType<AvoidObstacleObjectPool>();
        StartCoroutine(ShootObstaclesRoutine());
    }

    private IEnumerator ShootObstaclesRoutine()
    {
        while (!clearAvoidObstacle)
        {
            currentObstacleType = obstacleTypes[Random.Range(0, obstacleTypes.Count)]; // 랜덤으로 장애물 선택
            

            int noShootGizmoIndex = Random.Range(0, shooters.Count); // 쏘지 않는 Gizmo

            for (int i = 0; i < shooters.Count; i++)
            {
                if (i != noShootGizmoIndex)
                {
                    shooters[i].Shoot(currentObstacleType);
                }
            }

            yield return new WaitForSeconds(2f); // 쏘는 간격 난이도에 따라 수정
        }
    }

}