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

    public static string gameState = "playing"; //ゲームの状態

    //アニメーション作成
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public int score = 0; //スコア

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを取ってくる
        rb2D = this.GetComponent<Rigidbody2D>();

        //Animetorを取ってくる
        animator = this.GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        //ゲーム中にする
        gameState = "playing"; 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState != "playing")
        {
            return;
        }

        //水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");

        //向きの調整
        if(axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }

        else if(axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);//左右反転させる
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
            //地面の上 or 速度が0ではない
            //速度を更新する
            rb2D.velocity = new Vector2(speed * axisH, rb2D.velocity.y);
        }

        if(onGround && goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Debug.Log("ジャンプ！");
            Vector2 jumpPw = new Vector2(0, jump);
            rb2D.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        if(onGround)
        {
            //地面の上
            if(axisH == 0)
            {
                nowAnime = stopAnime; //停止中
            }
            else
            {
                nowAnime = moveAnime; //移動
            }
        }
        else
        {
            //空中
            nowAnime = jumpAnime;
        }

        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime); //アニメーションを再生
        }
    }

    public void Jump()
    {
        goJump = true; //ジャンプフラグを立てる
        Debug.Log("ジャンプボタン押し！");
    }

    //接触開始
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
            //スコアアイテム
            //ItemDataを作る
            ItemData item = collision.gameObject.GetComponent<ItemData>();

            //スコアを得る
            score = item.value;

            //アイテムを削除する
            Destroy(collision.gameObject);
        }
    }

    //ゴール
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop(); //ゲーム停止
    }

    //ゲームオーバー
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop(); //ゲーム停止
        // =====================
        //ゲームオーバー演出
        //======================
        //プレイヤーの当たり判定を消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        //プレイヤーを上に少し跳ね上げる演出
        rb2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    private void GameStop()
    {
        //Rigidbody2Dを取ってくる
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        //速度を0にして強制終了
        rb2D.velocity = new Vector2(0, 0);
    }
}
