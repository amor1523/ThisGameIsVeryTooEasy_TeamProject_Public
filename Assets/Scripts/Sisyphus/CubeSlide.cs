using UnityEngine;

public class CubeSlide : MonoBehaviour
{
    public float moveSpeed;
    private bool changeDirection = false;

    private Vector3 startPosition;

    private void Start()
    {
       startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PanelMovement();
    }


    private void PanelMovement()
    {
        if (!changeDirection)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            if (transform.position.z - startPosition.z > 15) changeDirection = true;
        }
        else
        {
            transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;
            if (transform.position.z - startPosition.z < 0) changeDirection = false;
        }
    }
}