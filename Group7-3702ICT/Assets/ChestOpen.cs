using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [Tooltip("Assign the opened chest GameObject here")]
    public GameObject openChest;

    void Start()
    {
        if (openChest != null)
        {
            openChest.transform.position = transform.position;
            openChest.transform.rotation = transform.rotation;
            openChest.SetActive(true);
        }

        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }
}
