using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private Vector3 entrancePosition; // 입구 위치

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            Debug.Log("미로 탈출!");

            UIManager.Instance.TurnOnText();

            // 플레이어를 입구 위치로 순간이동
            Vector3 entrancePosition = MazeGenerator.instance.GetEntrancePosition();
            other.transform.position = entrancePosition;

            // UI 텍스트 표시
            UIManager.Instance.TurnOnText(UIManager.Instance.text[(int)(PuzzleList.Maze)]);
            GameManager.instance.Puzzle[(int)PuzzleList.Maze] = true; //클리어 상태 저장

            // 트리거 상태 변경
            triggered = true;
        }
    }
}