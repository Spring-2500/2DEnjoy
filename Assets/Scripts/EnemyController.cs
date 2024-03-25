using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.3f; //移動速度
    public string direction = "left"; //向き
    public float range = 0.0f; //動き回わる範囲
    Vector3 defPos; //初期位置
    // Start is called before the first frame update
    void Start()
    {
        if(direction == "right")
        {
            transform.localScale = new Vector2(-1, 1); //向きの変更
        }

        //初期位置
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(range > 0.0f)
        {
            if(transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1); //向きの変更
            }

            if(transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1); //向きの変更
            }
        }
    }

    private void FixedUpdate()
    {
        //速度を更新する
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        if(direction == "right")
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        }

        else
        {
            rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
        }
    }

    //接触
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1, 1); //向きの変更
        }

        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1); //向きの変更
        }
    }
}
