using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class Projectile : MonoBehaviour 
{ 
    [SerializeField] float _InitialVelocity; 
    [SerializeField] float _Angle; 

    GameObject cube;

    private Vector3 targetPosition;
    private bool isMoving;

    private void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTargetPosition();
        }

        if (isMoving)
        {
            // Move the projectile towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _InitialVelocity * Time.deltaTime);

            // Check if the projectile has reached the target position
            if (transform.position == targetPosition)
            {
                // Destroy the projectile
                Destroy(gameObject);
            }
        } 
    }
 
    private void UpdateTargetPosition()
    {
        // Check if the user clicked on an object
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            // Set the target position to the position of the clicked object
            targetPosition = hit.point;

            // Stop all coroutines
            StopAllCoroutines();

            // Start the coroutine to simulate the projectile motion
            StartCoroutine(Coroutine_Movement(_InitialVelocity, _Angle * Mathf.Deg2Rad));
        }
    }

    IEnumerator Coroutine_Movement(float initialVelocity, float angle) 
    { 
        // Time 
        float time = 0; 
 
        // Initial position 
        Vector3 initialPosition = transform.position; 
 
        // Simulate the projectile moving for 100s 
        while (transform.position != targetPosition) 
        { 
            // Implement project motion physics formulas 
            float x = initialPosition.x + 0; 
            float y = initialPosition.y + initialVelocity * time * Mathf.Cos(angle); 
            float z = initialPosition.z + initialVelocity * time * Mathf.Sin(angle) - (1f / 2f) * Physics.gravity.z * Mathf.Pow(time, 2); 
            // Set new position of object 
            transform.position = new Vector3(x, y, z); 
 
            time += Time.deltaTime; 
            yield return null; 
        }

        // Destroy the projectile
        Destroy(cube); 
    } 
}