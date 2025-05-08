using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour, IPausable
{
    [SerializeField] private float waitTime = 3.0f;
    [SerializeField] private float elevateSpeed = 5.0f;
    [SerializeField] private float maxDistance = 15f;

    private float travelDistance;
    private Rigidbody rb;

    private Coroutine reverseElevator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (travelDistance >= maxDistance)
        {
            reverseElevator ??= StartCoroutine(ReverseElevate());
        } 
        else
        {
            float distanceStep = elevateSpeed * Time.deltaTime;
            travelDistance += Mathf.Abs(distanceStep);

            Vector3 elevatorPos = rb.position;
            elevatorPos.y += distanceStep;

            rb.MovePosition(elevatorPos);
        }
    }

    public void OnGameStart()
    {
        enabled = true;
        StartCoroutine(StartElevate());
    }

    private IEnumerator StartElevate()
    {
        yield return new WaitForSeconds(waitTime);
        enabled = true;
    }

    private IEnumerator ReverseElevate()
    {
        yield return new WaitForSeconds(waitTime);

        travelDistance = 0;
        elevateSpeed = -elevateSpeed;
        reverseElevator = null;
    }
}
