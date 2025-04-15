using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Rock")
        {
            Debug.Log(collision.gameObject.tag);
            _rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbody.AddForce(Vector3.up * 500 + Vector3.forward * -300, ForceMode.Impulse);
        }
    }
}