using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class pf_PlayerControl : MonoBehaviour, IPausable
{
    [SerializeField] private ObjectStats status;
    [SerializeField] private Camera followCamera;
    [SerializeField] private UnityEvent OnPlayerLost;

    private float runSpeed;
    private Rigidbody rb;
    private GameObject elevator;
    private float elevatorOffsetY;
    private Vector3 cameraPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        elevatorOffsetY = 0;
        runSpeed = 1;

        cameraPos = followCamera.transform.position - rb.position;

        enabled = false;
    }

    private void Update()
    {
        if (transform.position.y <= -15)
        {
            OnPlayerLost.Invoke();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 playPos = rb.position;
        Vector3 movement = new Vector3 (horizontalInput, 0, verticalInput).normalized;

        if (movement == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(movement);

        //movement.Normalize();

        if (elevator != null)
        {
            playPos.y = elevator.transform.position.y + elevatorOffsetY;
        }

        //When dot product between movement and forward vector of the poject = -1 
        /*if (Mathf.Approximately(Vector3.Dot(movement, Vector3.forward), -1.0f))
        {
            targetRotation = Quaternion.LookRotation(Vector3.back);
        }
        */

        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);

        //transform.Translate (movement * status.speed *  Time.deltaTime);
        rb.MovePosition(rb.position + runSpeed * status.speed * Time.fixedDeltaTime * movement);

        rb.MoveRotation(targetRotation);
    }

    private void LateUpdate()
    {
        followCamera.transform.position = rb.position + cameraPos;
    }

    public void OnGameStart()
    {
        enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
            runSpeed = 2f;
            StartCoroutine(SpeedCountDown());
        }

        if (collision.gameObject.CompareTag("Enemy") && runSpeed > 1)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * 75f, ForceMode.Impulse);
        }
    }

    private IEnumerator SpeedCountDown()
    {
        yield return new WaitForSeconds(5f);
        runSpeed = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            elevator = other.gameObject;
            elevatorOffsetY = transform.position.y - elevator.transform.position.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            elevator = null;
            elevatorOffsetY = 0;
        }
    }
}
