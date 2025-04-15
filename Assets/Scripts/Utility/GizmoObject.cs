using UnityEngine;

public class GizmoObject : MonoBehaviour
{
    // Gizmo는 개발 중 시각적 디버깅을 위해 사용한다.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one * 3);
    }
}