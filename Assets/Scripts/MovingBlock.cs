using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f; //X移動距離
    public float moveY = 0.0f; //Y移動距離
    public float times = 0.0f; //時間
    public float weight = 0.0f; //停止時間
    public bool isMoveWhenOn = false; //乗った時に動くフラグ

    public bool isCanMove = true; //動くフラグ
    float perDX; //１フレームのX移動値
    float perDY; //1フレームのY移動値
    Vector3 defpos; //初期位置
    bool isReverse = false; //反転フラグ

    // Start is called before the first frame update
    void Start()
    {
        //初期位置
        defpos = transform.position;

        //１フレームの移動時間取得
        float timestep = Time.fixedDeltaTime;
        //1フレームのX移動値
        perDX = moveX / (1.0f / timestep * times);
        //1フレームのY移動値
        perDY = moveY / (1.0f / timestep * times);

        Debug.Log("perDX: " + perDX);
        Debug.Log("perDY: " + perDY);

        if(isMoveWhenOn)
        {
            //乗ったとき動くので最初は動かさない
            isCanMove = false;
        }
    }

    private void FixedUpdate()
    {
        if(isCanMove)
        {
            //移動中
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if(isReverse)
            {
                //逆方向移動中
                //移動量がプラスで移動位置が初期位置より小さい
                //または、移動量がマイナスで初期位置が初期位置より小さい
                if((perDX >= 0.0f && x <= defpos.x) || (perDX < 0.0f && x >= defpos.x))
                {
                    endX = true; //X方向の移動終了
                }

                if((perDY >= 0.0f && y <= defpos.y) || (perDY < 0.0f && y >= defpos.y))
                {
                    endY = true; //Y方向の移動終了
                }

                //床を移動させる
                transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
            }

            else
            {
                //正方向移動中
                //移動量がプラスで初期位置が初期 + 移動距離より大きい
                //または、移動量がマイナスで初期 + 移動距離より小さい
                if((perDX >= 0.0f && x >= defpos.x + moveX) || (perDX < 0.0f && x <= defpos.x + moveX))
                {
                    endX = true; //X方向の移動終了
                }

                if((perDY >= 0.0f && y >= defpos.y + moveY) || (perDY < 0.0f && y <= defpos.y + moveY))
                {
                    endY = true; //Y方向の移動終了
                }

                //床を移動させる
                Vector3 v = new Vector3(perDX, perDY, defpos.z);
                transform.Translate(v);
            }

            if(endX && endY)
            {
                //移動終了
                if(isReverse)
                {
                    //正方向に戻る前に初期位置に戻す、そうしておかないと位置がずれていくため
                    transform.position = defpos;
                }

                isReverse = !isReverse; //フラグを反転させる
                isCanMove = false; //移動フラグを下す
                if (isMoveWhenOn == false)
                {
                    //乗った時に動くフラグOFF
                    Invoke("Move", weight); //移動フラグを立てる遅延実行
                }
            }
        }
    }

    //移動フラグを立てる
    public void Move()
    {
        isCanMove = true;
    }

    //移動フラグを下す
    public void Stop()
    {
        isCanMove = false;
    }

    //接触開始
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //接触したのがプレイヤーなら移動床の子にする
            collision.transform.SetParent(transform);
            if(isMoveWhenOn)
            {
                //乗った時に動くフラグON
                isCanMove = true; //移動フラグを立てる
            }
        }
    }

    //接触終了
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //接触したのがプレイヤーなら移動床の子から外す
            collision.transform.SetParent(null);
        }
    }
}
