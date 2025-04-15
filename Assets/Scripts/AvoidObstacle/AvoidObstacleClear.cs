using UnityEngine;

public class AvoidObstacleClear : MonoBehaviour
{
    private AvoidObstacleObjectPool aoObjectPool;
    private AvoidObstacleGameManager avoidObstacleGameManager;

    private void Start()
    {
        avoidObstacleGameManager = FindObjectOfType<AvoidObstacleGameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.TurnOnText();
            UIManager.Instance.TurnOnText(UIManager.Instance.text[(int)(PuzzleList.AvoidObstacle)]);
            GameManager.instance.Puzzle[(int)PuzzleList.AvoidObstacle] = true; //클리어 상태 저장
            avoidObstacleGameManager.clearAvoidObstacle = true;
        }
    }
}