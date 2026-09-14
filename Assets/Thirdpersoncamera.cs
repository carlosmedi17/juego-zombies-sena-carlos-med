using UnityEngine;

public class Thirdpersoncamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    float rotationY = 0f;
    float rotationX = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Movimiento horizontal
        rotationY += mouseX * mouseSensitivity * Time.deltaTime;

        //Movimiento vertical
        rotationX -= mouseY * mouseSensitivity * Time.deltaTime;

        //limitar a la camara para que no de la vuelta
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        //Aplicar ambas rotaciones
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
