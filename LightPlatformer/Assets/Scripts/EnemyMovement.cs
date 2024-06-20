using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variables

    //Essentials
    private CapsuleCollider2D collider2d;
    private Rigidbody2D rigBody2d;
    public LayerMask groundLayer;

    GameObject player;

    //Eye light
    [SerializeField] Transform eyeLight;

    //Movement
    [SerializeField] float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float checkWallDistanceJump = 1.8f;
    [SerializeField] private float overlapDistanceStop = .2f;

    //Trigger distance
    private Vector2 vectorToPlayer;
    public float triggerDistance = 10f;

    //Sprite and direction
    private int direction;
    public SpriteRenderer spriteRenderer;

    //Sounds
    RandomAudioPlayer AudioScript;
    bool triggerEnterSoundPlay = true;
    #endregion

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        rigBody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
        AudioScript = GetComponent<RandomAudioPlayer>();
    }

    void Update()
    {
        vectorToPlayer = player.transform.position - transform.position;
        SetDirection();

        //if distance to player is less than trigger distance
        if (vectorToPlayer.magnitude < triggerDistance)
        {
            //Play sound on trigger zone enter
            if (triggerEnterSoundPlay)
            {
                AudioScript.audioSrc.Play();
                AudioScript.PlayRandomSound(AudioScript.clipList1);
                triggerEnterSoundPlay = false;
            }

            rigBody2d.velocity = new Vector2(direction * movementSpeed, rigBody2d.velocity.y);

            AudioScript.PlayRandomSoundAtRandomTime(AudioScript.clipList1, 2f, 10f);
        }

        //If player is outside of trigger zone, allow sound playing
        if (vectorToPlayer.magnitude > triggerDistance)
        {
            AudioScript.audioSrc.Stop();
            triggerEnterSoundPlay = true;
        }

        //if wall is detected, jump
        if (CheckWall(direction, checkWallDistanceJump))
        {
            Jump();
        }
    }

    //Changes walk direction and flippes sprite + eye light
    private void SetDirection()
    {
        if (vectorToPlayer.x >= 0 + overlapDistanceStop)
        {
            direction = 1;
            spriteRenderer.flipX = false;

            if(eyeLight.localPosition.x < 0)
            {
                eyeLight.localPosition *= new Vector2(-1, 1);
            }
        }
        else if (vectorToPlayer.x <= 0 - overlapDistanceStop)
        {
            direction = -1;
            spriteRenderer.flipX = true;

            if (eyeLight.localPosition.x > 0)
            {
                eyeLight.localPosition *= new Vector2(-1, 1);
            }
        }
        else
        {
            direction = 0;
        }
    }

    private bool CheckWall(int direction, float checkWallDistance)
    {
        if (direction >= 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, checkWallDistance, groundLayer);
            if (hit.collider != null)
            {
                return true;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, checkWallDistance, groundLayer);
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rigBody2d.velocity = new Vector2(rigBody2d.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        Vector2 sizeVector = new Vector2(collider2d.size.x, collider2d.size.y);
        return Physics2D.BoxCast(collider2d.bounds.center, sizeVector, 0f, Vector2.down, .1f, groundLayer);
    }
}