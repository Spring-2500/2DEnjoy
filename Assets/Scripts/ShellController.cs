using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; //削除する時間設定
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime); //削除設定
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //何かに接触したら消す
    }
}
