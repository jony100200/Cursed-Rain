using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] protected Animator _anim;

    [SerializeField] protected float _moveSpeed = 3f;
    [SerializeField] protected Rigidbody2D _rigidbody2D;

    [SerializeField] protected SpriteRenderer _spriteRenderer;

    [Header("Variables")]

    private float min_X = -2.7f;
    private float max_X = 2.7f;

    [Header("Time stuff")]
    public Text timer_Text;
    private int timer;

    // Start is called before the first frame update
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(CountTime());
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        PlayerBounds();
    }

    void PlayerBounds()
    {

        Vector3 temp = transform.position;

        if (temp.x > max_X)
        {

            temp.x = max_X;

        }
        else if (temp.x < min_X)
        {

            temp.x = min_X;
        }

        transform.position = temp;

    }

    protected void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var temp = transform.position;
        if (h > 0)
        {
            temp.x += _moveSpeed * Time.deltaTime;
            _spriteRenderer.flipX = false;
            _anim.SetBool("isRunning", true);
        }
        else if (h < 0)
        {
            temp.x -= _moveSpeed * Time.deltaTime;
            _spriteRenderer.flipX = true;
            _anim.SetBool("isRunning", true);
        }
        else if (h == 0)
        {
            _anim.SetBool("isRunning", false);
            _anim.SetBool("isIdle", true);
        }

        if (v > 0)
        {
            temp.y = _moveSpeed * Time.deltaTime;
            _anim.SetBool("isJumping", true);
        }
        else if (v <= 0)
        {
            _anim.SetBool("isJumping", false);
        }

        transform.position = temp;
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }


    protected void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Knife")
        {
            Time.timeScale = 0f;
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator CountTime()
    {

        yield return new WaitForSeconds(1f);
        timer++;

        timer_Text.text = "Timer: " + timer;

        StartCoroutine(CountTime());
    }
}