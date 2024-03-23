using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2D;
    float axisH = 0.0f;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(axisH * speed, rb2D.velocity.y);
    }
}
