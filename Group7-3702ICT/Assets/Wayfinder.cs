using UnityEngine;

public class FootprintPath : MonoBehaviour
{
    public GameObject footprintPrefab;      // Your footprint prefab
    public Transform treasureChest;          // The target object (treasure chest)
    public float spacing = 1f;               // Distance between footprints
    public LayerMask groundLayer;            // Layer for your ground/terrain

    void Start()
    {
        SpawnFootprintsAlongPath();
    }

    void SpawnFootprintsAlongPath()
    {
        // Get start position from main camera
        Vector3 startPos = Camera.main.transform.position;
        Vector3 endPos = treasureChest.position;

        // Raycast down from startPos to find ground height
        startPos = GetGroundPosition(startPos);
        endPos = GetGroundPosition(endPos);

        float distance = Vector3.Distance(startPos, endPos);
        int footprintCount = Mathf.FloorToInt(distance / spacing);

        Vector3 direction = (endPos - startPos).normalized;

        for (int i = 0; i <= footprintCount; i++)
        {
            Vector3 spawnPos = startPos + direction * spacing * i;

            // Adjust height to match ground
            spawnPos = GetGroundPosition(spawnPos);

            // 🔧 Raise slightly above ground to prevent clipping
            spawnPos.y += 0.05f;

            // Rotate to lay flat (ensure your prefab faces up in the Z direction)
            Quaternion rotation = Quaternion.Euler(90, 0, 0);

            Instantiate(footprintPrefab, spawnPos, rotation);
        }

    }

    Vector3 GetGroundPosition(Vector3 position)
    {
        RaycastHit hit;
        // Cast ray downward from 10 units above the position
        if (Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, 20f, groundLayer))
        {
            position.y = hit.point.y;
        }
        else
        {
            // If no ground detected, fallback to original y
            Debug.LogWarning("Ground not found under position: " + position);
        }
        return position;
    }
}
