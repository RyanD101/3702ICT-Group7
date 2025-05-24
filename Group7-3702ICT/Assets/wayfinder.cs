using UnityEngine;
using UnityEngine.AI;

public class FootprintPath : MonoBehaviour
{
    public GameObject footprintPrefab;      // Your footprint prefab
    public Transform treasureChest;         // The target object (treasure chest)
    public float spacing = 1f;              // Distance between footprints
    public float movementThreshold = 0.5f;  // Distance player must move to update
    public LayerMask groundLayer;           // Layer for ground/terrain
    public LayerMask obstacleLayer;         // Layer for obstacles/walls

    public string treasureTag = "Treasure";         // Optional fallback tag
    public string footprintTag = "Footprint";       // Tag assigned to footprints
    public string footprintLayerName = "PhoneVisible"; // Optional: layer for camera filtering

    private Vector3 lastPlayerPosition;

    void Start()
    {
        // Fallback: find treasure by tag if not assigned
        if (treasureChest == null)
        {
            GameObject found = GameObject.FindWithTag(treasureTag);
            if (found != null)
            {
                treasureChest = found.transform;
            }
            else
            {
                Debug.LogError("TreasureChest not assigned and not found by tag.");
                return;
            }
        }

        lastPlayerPosition = Camera.main.transform.position;
        SpawnFootprintsAlongNavMeshPath();
    }

    void Update()
    {
        if (treasureChest == null)
        {
            Debug.LogWarning("TreasureChest lost reference!");
            return;
        }

        float moved = Vector3.Distance(Camera.main.transform.position, lastPlayerPosition);
        if (moved > movementThreshold)
        {
            lastPlayerPosition = Camera.main.transform.position;
            ClearExistingFootprints();
            SpawnFootprintsAlongNavMeshPath();
        }
    }

    void ClearExistingFootprints()
    {
        GameObject[] footprints = GameObject.FindGameObjectsWithTag(footprintTag);
        foreach (var footprint in footprints)
        {
            Destroy(footprint);
        }
    }

    void SpawnFootprintsAlongNavMeshPath()
    {
        Vector3 startPos = Camera.main.transform.position;
        Vector3 endPos = treasureChest.position;

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, path))
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Vector3 segmentStart = path.corners[i];
                Vector3 segmentEnd = path.corners[i + 1];
                Vector3 dir = (segmentEnd - segmentStart).normalized;
                float segmentDistance = Vector3.Distance(segmentStart, segmentEnd);
                int stepCount = Mathf.FloorToInt(segmentDistance / spacing);

                for (int j = 0; j <= stepCount; j++)
                {
                    Vector3 pos = segmentStart + dir * spacing * j;
                    pos = GetGroundPosition(pos);
                    pos.y += 0.2f;

                    if (!Physics.CheckSphere(pos, 0.3f, obstacleLayer))
                    {
                        Quaternion rotation = Quaternion.LookRotation(dir);
                        rotation *= Quaternion.Euler(90, 0, 0); // Lay flat
                        GameObject footprint = Instantiate(footprintPrefab, pos, rotation);
                        footprint.tag = footprintTag;

                        if (footprintLayerName != "")
                        {
                            int layer = LayerMask.NameToLayer(footprintLayerName);
                            if (layer != -1) footprint.layer = layer;
                        }
                    }
                    else
                    {
                        Debug.Log("Skipping footprint due to obstacle at: " + pos);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Failed to calculate NavMesh path.");
        }
    }

    Vector3 GetGroundPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, 20f, groundLayer))
        {
            position.y = hit.point.y;
        }
        else
        {
            Debug.LogWarning("Ground not found under position: " + position);
        }
        return position;
    }
}
