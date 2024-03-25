using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f; //X�ړ�����
    public float moveY = 0.0f; //Y�ړ�����
    public float times = 0.0f; //����
    public float weight = 0.0f; //��~����
    public bool isMoveWhenOn = false; //��������ɓ����t���O

    public bool isCanMove = true; //�����t���O
    float perDX; //�P�t���[����X�ړ��l
    float perDY; //1�t���[����Y�ړ��l
    Vector3 defpos; //�����ʒu
    bool isReverse = false; //���]�t���O

    // Start is called before the first frame update
    void Start()
    {
        //�����ʒu
        defpos = transform.position;

        //�P�t���[���̈ړ����Ԏ擾
        float timestep = Time.fixedDeltaTime;
        //1�t���[����X�ړ��l
        perDX = moveX / (1.0f / timestep * times);
        //1�t���[����Y�ړ��l
        perDY = moveY / (1.0f / timestep * times);

        Debug.Log("perDX: " + perDX);
        Debug.Log("perDY: " + perDY);

        if(isMoveWhenOn)
        {
            //������Ƃ������̂ōŏ��͓������Ȃ�
            isCanMove = false;
        }
    }

    private void FixedUpdate()
    {
        if(isCanMove)
        {
            //�ړ���
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if(isReverse)
            {
                //�t�����ړ���
                //�ړ��ʂ��v���X�ňړ��ʒu�������ʒu��菬����
                //�܂��́A�ړ��ʂ��}�C�i�X�ŏ����ʒu�������ʒu��菬����
                if((perDX >= 0.0f && x <= defpos.x) || (perDX < 0.0f && x >= defpos.x))
                {
                    endX = true; //X�����̈ړ��I��
                }

                if((perDY >= 0.0f && y <= defpos.y) || (perDY < 0.0f && y >= defpos.y))
                {
                    endY = true; //Y�����̈ړ��I��
                }

                //�����ړ�������
                transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
            }

            else
            {
                //�������ړ���
                //�ړ��ʂ��v���X�ŏ����ʒu������ + �ړ��������傫��
                //�܂��́A�ړ��ʂ��}�C�i�X�ŏ��� + �ړ�������菬����
                if((perDX >= 0.0f && x >= defpos.x + moveX) || (perDX < 0.0f && x <= defpos.x + moveX))
                {
                    endX = true; //X�����̈ړ��I��
                }

                if((perDY >= 0.0f && y >= defpos.y + moveY) || (perDY < 0.0f && y <= defpos.y + moveY))
                {
                    endY = true; //Y�����̈ړ��I��
                }

                //�����ړ�������
                Vector3 v = new Vector3(perDX, perDY, defpos.z);
                transform.Translate(v);
            }

            if(endX && endY)
            {
                //�ړ��I��
                if(isReverse)
                {
                    //�������ɖ߂�O�ɏ����ʒu�ɖ߂��A�������Ă����Ȃ��ƈʒu������Ă�������
                    transform.position = defpos;
                }

                isReverse = !isReverse; //�t���O�𔽓]������
                isCanMove = false; //�ړ��t���O������
                if (isMoveWhenOn == false)
                {
                    //��������ɓ����t���OOFF
                    Invoke("Move", weight); //�ړ��t���O�𗧂Ă�x�����s
                }
            }
        }
    }

    //�ړ��t���O�𗧂Ă�
    public void Move()
    {
        isCanMove = true;
    }

    //�ړ��t���O������
    public void Stop()
    {
        isCanMove = false;
    }

    //�ڐG�J�n
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�ڐG�����̂��v���C���[�Ȃ�ړ����̎q�ɂ���
            collision.transform.SetParent(transform);
            if(isMoveWhenOn)
            {
                //��������ɓ����t���OON
                isCanMove = true; //�ړ��t���O�𗧂Ă�
            }
        }
    }

    //�ڐG�I��
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�ڐG�����̂��v���C���[�Ȃ�ړ����̎q����O��
            collision.transform.SetParent(null);
        }
    }
}
