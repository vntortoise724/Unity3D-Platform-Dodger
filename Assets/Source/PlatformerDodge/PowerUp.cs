using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatDistance;

    private Vector3 floatTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floatTarget = transform.position + Vector3.up * floatDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(60 * Time.deltaTime * Vector3.up);
        
        if (transform.position.y == floatTarget.y)
        {
            floatDistance = -floatDistance;
            floatTarget = transform.position + Vector3.up * floatDistance;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, floatTarget, floatSpeed * Time.deltaTime);

    }
}
