using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMoveView : MonoBehaviour
{
    public Slider slider;
    public float distance = 0.2f; // 
    public float speed = 2f;

    private bool _isMovingRight = true;
    private float _initialValue;

    public delegate void OnSliderValueHandler(float value);
    public event OnSliderValueHandler OnSliderValueChanged;
    private void Start()
    {
        _initialValue = slider.value;
        slider.gameObject.SetActive(false);
    }

    public void SetSliderMove(bool on)
    {
        if (on)
        {
            StartCoroutine("MoveSlider");
          
        }
        else
        {
            StopCoroutine("MoveSlider");
            
        }
        slider.gameObject.SetActive(on);
    }

    private IEnumerator MoveSlider()
    {
        while (true)
        {
            float targetValue = _isMovingRight ? _initialValue + distance : _initialValue - distance;
            targetValue = Mathf.Clamp01(targetValue);

            while (Mathf.Abs(slider.value - targetValue) > 0.01f)
            {
                slider.value = Mathf.MoveTowards(slider.value, targetValue, speed * Time.deltaTime);
                OnSliderValueChanged?.Invoke(slider.value);
                yield return null;
            }

            _isMovingRight = !_isMovingRight;
        }
    }
}
