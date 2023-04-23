using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YatayAtis : MonoBehaviour
{
    public GameObject vektor;
    //
    public Text timeText;
    //
    public Text heightText;
    //
    public GameObject miniCam;
    //
    public Slider angleSlider;
    public Text angleSliderValueText;
    //
    public Slider speedSlider;
    public Text speedSliderValueText;
    
    public Text startButtonText;
    
    public float initialSpeed = 5f; // Başlangıç hızı
    public float launchAngle = 45f; // Atış açısı
    public float gravity = -9.81f; // Yerçekimi kuvveti
    private Rigidbody2D rb;

    private bool _isOnGround;

    private bool _drawVector = true;

    private bool _started = false;

    public void ChangeDrawVector()
    {
        vektor.SetActive(!vektor.activeSelf);
        _drawVector = !_drawVector;
    }
    private void Start()
    {
        DrawLine();
        //
        miniCam.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<Timer>().EndTimer();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _isOnGround = true;
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void Update()
    {
        // Cisim yere düşerse yok et
        if (transform.position.y < -10f) // Yerden -10 birim aşağıya düşerse
        {
            Destroy(gameObject); // Nesneyi yok et
        }
    }

    private void FixedUpdate()
    {
        if (_started)
        {
            timeText.text = "Geçen Zaman: " + FindObjectOfType<Timer>().GetCurrentTime();   
        }
        
        if (_started)
        {
            int indexOfDot = 0;
            if (rb.position.y != 0)
            {
                foreach (var letter in (rb.position.y).ToString().ToCharArray())
                {
                    if (letter == '.')
                    {
                        break;
                    }

                    indexOfDot++;
                }
            }
            heightText.text = "Yükseklik: " + rb.position.y.ToString().Substring(0, indexOfDot + 2);
            try
            {
                
            }
            catch
            {
                
            }
            
        }
        // Yerçekimi kuvveti uygula
        //if (!_isOnGround && _started)rb.AddForce(new Vector2(0f, gravity), ForceMode2D.Force);
    }
    
    public void ChangeStarted()
    {
        _started = !_started;

        if (startButtonText.text == "Sıfırla") startButtonText.text = "Başlat";
        else startButtonText.text = "Sıfırla";
        
        if (startButtonText.text == "Sıfırla")
        {
            FindObjectOfType<Timer>().StartTimer();
            rb.constraints = RigidbodyConstraints2D.None;
            miniCam.SetActive(true);
            rb.gravityScale = 1; // Yerçekimi katsayısı
            float angleRad = launchAngle * Mathf.Deg2Rad;
            float horizontalVelocity = initialSpeed * Mathf.Cos(angleRad);
            float verticalVelocity = initialSpeed * Mathf.Sin(angleRad);
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            if (_drawVector) FindObjectOfType<Cizgi>().DrawPath(angleRad, horizontalVelocity, verticalVelocity);
        }
        else if (startButtonText.text == "Başlat")
        {
            timeText.text = "Geçen Zaman: 0";
            FindObjectOfType<Timer>().RefreshTimer();
            
            heightText.text = "Yükseklik: 4";
            
            miniCam.SetActive(false);
            
            _isOnGround = false;

            transform.position = new Vector3(-2.26999998f, 4, 0f);
            
            angleSlider.value = 45;
            angleSliderValueText.text = "45";
            //
            speedSlider.value = 7;
            speedSliderValueText.text = "7";
            //
            rb.gravityScale = 0f; // Yerçekimi katsayısı
            rb.velocity = new Vector2(0, 0); // İlk hızı uygula
        }
    }

    private void DrawLine()
    {
        if (_drawVector)
        {
            float angleRad = launchAngle * Mathf.Deg2Rad;
            float horizontalVelocity = initialSpeed * Mathf.Cos(angleRad);
            float verticalVelocity = initialSpeed * Mathf.Sin(angleRad);
            FindObjectOfType<Cizgi>().DrawPath(angleRad, horizontalVelocity, verticalVelocity);
        }
    }
    
    public void SetCurrentAngle()
    {
        angleSliderValueText.text = angleSlider.value.ToString();
        launchAngle = angleSlider.value;
        if (startButtonText.text == "Başlat") DrawLine();
    }
    
    public void SetCurrentSpeed()
    {
        speedSliderValueText.text = speedSlider.value.ToString();
        initialSpeed = speedSlider.value;
        if (startButtonText.text == "Başlat") DrawLine();
    }
}
