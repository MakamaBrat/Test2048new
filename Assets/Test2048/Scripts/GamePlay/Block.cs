using System;
using System.Security.Cryptography;
using Test2048.Scripts.GamePlay;
using UnityEngine;
using TMPro;
using Zenject;


public class Block : MonoBehaviour
{
    public int Type;
    [SerializeField] private TextMeshProUGUI[] textSides;
    private IBlockFactory _blockFactory;
    private MainValues _mainValues;
    private Rigidbody rb;
    
    public bool hasMerged = false;
    public bool canCollision;
    public delegate void DestroyHandler(Block block);

    public event DestroyHandler OnBlockDestroy;
    public Action OnCollisionSound;
    private void OnDestroy()
    {
       OnBlockDestroy.Invoke(this);
    }

    private void Awake()
    {
        canCollision = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    [Inject]
    public void Construct(IBlockFactory _factory, MainValues mainValues)
    {
        _blockFactory = _factory;
        _mainValues = mainValues;
    }
    

    public void setType(int type)
    {
        Type = type;
        foreach (var textSide in textSides)
        {
            textSide.text = type.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Block>(out Block block))
        {
            if (Type == block.Type && canCollision)
            {
                hasMerged = true;
                if (block.hasMerged == true)
                {
                    
                        Destroy(block.gameObject);
                        _blockFactory.Create(Type * Type, transform.position);
                        _mainValues.SetScore(_mainValues.Score+1);
                        OnCollisionSound?.Invoke();
                        Destroy(gameObject);
                    
                }

               
            }
        }
    }
    


    public Rigidbody GetRigidbody()
    {
        return rb;
    }

}
