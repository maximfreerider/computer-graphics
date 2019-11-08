using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{                                    
    public static int Lives = 1;
    public float gridSize = 1;
    private Rigidbody _rigidbody;

    private int rangeBound = 25;
    private const int TailLength = 3; 
    private const int RotationAngle = 90;
    private GameObject _fruit, _frog, _nest, _tail;
    private Timer _timer;
    private bool _canRotate;
    private Vector3 _direction;
    private List<GameObject> _tailParts;
    private List<Vector3> _vectors;
    
    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _direction = _rigidbody.transform.forward;
        _timer = gameObject.GetComponent<Timer>();  
        _timer.Tick += OnTick;

        _vectors = new List<Vector3>();
        _tailParts = new List<GameObject>();
        
        for (var i = 0; i < TailLength; i++)
        {
            var tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tail.transform.localPosition = new Vector3(0, 0, -2);
            Destroy(tail.GetComponent<Collider>());
            _tailParts.Add(tail);
        }
    }
    
    private void OnTick()
    {
        _rigidbody.transform.localPosition += _direction * gridSize;
        _canRotate = true;
        _vectors.Add(_rigidbody.transform.localPosition);
        
        if(_vectors.Count-1 > _tailParts.Count)
            _vectors.RemoveAt(0);

        for (int i = 0; i < _vectors.Count-1; i++)
            _tailParts[i].transform.localPosition = _vectors[i];
        
    }

    public void Update()
    {
        TryRotate();
    }
    
    void TryRotate()
    {
        TryRotateSnake(KeyCode.RightArrow, 1);
        TryRotateSnake(KeyCode.LeftArrow, -1);
    }

    private void TryRotateSnake(KeyCode key, int rotationDirection)
    {
        if (Input.GetKeyDown(key) && _canRotate)
        {
            _direction = Quaternion.AngleAxis(RotationAngle * rotationDirection, Vector3.up) * _direction;
            _canRotate = false;
        }
    }

    void EatFood(string foodName)
    {
        Game.Points++;
        var food = GameObject.Find(foodName);
        var foodPosition = new Vector3(Random.Range(-rangeBound, rangeBound), 0, Random.Range(-rangeBound, rangeBound));
        food.transform.position = foodPosition;
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("on Collision");
        if(collision.gameObject.name == "Wall" || collision.gameObject.name == "Obstacle")  
        {
            Lives -= 1;
            Debug.Log("Lives -- " + Lives);
            if (Lives == 0)
                SceneManager.LoadScene("GameOver");
        }
        
        if (collision.gameObject.name == "Fruit" || collision.gameObject.name == "Frog")
            EatFood(collision.gameObject.name);

        if (collision.gameObject.name == "Nest")
        {
            Lives++;
            Destroy(collision.gameObject);
        }
    }
}