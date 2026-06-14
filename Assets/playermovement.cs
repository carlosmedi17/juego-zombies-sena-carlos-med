using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;

        if (moveDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
