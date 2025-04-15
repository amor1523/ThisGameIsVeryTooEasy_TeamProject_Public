using UnityEngine;

public class JumpTowerClear : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Clear();
    }

    public void Clear()
    {
        UIManager.Instance.TurnOnText();
        UIManager.Instance.TurnOnText(UIManager.Instance.text[(int)(PuzzleList.JumpTower)]);
        GameManager.instance.Puzzle[(int)PuzzleList.JumpTower] = true; //클리어 상태 저장
        gameObject.SetActive(false);
    }
}