using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float minJumpForce = 5f;  // 最小跳跃力度
    public float maxJumpForce = 20f; // 最大跳跃力度
    public float maxPressDuration = 2f; // 最大按压时间，超过此值不再增加跳跃力度

    public AudioClip pressSound; // 按压时的音效
    public AudioClip collisionSound; // 碰撞时的音效
    private AudioSource audioSource; // 用于播放音效的AudioSource组件

    private Vector3 originalScale; // 记录角色原始的缩放比例
    private float pressDuration = 0f;
    private bool isPressing = false;
    private bool isGrounded = false; // 是否检测到碰撞

    private Rigidbody2D rb;

    void Start()
    {
        originalScale = transform.localScale; // 获取角色的初始缩放比例
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // 获取AudioSource组件

        // 锁定Z轴旋转
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 开始按下空格键
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isPressing = true;
            pressDuration = 0f;

            // 播放按压音效
            if (pressSound != null)
            {
                audioSource.clip = pressSound;
                audioSource.loop = true; // 设置为循环播放
                audioSource.Play();
            }
        }

        // 持续按住空格键
        if (isPressing)
        {
            pressDuration += Time.deltaTime;

            // 限制按压时间
            if (pressDuration > maxPressDuration)
            {
                pressDuration = maxPressDuration;
            }

            // 角色模型压缩效果
            float compression = 1 - pressDuration / maxPressDuration * 0.3f; // 压缩比例
            transform.localScale = new Vector3(originalScale.x + (1 - compression) * 0.2f, originalScale.y * compression, originalScale.z);
        }

        // 松开空格键
        if (Input.GetKeyUp(KeyCode.Space) && isPressing)
        {
            isPressing = false;

            // 停止按压音效
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // 根据按压时间计算跳跃力度
            float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, pressDuration / maxPressDuration);
            rb.AddForce(new Vector2(jumpForce, jumpForce), ForceMode2D.Impulse);

            // 恢复角色的原始缩放比例
            transform.localScale = originalScale;
        }
    }

    // 碰撞开始时调用
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        // 播放碰撞音效
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }

        // 锁定水平速度，防止角色滑动
        rb.velocity = Vector2.zero;
    }

    // 碰撞结束时调用
    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
