using UnityEngine;

public class Thirdpersoncamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float rotationY = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        rotationY += mouseX * mouseSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
