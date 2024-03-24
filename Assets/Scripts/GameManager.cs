using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage; //�摜������GameObject
    public Sprite gameOverSpr; //GAME�@OVER�摜
    public Sprite gameClearSpr; //GAME CLEAR�摜
    public GameObject panel; //�p�l��
    public GameObject restartButton; //RESTART�{�^��
    public GameObject nextButton; //NEXT�{�^��

    Image tittleImage; //�摜��\�����Ă���Image�R���|�[�l���g

    public GameObject timeBar; //���ԕ\���C���[�W
    public GameObject timeText; //���ԃe�L�X�g
    TimeController timeCnt; //TimeController

    public GameObject scoreText; //�X�R�A�e�L�X�g
    public static int totalScore; //���v�X�R�A
    public int stageScore = 0; //�X�e�[�W�X�R�A
    // Start is called before the first frame update
    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);

        //�{�^��(�p�l��)���\���ɂ���
        panel.SetActive(false);

        //TimeController���擾
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false); //���Ԑ����Ȃ��Ȃ�B��
            }
        }

        //�X�R�A�ǉ�
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            //�Q�[���N���A
            mainImage.SetActive(true); //�摜��\������
            panel.SetActive(true); //�{�^��(�p�l��)��\������

            //RESTART�{�^���𖳌�������
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr; //�摜��ݒ肷��
            PlayerController.gameState = "gameend";

            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true; //���ԃJ�E���g��~
            }
        }

        else if(PlayerController.gameState == "gameover")
        {
            //�Q�[���I�[�o�[
            mainImage.SetActive(true); //�摜��\������
            panel.SetActive(true); //�{�^��(�p�l��)��\������

            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;  //�摜��ݒ肷��
            PlayerController.gameState = "gameend";

            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; //���ԃJ�E���g��~

                //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10; //�c�莞�Ԃ��X�R�A�ɉ�����
            }

            //�X�R�A�X�V
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
        }

        else if(PlayerController.gameState == "playing")
        {
            //�Q�[����
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //PlayerController���擾����
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            //�^�C�����X�V����
            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                    int time = (int)timeCnt.displayTime;

                    //�^�C���X�V
                    timeText.GetComponent<Text>().text = time.ToString();

                    //�^�C���I�[�o�[
                    if(time == 0)
                    {
                        playerCnt.GameOver(); //�Q�[���I�[�o�[�ɂ���
                    }
                }
            }

            //�X�R�A�X�V
            if(playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
    }

    //�摜���\���ɂ���
    private void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //�X�R�A�ǉ�
    private void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
