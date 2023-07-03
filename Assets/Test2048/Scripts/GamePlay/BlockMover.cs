using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlockMover : MonoBehaviour
{
    [SerializeField] private float _power;
    private InputService _inputService;
    private SliderMoveView _sliderMoveView;
    
    [Inject]
    void Construct(InputService inputService, SliderMoveView sliderMoveView)
    {
        _inputService = inputService;
        _sliderMoveView = sliderMoveView;
        sliderMoveView.OnSliderValueChanged += changePower;
    }
    

 
    public void MoveBlock(Rigidbody movedBlock) 
    {
        movedBlock.isKinematic = false;
        movedBlock.useGravity = true;
        movedBlock.AddForce(movedBlock.transform.forward+new Vector3(0,0,250)*_power);
    }

    private void changePower(float power)
    {
        _power = power*10;
    }
  
}
