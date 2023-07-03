using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BlockMover : MonoBehaviour
{
    [SerializeField] private float _power;
    private InputService _inputService;
    private SliderMoveView _sliderMoveView;
    public Action OnBlockMove;
    [Inject]
    void Construct(InputService inputService, SliderMoveView sliderMoveView,SoundController soundController)
    {
        _inputService = inputService;
        _sliderMoveView = sliderMoveView;
        sliderMoveView.OnSliderValueChanged += changePower;
        OnBlockMove += soundController.PlayPushSound;
    }
    

 
    public void MoveBlock(Rigidbody movedBlock) 
    {
        movedBlock.isKinematic = false;
        movedBlock.useGravity = true;
        movedBlock.AddForce(movedBlock.transform.forward+new Vector3(0,0,500)*_power);
        OnBlockMove?.Invoke();
    }

    private void changePower(float power)
    {
        _power = power*5;
    }
  
}
