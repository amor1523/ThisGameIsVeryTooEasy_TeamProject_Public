using UnityEngine;

public class RockObject : MonoBehaviour
{
    public GameObject[] rockFormations;

    private void Start()
    {
        int childCount = 4; // 자식 오브젝트의 수
        rockFormations = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            rockFormations[i] = transform.Find("RockFormation" + (i + 1)).gameObject;
        }
    }

    private void Update()
    {
        for(int i=0; i < rockFormations.Length; i++)
        {
            if (GameManager.instance.Puzzle[i])
            {
                rockFormations[i].SetActive(false);
            }
        }        
    }
}