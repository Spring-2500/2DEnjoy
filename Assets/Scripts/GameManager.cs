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
    // Start is called before the first frame update
    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);

        //�{�^��(�p�l��)���\���ɂ���
        panel.SetActive(false);
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
        }
        
        else if(PlayerController.gameState == "playing")
        {
            //�Q�[����
        }
    }

    //�摜���\���ɂ���
    private void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}