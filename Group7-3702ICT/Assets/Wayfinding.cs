using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;

public class FootprintPath : MonoBehaviour
{
    public GameObject footprintPrefab;
    public Transform treasureChest;
    public float spacing = 1f;
    public float movementThreshold = 0.5f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public ClueClickPopup clueClickPopup;

    public string treasureTag = "Treasure";
    public string footprintTag = "Footprint";
    public string footprintLayerName = "PhoneVisible";

    private Vector3 lastPlayerPosition;
    private List<GameObject> myFootprints = new List<GameObject>(); // Tracks this instance's footprints

    void Start()
    {
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

    public void ClearExistingFootprints()
    {
        foreach (var footprint in myFootprints)
        {
            if (footprint != null)
            {
                Destroy(footprint);
            }
        }
        myFootprints.Clear();
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
                            if (layer != -1)
                                footprint.layer = layer;
                        }

                        myFootprints.Add(footprint); // Track this footprint
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
