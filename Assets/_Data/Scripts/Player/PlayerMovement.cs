using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected float moveHorizontal;
    [SerializeField] protected float moveVertical;

    [SerializeField] protected bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       this.PlayerMove();
        this.PlayerJump();
    }

    protected virtual void PlayerMove()
    {
         moveHorizontal = Input.GetAxis("Horizontal");
         moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.parent.position + movement);
    }

    protected virtual void PlayerJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}