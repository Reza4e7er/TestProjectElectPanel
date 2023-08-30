using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public GameObject[] pickUps;
    public Transform spawnPoint; // A reference to the spawn point in the center of the cube

    private void Start()
    {
        // Initialize your pickUps array here if needed
    }

    private void Update()
    {
        // Iterate through each pickUp in the array
        foreach (GameObject pickUp in pickUps)
        {
            // Check if the pickUp is outside the bounds of the cube's collider
            if (!IsInsideCollider(pickUp.GetComponent<Collider>()))
            {
                // Delete the out-of-bounds pickUp
                

                // Spawn a new pickUp at the spawn point
                SpawnPickUp();
            }
        }
    }

    private bool IsInsideCollider(Collider collider)
    {
        // Check if the collider's bounds intersect with the cube's collider
        return GetComponent<Collider>().bounds.Intersects(collider.bounds);
    }

    private void SpawnPickUp()
    {
        // Spawn a new pickUp at the spawn point's position
        GameObject newPickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)], spawnPoint.position, Quaternion.identity);
        // You can apply any necessary setup for the new pickUp here
    }
}
