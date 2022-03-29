using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLauncher : MonoBehaviour
{
    [SerializeField] float forceMultiplier = 1;
    [SerializeField] float minimumForce = 10;
    [SerializeField] float maxPullDistance = 2;
    [SerializeField] float worldDistancePerMouseDistance = 1;
    [SerializeField] PlayerController player;
    [SerializeField] Transform playerHolder;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float lineLength = 1f;
    [SerializeField] float lineSpeedAdjuster = 0.2f;

    private bool launcherActive;
    private Camera camera;
    private float baseCameraOrtSize;


    void Start()
    {
        PutPlayerIntoLauncher();
        camera = Camera.main;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PutPlayerIntoLauncher();
        }

        if (!launcherActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.enabled = true;
        }

            if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;


            Vector3 direction = (mousePos - playerHolder.position).normalized;
            float distance = Vector3.Distance(mousePos, playerHolder.position) * worldDistancePerMouseDistance;
            Vector3 clampedPosition = Vector3.ClampMagnitude(direction * distance, maxPullDistance);
            player.transform.position = clampedPosition + playerHolder.position;

            float seconsToPredict = lineLength * (clampedPosition.magnitude / maxPullDistance);
            float step = 0.1f;
            int numberOfSteps = (int)(seconsToPredict / step);

            lineRenderer.positionCount = numberOfSteps;
            Vector3[] linePositions = new Vector3[numberOfSteps];
            for(int i = 0; i < numberOfSteps; i++)
            {
                Vector3 force = ((playerHolder.position - player.transform.position)
                * Vector2.Distance(playerHolder.position, player.transform.position) * forceMultiplier);
                linePositions[i] = GetPositionInTime(step * i, player.transform.position, force * lineSpeedAdjuster);
            }
            lineRenderer.SetPositions(linePositions);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
            launcherActive = false;
            player.EnableRigidBody();

            Vector3 force = ((playerHolder.position - player.transform.position)
                * Vector2.Distance(playerHolder.position, player.transform.position) * forceMultiplier);

            player.AddForce(force);

            GameController.Instance.OnBirdLaunched();
        }
    }

    public void PutPlayerIntoLauncher()
    {
        player.DisableRigidBody();
        player.transform.position = playerHolder.position;

        launcherActive = true;
    }

    private Vector2 GetPositionInTime(float time, Vector2 initialPosition, Vector2 initialSpeed)
    {
        return initialPosition + new Vector2(initialSpeed.x * time, initialSpeed.y * time - 4.905f * 3 * (time * time));
    }
}
