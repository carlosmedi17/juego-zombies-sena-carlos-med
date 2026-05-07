using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movimiento = transform.forward * vertical + transform.right * horizontal;
        transform.position += movimiento * 25f * Time.deltaTime;
    }
}
