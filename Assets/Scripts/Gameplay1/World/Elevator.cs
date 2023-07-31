using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;
    private float moveSpeed = 10f;
    private Transform elevatorHeight;
    private bool isPlayerOnElevator = false;
    private Vector3 targetPosition;

    private void Start()
    {
        elevatorHeight = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnElevator = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnElevator = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerOnElevator)
        {
            if (elevatorHeight.position.y < maxHeight)
            {
                MoveElevator(Vector3.up);
            }
        }
        else
        {
            if (elevatorHeight.position.y > minHeight)
            {
                MoveElevator(Vector3.down);
            }
        }
    }

    private void MoveElevator(Vector3 direction)
    {
        targetPosition = elevatorHeight.position + direction;
        elevatorHeight.position = Vector3.MoveTowards(elevatorHeight.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}