using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class ProjectileController : MonoBehaviour 
{ 
    [SerializeField] float _InitialVelocity; 
    [SerializeField] float _ZXAngle; 

    [SerializeField] float _ZYAngle; 

    GameObject cube;

    GameObject projectile;

    private Vector3 targetPosition;
    private bool isMoving;

    private void Update()
    {
        // Get the position of the camera
        Vector3 cameraPosition = Camera.main.transform.position;

        // Set the position of the projectile to be directly under the camera
        float distanceBelowCamera = 1; // adjust this value to change the distance below the camera
        Vector3 projectilePosition = new Vector3(cameraPosition.x, cameraPosition.y - distanceBelowCamera, cameraPosition.z);
        transform.position = projectilePosition;

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
                Destroy(projectile);
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
            StartCoroutine(Coroutine_Movement(_InitialVelocity, _ZXAngle * Mathf.Deg2Rad, _ZYAngle * Mathf.Deg2Rad));
        }
    }

    IEnumerator Coroutine_Movement(float initialVelocity, float ZXangle, float ZYangle) 
    { 
        // Time 
        float time = 0; 
 
        // Initial position 
        Vector3 initialPosition = transform.position; 
 
        // Simulate the projectile moving for 100s 
        while (transform.position != targetPosition) 
        { 
            // Implement project motion physics formulas 
        
            float x = initialPosition.x + initialVelocity * time * Mathf.Cos(ZXangle);
            float y = initialPosition.y + initialVelocity * time * Mathf.Cos(ZYangle); 
            float z = initialPosition.z + initialVelocity * time * Mathf.Sin(ZXangle) - (1f / 2f) * Physics.gravity.y * Mathf.Pow(time, 2); 
            // Set new position of object 
            transform.position = new Vector3(x, y, z); 
 
            time += Time.deltaTime; 
            yield return null; 
        }

        // Destroy the target
        Destroy(cube); 
    }     
}