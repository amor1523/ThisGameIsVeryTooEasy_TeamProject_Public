using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacleObjectPool : MonoBehaviour
{
    // Resources 폴더에서 로드할 프리팹
    public List<string> obstacleNames;

    // 기존 Dictionary의 Value 값을 Lsti<GameObject>로 했으나 
    // Object Pooling의 효율성을 높이기 위해 Queue로 변경 FIFO(First Input First Out) 구조로 오브젝트를 순서대로 사용하는데 유리
    // List를 사용할 경우 비활성화 된 오브젝트를 찾기 위해 매번 List를 순환하여야한다.
    public Dictionary<string, Queue<GameObject>> objectPoolDictionary;

    public int poolSize = 30;
    
    private void Awake()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        //obstacleRotations = new Dictionary<string, Quaternion>();

        //각 장애물마다 오브젝트 풀 생성
        foreach (var obstacleName in obstacleNames)
        {
            Queue<GameObject> obstaclePool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obstaclePrefab = Resources.Load<GameObject>($"{obstacleName}");
                if (obstaclePrefab != null)
                {
                    GameObject obstacleInstance = Instantiate(obstaclePrefab, transform);
                    obstacleInstance.SetActive(false);
                    obstaclePool.Enqueue(obstacleInstance);
                }
                else
                {
                    Debug.LogError($"Failed to load prefab: {obstacleName}");
                }
            }

            objectPoolDictionary.Add(obstacleName, obstaclePool);
        }
    }

    public GameObject GetPooledObject(string type)
    {
        if (objectPoolDictionary.ContainsKey(type))
        {
            Queue<GameObject> pool = objectPoolDictionary[type];
            if (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                if (obj != null)
                {
                    return obj;
                }
            }
        }
        return null;
    }

    public void ReturnToPool(string type, GameObject obj)
    {
        if (objectPoolDictionary.ContainsKey(type))
        {
            obj.SetActive(false);
            objectPoolDictionary[type].Enqueue(obj);
        }
    }
}