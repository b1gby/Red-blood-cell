using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float speed;//Ruby的速度


    private Vector2 lookDirection = new Vector2(1, 0);
    private Animator animator;
    // Start is called before the first frame update
    public PlayerState state;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //玩家输入监听
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        //当前玩家输入的某个轴向值不为0
        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();

        }
        //动画的控制
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //移动
        Vector2 position = transform.position;
        //位置的移动
        position = position + speed * move * Time.deltaTime;
        //transform.position = position;
        rigidbody2d.MovePosition(position);

        //状态转换
        SwitchState();
    }

    void SwitchState()
    {
        if (state == PlayerState.resist)
        {
            Invoke("ResetState", 60);
        }
    }

    void ResetState()
    {
        state = PlayerState.normal;
    }
}