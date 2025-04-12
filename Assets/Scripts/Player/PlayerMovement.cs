using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Range range = new Range { min = -5f, max = 5f };
    bool isStartGameOverProc;
    Rigidbody2D rb;
    [SerializeField] float power;
    [SerializeField] float rotatespeed;
    [SerializeField] SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameSystem.Instance.IsGameOver == true)
        {
            transform.Rotate(0, 0, rotatespeed * Time.deltaTime);
            if (isStartGameOverProc == false) Jump();
            if (transform.position.y > 5f) Destroy(gameObject);
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal > 0) sprite.flipX = true;
        else if(horizontal < 0) sprite.flipX = false;
        Transform pos = transform;

        pos.position += new Vector3(horizontal, 0) * PlayerStatusSO.MoveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(pos.position.x, range.min, range.max), pos.position.y, pos.position.z);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        isStartGameOverProc = true;
    }
}
