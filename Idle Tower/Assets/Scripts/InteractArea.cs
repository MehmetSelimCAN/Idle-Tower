using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
    private int remainingInteractCount;

    [SerializeField] private float requiredTimeToInteract;
    private float remainingTimeToInteract;

    private IInteractable interactable;
    private bool interacted = false;

    [SerializeField] private InteractAreaVisual interactAreaVisual;

    private void Awake()
    {
        remainingTimeToInteract = requiredTimeToInteract;
        interactable = GetComponentInParent<IInteractable>();
        remainingInteractCount = interactable.GetInteractCount();
    }

    private void Interact()
    {
        interactable.Interact();
        interacted = true;

        if (!interactable.IsPermanent())
        {
            remainingInteractCount--;

            if (remainingInteractCount <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                ResetValues();
            }

            return;
        }

        interactAreaVisual.Hide();
    }

    private void ResetValues()
    {
        interacted = false;
        remainingTimeToInteract = requiredTimeToInteract;
        interactAreaVisual.ResetTimerVisual();
        interactAreaVisual.Show();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!interacted)
        {
            if (other.CompareTag("Player"))
            {
                //TODO: Player animasyonu deðiþecek. (Resource toplarken pickaxe sallama gibi.)
                remainingTimeToInteract -= Time.deltaTime;
                interactAreaVisual.UpdateTimerVisual(remainingTimeToInteract, requiredTimeToInteract);

                if (remainingTimeToInteract <= 0)
                {
                    Interact();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetValues();
        }
    }
}
