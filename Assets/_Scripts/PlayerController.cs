using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xShift, zShift;
    private float movementSpeed = 5f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        HandleInput();
        Move();
    }

    private void HandleInput()
    {
        xShift = Input.GetAxis("Horizontal");
        zShift = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        velocity = new Vector3(xShift, 0, zShift) * movementSpeed * Time.deltaTime;
        transform.position += velocity;
    }
}
