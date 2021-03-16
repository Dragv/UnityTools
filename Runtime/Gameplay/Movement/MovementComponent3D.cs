using UnityEngine;

namespace Vocario.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent3D : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 100.0f)]
        private float _maxSpeed = 10.0f;
        [SerializeField, Range(0.0f, 100.0f)]
        private float _maxAcceleration = 10.0f;
        [SerializeField, Range(0.0f, 100.0f)]
        private float _maxAirAcceleration = 2.0f;
        [SerializeField, Range(0.0f, 10.0f)]
        private float _jumpHeight = 2.0f;
        [SerializeField, Range(0.0f, 5.0f)]
        private float _maxAirJumps = 0.0f;
        [SerializeField, Range(0.0f, 90.0f)]
        private float _maxGroundAngle = 25.0f;

        private Vector3 _velocity = Vector3.zero;
        private Vector3 _desiredVelocity = Vector3.zero;
        private Rigidbody _body = null;
        private bool _desiredJump = false;
        private int _jumpCount = 0;
        private float _minGroundDotProduct = 0.0f;
        private Vector3 _contactNormal = Vector3.zero;
        private int _groundContactCount = 0;
        private bool OnGround => _groundContactCount > 0;

        public Vector3 DesiredVelocity { set => _desiredVelocity = value * _maxSpeed; }
        public bool DesiredJump { set => _desiredJump = value; }

        private void OnValidate()
        {
            _minGroundDotProduct = Mathf.Cos(_maxGroundAngle * Mathf.Deg2Rad);
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            OnValidate();
        }

        private void FixedUpdate()
        {
            UpdateState();
            AdjustVelocity();

            if (_desiredJump == true)
            {
                _desiredJump = false;
                Jump();
            }
            _body.velocity = _velocity;
            ClearState();
        }

        private void ClearState()
        {
            _groundContactCount = 0;
            _contactNormal = Vector3.zero;
        }

        private void UpdateState()
        {
            _velocity = _body.velocity;
            if (OnGround == true)
            {
                _jumpCount = 0;
                if (_groundContactCount > 1)
                {
                    _contactNormal.Normalize();
                }
            }
            else
            {
                _contactNormal = Vector3.up;
            }
        }

        private Vector3 ProjectOnContactPlane(Vector3 vector)
        {
            return vector - _contactNormal * Vector3.Dot(vector, _contactNormal);
        }

        private void AdjustVelocity()
        {
            Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            float currentX = Vector3.Dot(_velocity, xAxis);
            float currentZ = Vector3.Dot(_velocity, zAxis);

            float acceleration = OnGround ? _maxAcceleration : _maxAirAcceleration;
            float maxSpeedChange = acceleration * Time.deltaTime;

            float newX = Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
            float newZ = Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

            _velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }

        private void Jump()
        {
            if (OnGround == true || _jumpCount < _maxAirJumps)
            {
                _jumpCount++;
                float jumpSpeed = Mathf.Sqrt(-2.0f * Physics.gravity.y * _jumpHeight);
                float alignedSpeed = Vector3.Dot(_velocity, _contactNormal);

                if (alignedSpeed > 0.0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0.0f);
                }
                _velocity += _contactNormal * jumpSpeed;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            EvaluateCollision(other);
        }

        private void OnCollisionStay(Collision other)
        {
            EvaluateCollision(other);
        }

        private void EvaluateCollision(Collision other)
        {
            for (int index = 0; index < other.contactCount; index++)
            {
                Vector3 normal = other.GetContact(index).normal;
                if (normal.y >= _minGroundDotProduct)
                {
                    _groundContactCount += 1;
                    _contactNormal += normal;
                }
            }
        }
    }
}
