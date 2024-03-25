using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.3f; //�ړ����x
    public string direction = "left"; //����
    public float range = 0.0f; //��������͈�
    Vector3 defPos; //�����ʒu
    // Start is called before the first frame update
    void Start()
    {
        if(direction == "right")
        {
            transform.localScale = new Vector2(-1, 1); //�����̕ύX
        }

        //�����ʒu
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
                transform.localScale = new Vector2(-1, 1); //�����̕ύX
            }

            if(transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1); //�����̕ύX
            }
        }
    }

    private void FixedUpdate()
    {
        //���x���X�V����
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

    //�ڐG
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1, 1); //�����̕ύX
        }

        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1); //�����̕ύX
        }
    }
}
