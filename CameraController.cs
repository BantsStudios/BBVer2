using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BBMovement BB;
    private Vector3 targetPoint = Vector3.zero;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookAheadDistance;
    [SerializeField] private float lookAheadSpeed;
    private float lookOffset;
    private bool isFalling;
    public float maxVertOffset = 5f;

    private void Start()
    {
        targetPoint = new Vector3(BB.transform.position.x, BB.transform.position.y, -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPoint.x = BB.transform.position.x;
        //targetPoint.y = BB.transform.position.y;

        if (BB.IsGrounded())
        {
            targetPoint.y = BB.transform.position.y;
        }

        if (transform.position.y - BB.transform.position.y > maxVertOffset)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            targetPoint.y = BB.transform.position.y;

            if (BB.IsGrounded()) 
            {
                isFalling = false;
            }
        }
        /*if (targetPoint.y < 0)
        {
            targetPoint.y = 0;
        }*/

        if (BB.rb.velocity.x > 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        else if (BB.rb.velocity.x < 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime); ;
        }

        targetPoint.x = BB.transform.position.x + lookOffset;
        
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }
}
