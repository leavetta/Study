using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    // 0.1 - минимальное значение, 1 - максимальное
    [Range(0.1f, 1.0f)] public float verticalMovementDistance = 0.30f;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        /**
        * Проверяем, тэг объекта, активировавшего триггер
        * Если его тэг "Player", то условия выполнено
        **/
        if (collision.gameObject.tag == "Player")
        {
            // Уничтожаем наш объект
            
            Destroy(gameObject);
            //score++;
            //Debug.Log(score);
            //scoreText.text = "Score = " + score.ToString();

        }
    }
    
}
