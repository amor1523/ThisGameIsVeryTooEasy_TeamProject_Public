using UnityEngine;

public abstract class RotateFloor : MonoBehaviour
{
    protected Transform floor;
    [SerializeField] protected float rotationSpeed;

    public abstract void Rotation();

    private void Awake()
    {
        floor = GetComponent<Transform>();
    }

    private void Start()
    {
        Rotation();
    }
}