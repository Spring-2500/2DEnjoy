using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2D;
    float axisH = 0.0f;
    public float speed = 3.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    public static string gameState = "playing"; //�Q�[���̏��

    //�A�j���[�V�����쐬
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public int score = 0; //�X�R�A

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D������Ă���
        rb2D = this.GetComponent<Rigidbody2D>();

        //Animetor������Ă���
        animator = this.GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        //�Q�[�����ɂ���
        gameState = "playing"; 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState != "playing")
        {
            return;
        }

        //���������̓��͂��`�F�b�N����
        axisH = Input.GetAxisRaw("Horizontal");

        //�����̒���
        if(axisH > 0.0f)
        {
            //�E�ړ�
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        }

        else if(axisH < 0.0f)
        {
            //���ړ�
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1);//���E���]������
        }

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }

        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)
        {
            //�n�ʂ̏� or ���x��0�ł͂Ȃ�
            //���x���X�V����
            rb2D.velocity = new Vector2(speed * axisH, rb2D.velocity.y);
        }

        if(onGround && goJump)
        {
            //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
            //�W�����v������
            Debug.Log("�W�����v�I");
            Vector2 jumpPw = new Vector2(0, jump);
            rb2D.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        if(onGround)
        {
            //�n�ʂ̏�
            if(axisH == 0)
            {
                nowAnime = stopAnime; //��~��
            }
            else
            {
                nowAnime = moveAnime; //�ړ�
            }
        }
        else
        {
            //��
            nowAnime = jumpAnime;
        }

        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime); //�A�j���[�V�������Đ�
        }
    }

    public void Jump()
    {
        goJump = true; //�W�����v�t���O�𗧂Ă�
        Debug.Log("�W�����v�{�^�������I");
    }

    //�ڐG�J�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            Goal();
        }

        else if(collision.gameObject.CompareTag("Dead"))
        {
            GameOver();
        }

        else if(collision.gameObject.CompareTag("ScoreItem"))
        {
            //�X�R�A�A�C�e��
            //ItemData�����
            ItemData item = collision.gameObject.GetComponent<ItemData>();

            //�X�R�A�𓾂�
            score = item.value;

            //�A�C�e�����폜����
            Destroy(collision.gameObject);
        }
    }

    //�S�[��
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop(); //�Q�[����~
    }

    //�Q�[���I�[�o�[
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop(); //�Q�[����~
        // =====================
        //�Q�[���I�[�o�[���o
        //======================
        //�v���C���[�̓����蔻�������
        GetComponent<CapsuleCollider2D>().enabled = false;
        //�v���C���[����ɏ������ˏグ�鉉�o
        rb2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    private void GameStop()
    {
        //Rigidbody2D������Ă���
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        //���x��0�ɂ��ċ����I��
        rb2D.velocity = new Vector2(0, 0);
    }
}
