using UnityEngine;
using Random = UnityEngine.Random;

public class Bounce : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// The desired speed of the ball's displacement.
    /// </summary>
    [SerializeField]
    private float _speed;

    /// <summary>
    /// The ball's current rate of change in position and its direction.
    /// </summary>
    private Vector3 _velocity;

    /// <summary>
    /// Camera to use to confine the ball to its viewport.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// Caches Transform component.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Allows you to tweak the ball's collision with the viewport.
    /// </summary>
    [SerializeField]
    private float _radiusOffset = .1f;

    /// <summary>
    /// The radius of the ball game object.
    /// </summary>
    private float _radius;

    /// <summary>
    /// Camera's viewport position in world space.
    /// </summary>
    private Vector3 _viewport;
    
    #endregion

    #region --Unity Specific Methods--
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }
    
    #endregion
    
    #region --Custom Methods--

    /// <summary>
    /// Initializes variables and caches components.
    /// </summary>
    private void Init()
    {
        _transform = transform;
        _camera = Camera.main;
        _radius = GetComponent<SphereCollider>().radius + _radiusOffset;
        _viewport = _camera.ViewportToWorldPoint(_camera.transform.position); 
        _velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

    /// <summary>
    /// Handles movement and tracks the ball's position.
    /// </summary>
    private void Move()
    {
        transform.position += _velocity * _speed;
        TrackPosition();
    }

    /// <summary>
    /// Confines the ball to the camera's viewport.
    /// </summary>
    private void TrackPosition()
    {
        Vector3 _currentPosition = _transform.position;
        if (_currentPosition.x - _radius < -_viewport.x)
        {
            _velocity = Vector3.Reflect(_velocity, Vector3.left);
        }
        else if (_currentPosition.x + _radius > _viewport.x)
        {
            _velocity = Vector3.Reflect(_velocity, Vector3.right);
        }
        
        if (_currentPosition.y - _radius > _viewport.y)
        {
            _velocity = Vector3.Reflect(_velocity, Vector3.up);
        }
        else if (_currentPosition.y + _radius < -_viewport.y)
        {
            _velocity = Vector3.Reflect(_velocity, Vector3.down);
        }
    }
    
    #endregion

}