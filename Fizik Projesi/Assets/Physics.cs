using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Physics : MonoBehaviour
{
    public Text timeText;
    //
    public Slider heightSlider;
    public Text heightSliderValueText;
    //
    public Slider weightSlider;
    public Text weightSliderValueText;
    //
    public Text startButtonText;
    //
    public Text velocityText;
    public Text positionYText;
    public float initialVelocity = 0f; // İlk hız
    private Rigidbody2D rb;
    //
    public bool started;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<Timer>().EndTimer();
        }
    }

    public void ChangeStarted()
    {
        if (startButtonText.text == "Sıfırla") startButtonText.text = "Başlat";
        else startButtonText.text = "Sıfırla";
        
        if (startButtonText.text == "Sıfırla")
        {
            FindObjectOfType<Timer>().StartTimer();
            transform.position = new Vector3(transform.position.x, heightSlider.value, transform.position.z);
            rb.gravityScale = weightSlider.value; // Yerçekimi katsayısı
            rb.velocity = new Vector2(0, initialVelocity); // İlk hızı uygula
        }
        else if (startButtonText.text == "Başlat")
        {
            FindObjectOfType<Timer>().RefreshTimer();
            //
            timeText.text = "Geçen Zaman: " + 0;
            //
            velocityText.text = "Düşüş Hızı: 0";
            //
            positionYText.text = "Yükseklik: 0";
            //
            weightSlider.value = 1;
            weightSliderValueText.text = "1";
            //
            heightSlider.value = 7;
            heightSliderValueText.text = "7";
            //
            transform.position = new Vector3(transform.position.x, 7, transform.position.z);
            rb.gravityScale = 0f; // Yerçekimi katsayısı
            rb.velocity = new Vector2(0, 0); // İlk hızı uygula
        }
        
        started = !started;
    }

    private void FixedUpdate()
    {
        if (started)
        {
            timeText.text = "Geçen Zaman: " + FindObjectOfType<Timer>().GetCurrentTime();   
        }
    }

    private void Update()
    {
        if (!started)
        {
            return;
        }
        
        
        VelocityCount();
        SetHeight();
        
        if (transform.position.y < -10f) // Yerden -10 birim aşağıya düşerse
        {
            //Destroy(gameObject); // Nesneyi yok et
        }
    }

    private void SetHeight()
    {
        int indexOfDot = 0;
        foreach (var letter in (rb.position.y).ToString().ToCharArray())
        {
            if (letter == '.')
            {
                break;
            }

            indexOfDot++;
        }

        if (rb.position.y.ToString().Length > 3)positionYText.text = "Yükseklik: " + (rb.position.y).ToString().Substring(0, indexOfDot + 2);
    }

    private void VelocityCount()
    {
        int indexOfDot = 0;
        if (rb.velocity.y * -1 != 0)
        {
            foreach (var letter in (rb.velocity.y * -1).ToString().ToCharArray())
            {
                if (letter == '.')
                {
                    break;
                }

                indexOfDot++;
            }
        }

        if (indexOfDot != 0)
            velocityText.text = "Düşüş Hızı: " + (rb.velocity.y * -1).ToString().Substring(0, indexOfDot + 2);
    }

    public void SetCurrentHeight()
    {
        heightSliderValueText.text = heightSlider.value.ToString();
        if (startButtonText.text == "Başlat")transform.position = new Vector3(transform.position.x, heightSlider.value, transform.position.z);
    }
    
    public void SetCurrentWeight()
    {
        weightSliderValueText.text = weightSlider.value.ToString();
    }
}
