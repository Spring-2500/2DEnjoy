using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f; //�����������m����
    public bool isDelete = false; //������ɍ폜����t���O

    bool isFell = false; //�����t���O
    float fadeTime = 0.5f; //�t�F�[�h�A�E�g����
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2d�̕����������~
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�v���C���[��T��
        if(player != null)
        {
            //�v���C���[�Ƃ̋����v�Z
            float d = Vector2.Distance(transform.position, player.transform.position);
            
            if(length >= d)
            {
                Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
                if(rb2D.bodyType == RigidbodyType2D.Static)
                {
                    //Rigidbody2D�̕����������J�n
                    rb2D.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }

        if(isFell)
        {
            //��������
            //�����l��ύX���ăt�F�[�h�A�E�g������
            fadeTime -= Time.deltaTime; //�O�t���[���̍����b�}�C�i�X
            Color col = GetComponent<SpriteRenderer>().color; //�J���[�����o��
            col.a = fadeTime; //�����l��ǉ�
            GetComponent<SpriteRenderer>().color = col; //�J���[���Đݒ肷��
            
            if(fadeTime <= 0.0f)
            {
                //0�ȉ��i�����j�ɂȂ��������
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDelete)
        {
            isFell = true; //�����t���O�I��
        }
    }
}
