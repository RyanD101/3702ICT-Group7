using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectableItem : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Collection Settings")]
    public int points = 1;
    public AudioClip collectSfx;
    public ParticleSystem collectVfx;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        GameManager.Instance.AddScore(points);

        if (collectSfx != null)
            AudioSource.PlayClipAtPoint(collectSfx, transform.position);

        if (collectVfx != null)
            Instantiate(collectVfx, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}