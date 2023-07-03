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
    private Rigidbody rb;
    
    public bool hasMerged = false;
    public delegate void DestroyHandler(Block block);

    public event DestroyHandler OnBlockDestroy;

    private void OnDestroy()
    {
       OnBlockDestroy.Invoke(this);
    }

    private void Awake()
    {
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
    public void Construct(IBlockFactory _factory)
    {
        _blockFactory = _factory;
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
            if (Type == block.Type)
            {
                hasMerged = true;
                if (block.hasMerged == true)
                {
                    
                        Destroy(block.gameObject);
                        _blockFactory.Create(Type * Type, transform.position);

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
