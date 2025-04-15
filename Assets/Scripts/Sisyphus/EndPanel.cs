using UnityEngine;

public class EndPanel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Rock")
        {
            Debug.Log("Clear!!");
            UIManager.Instance.TurnOnText();
            UIManager.Instance.TurnOnText(UIManager.Instance.text[(int)(PuzzleList.Sisyphus)]);
            GameManager.instance.Puzzle[(int)PuzzleList.Sisyphus] = true; //클리어 상태 저장
            collision.gameObject.SetActive(false);
        }
    }
}