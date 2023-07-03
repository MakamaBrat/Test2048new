using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputService : MonoBehaviour
{
    
    public float Distance = 5f;
    public float Speed = 2f;

    private BlockMover _blockMover;
    private LifeCircle _lifeCircle;
    private SliderMoveView _sliderMoveView;
    private bool _isMovingRight = true;
    private Vector3 _initialPosition;
    
    private Coroutine _blockMoving;
    private Block _block;

    [Inject]
    private void Construct(BlockMover blockMover,SliderMoveView sliderMoveView,LifeCircle lifeCircle)
    {
        _blockMover = blockMover;
        _sliderMoveView = sliderMoveView;
        _lifeCircle = lifeCircle;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_block != null)
            {
                ThrowBlock();
            }
        }
    }


    public void SetBlock(Block _block)
    {
        this._block = _block;
        _sliderMoveView.SetSliderMove(true);
        StartCoroutine("blockMoving");
    }
    public void ThrowBlock()
    {
        _sliderMoveView.SetSliderMove(true);
        StopCoroutine("blockMoving");
        _blockMover.MoveBlock(_block.GetRigidbody());
        this._block = null;
        _sliderMoveView.SetSliderMove(false);
        _lifeCircle.MakeCheckForAtLeastOneMovedBlock();

    }
    private IEnumerator blockMoving()
    {
        while (true)
        {
            if (_block == null)
                yield return null;
            
            float targetX = _isMovingRight ? _initialPosition.x + Distance : _initialPosition.x - Distance;
            Vector3 targetPosition = new Vector3(targetX, _block.transform.position.y, _block.transform.position.z);

            while (_block.transform.position != targetPosition)
            {
                _block.transform.position = Vector3.MoveTowards(_block.transform.position, targetPosition, Speed * Time.deltaTime);
                yield return null;
            }

            _isMovingRight = !_isMovingRight;
        }
    }
}
