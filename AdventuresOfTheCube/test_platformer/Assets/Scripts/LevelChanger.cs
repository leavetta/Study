using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    //public int level;
    //public int level;
    public GameObject panel;
    public GameObject player;
    // 0.1 - минимальное значение, 1 - максимальное
    [Range(0.1f, 2.0f)] public float verticalMovementDistance = 0.3f;

    // Изначальное расположение объекта по оси Y 
    private float initialCoinVerticalPosition;
    //public int score = 0;
    //public Text scoreText;

    void Start()
    {
        initialCoinVerticalPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCoinVerticalPosition();
    }
    void CalculateCoinVerticalPosition()
    {
        // Расчет значения Y
        float coinVerticalPosition = Mathf
          .Lerp(
            initialCoinVerticalPosition - (verticalMovementDistance / 2),
            initialCoinVerticalPosition + (verticalMovementDistance / 2),
            Mathf.PingPong(Time.time, 1)
          );

        // Присваиваем новое значение позиции объекта по оси Y 
        transform.position = new Vector3(transform.position.x, coinVerticalPosition, transform.position.z);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(level);
            player.SetActive(false);
            panel.SetActive(true);
        }
    }
}
