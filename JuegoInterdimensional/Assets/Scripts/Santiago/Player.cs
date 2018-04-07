using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Vector3 movement;
    private float horizontalMovement;
    private float verticalMovement;

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        movement = new Vector3(horizontalMovement, verticalMovement, 0f);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, moveSpeed * Time.deltaTime);
    }

}