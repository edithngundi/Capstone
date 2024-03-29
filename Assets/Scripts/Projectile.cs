using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class Projectile : MonoBehaviour 
{ 
    // Add a variable to store the initial velocity of the projectile
    [SerializeField] float initialVelocity; 
    // Reference to the projectile
    private GameObject projectile;
    // Reference to the target position
    private Vector3 targetPosition;
    // Reference to the projectile position
    private Vector3 projectilePosition;
    // Reference to the direction of the projectile
    private Vector3 direction;
    // Reference to the isMoving variable
    private bool isMoving;
    // Reference to the normalizer variable
    private float normalizer;
    // Add a variable to store the burst sound
    public AudioClip burstSound;
    // Add a variable to store the volume of the sound
    private float volume = 1.0f;

    private void Update()
    {
        // Get the position of the camera
        Vector3 cameraPosition = Camera.main.transform.position;
        //  Set the position of the projectile to be under the camera
        transform.position = projectilePosition;
        // Set the position of the projectile to be under the camera
        projectilePosition = new Vector3(cameraPosition.x, cameraPosition.y - 4, cameraPosition.z);

        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Update the target position
            UpdateTargetPosition();
        }

        if (isMoving)
        {
            // Move the projectile towards the target position
            projectilePosition = Vector3.MoveTowards(transform.position, targetPosition, initialVelocity * Time.deltaTime);
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
            // Set the direction of the projectile
            direction.x = targetPosition.x - projectilePosition.x;
            direction.y = targetPosition.y - projectilePosition.y;
            direction.z = targetPosition.z - projectilePosition.z;
            // Set the normalizer to the magnitude of the direction vector
            normalizer = Mathf.Sqrt(Mathf.Pow(direction.x, 2)+ Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
            // Stop all coroutines
            StopAllCoroutines();
            // Start the coroutine to simulate the projectile motion
            StartCoroutine(Coroutine_Movement(initialVelocity));
        }
    }

    IEnumerator Coroutine_Movement(float initialVelocity) 
    { 
        // In a coroutine, time is used to simulate motion
        // Initialize time to 0 because we want to simulate motion from the moment the projectile is fired
        float time = 0;
 
        // Initial position 
        Vector3 initialPosition = projectilePosition;
 
        // Simulate the projectile moving for 100s 
        while (projectilePosition.z < targetPosition.z) 
        { 
            // Implement project motion physics formulas 
            float x = initialPosition.x + initialVelocity * time * direction.x/normalizer; 
            float y = initialPosition.y + initialVelocity * time * direction.y/normalizer; 
            float z = initialPosition.z + initialVelocity * time * direction.z/normalizer; 
            // Set new position of object 
            transform.position = new Vector3(x, y, z); 
            // time is incremented by Time.deltaTime in each frame to simulate the projectile's motion
            time += Time.deltaTime; 
            // Check if the projectile has hit any objects with the 'Moving Obstacle' tag
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Moving Obstacle"))
                {
                    // Play the explosion sound
                    AudioSource.PlayClipAtPoint(burstSound, transform.position, volume);
                    // Destroy the hit object
                    Destroy(hitCollider.gameObject);

                    // Instantiate a new projectile at the initial position of the previous projectile
                    GameObject newProjectile = Instantiate(projectile, projectilePosition, Quaternion.identity);
                    
                    // Destroy the previous projectile
                    Destroy(gameObject);
                }
            }
            // Yield null to wait for the next frame
            yield return null; 
        }
    } 
}