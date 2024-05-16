using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class DinoWalk : MonoBehaviour
{
    
    [SerializeField]public LayerMask groundLayer;
    [SerializeField] public float dinoSpeed;

    int floorObjecID = -1;
    public Vector3 walkPoint;
    public float walkRadious;

    void Start()
    {
        floorObjecID = GetInstanceIDBelow();
        walkPoint = GetRandomPoint(gameObject.transform.position.y, walkRadious);
    }

    // Update is called once per frame
    void Update()
    {
        if(floorObjecID != -1)
        {
            if (IsGroundAtPosition(walkPoint, 2f, floorObjecID))
            {
                // walk to position

                if (walkPoint != gameObject.transform.position)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, walkPoint, dinoSpeed);
                    gameObject.transform.LookAt(walkPoint);
                }

                if (walkPoint == gameObject.transform.position)
                {
                    walkPoint = GetRandomPoint(gameObject.transform.position.y, walkRadious);
                }
            }
            else
            {
                walkPoint = GetRandomPoint(gameObject.transform.position.y, walkRadious);
            }
        }
        
    }

    public Vector3 GetRandomPoint(float y, float radius)
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        // Calculate random x and z offsets within the circle
        float xOffset = radius * Mathf.Cos(randomAngle);
        float zOffset = radius * Mathf.Sin(randomAngle);

        // Create and return the final Vector3
        return new Vector3(xOffset, y, zOffset);
    }

    public bool IsGroundAtPosition(Vector3 position, float maxDistance,int groundID)
    {
        // Adjust 'maxDistance' based on how close to the ground you consider valid

        // Cast a ray downwards from the given position
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, maxDistance, groundLayer))
        {
            // The ray hit something on the ground layer
            if(hit.collider.gameObject.GetInstanceID() == groundID) { 
                //Debug.DrawRay(position, Vector3.down * hit.distance, Color.green); // Visualize (optional)
                return true;
            }
            
        }

        Debug.DrawRay(position, Vector3.down * maxDistance, Color.red); // Visualize (optional)
        return false; // No ground was found within the max distance
    }

    public int GetInstanceIDBelow(float maxDistance = 2.0f)
    {
        RaycastHit hit;

        // Raycast downwards from the object's position
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance, groundLayer))
        {
            // Hit something! Return its Instance ID
            return hit.collider.gameObject.GetInstanceID();
        }

        // Didn't hit anything within the specified distance
        return -1; // Or some other indicator value
    }
}

