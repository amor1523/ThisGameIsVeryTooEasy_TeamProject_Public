using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public GameObject EndPanel;

    public void Trigger()
    {
        // 버튼 오브젝트의 동작을 여기에 구현합니다.
        Debug.Log("Button object is triggered.");
        EndPanel.SetActive(true);
    }
}