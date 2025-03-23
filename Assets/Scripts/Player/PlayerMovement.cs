using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Range range = new Range { min = -5f, max = 5f };
    private int lastDirection = 0; // -1: 左, 1: 右, 0: 無入力

    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Transform pos = transform;

        pos.position += new Vector3(horizontal, 0) * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(pos.position.x, range.min, range.max), pos.position.y, pos.position.z);
    }
}
