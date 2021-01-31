using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float speed;//Ruby的速度


    private Vector2 lookDirection = new Vector2(1, 0);
    private Animator animator;
    // Start is called before the first frame update
    public PlayerState state;

    public GameObject Tile;

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

        //检测道具
        DetectTools();

        //状态转换
        SwitchState();
    }

    void SwitchState()
    {
        if (state == PlayerState.resist)
        {
            this.GetComponentInChildren<Text>().text = "Resist";
            Invoke("ResetState", 5);
        }
    }

    void ResetState()
    {
        this.GetComponentInChildren<Text>().text = "";
        state = PlayerState.normal;
    }

    void DetectTools()
    {
        if(UIManager.Instance.gameWnd.ResistUI.transform.GetChild(0).gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UIManager.Instance.gameWnd.ResistUI.transform.GetChild(0).gameObject.SetActive(false);
                state = PlayerState.resist;
            }
        }
        if (UIManager.Instance.gameWnd.ResistUI.transform.GetChild(1).gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UIManager.Instance.gameWnd.ResistUI.transform.GetChild(1).gameObject.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    Transform ranBody = Tile.transform.GetChild((int)Random.Range(0, 6));

                    //如果在lung里
                    if (ranBody.name.Contains("lung"))
                    {
                        ranBody.GetComponentInChildren<Lung>().InsRedCell();
                    }
                    else
                    {
                        ranBody.GetComponentInChildren<NormalBody>().InsRedCell();
                    }
                }
            }
        }
    }
}