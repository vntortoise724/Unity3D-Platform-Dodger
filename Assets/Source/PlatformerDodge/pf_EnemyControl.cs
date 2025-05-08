using UnityEngine;

public class pf_EnemyControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float pushRadius;

    private Rigidbody rb;
    private GameObject followTarget;
    private bool isRecharged = true;

    private void Awake()
    {
        AddCircle();

        rb = GetComponent<Rigidbody>();
    }

    private void AddCircle()
    {
        GameObject go = new()
        {
            name = "Circle"
        };
        Vector3 circlePos = Vector3.zero;
        circlePos.y = -.45f;

        go.transform.parent = transform;
        go.transform.localPosition = circlePos;

        go.DrawCircle(pushRadius, .02f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        followTarget = FindFirstObjectByType<pf_PlayerControl>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveTo = (followTarget.transform.position - transform.position);

        moveTo.y = 0;

        rb.AddForce(moveTo.normalized * speed);

        if (Mathf.Abs(moveTo.magnitude) <= pushRadius && isRecharged)
        {
            isRecharged = false;
            rb.AddForce(.5f * speed * moveTo.normalized, ForceMode.Impulse);
            Invoke(nameof(Recharge), 2f);
        }

        if (transform.position.y <= -15)
        {
            Destroy(gameObject);
        }
    }

    private void Recharge()
    {
        isRecharged = true;
    }
}
