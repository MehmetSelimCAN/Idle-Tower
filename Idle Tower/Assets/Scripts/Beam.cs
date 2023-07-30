using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Beam : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float beamDuration = 0.5f;
    private int damage = 10;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void ActivateBeam(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        StartCoroutine(MoveBeam(startPoint, endPoint));
    }

    private IEnumerator MoveBeam(Vector3 startPoint, Vector3 endPoint)
    {
        float elapsedTime = 0f;

        while (elapsedTime < beamDuration)
        {
            Vector3 currentEndPoint = Vector3.Lerp(startPoint, endPoint, elapsedTime / beamDuration);

            // Check for objects hit by the beam
            RaycastHit[] hits = Physics.RaycastAll(startPoint, currentEndPoint - startPoint, Vector3.Distance(startPoint, currentEndPoint));

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("enemy"))
                {
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage * (1 - elapsedTime / beamDuration));
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.enabled = false;
    }
}
