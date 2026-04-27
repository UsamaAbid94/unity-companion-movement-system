using System.Collections;
using UnityEngine;

namespace Companion.Gameplay
{
    public class CompanionMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private CompanionData _companionData;
        [SerializeField] private Animator _companionAnimator;
        private Rigidbody _companionBody;
        private float _horizontalInput, _verticalInput;
        private bool _wasMoving = false;

        private void Awake()
        {
            _companionBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            GetInputs();

            if (Input.GetButtonDown("Jump") && _companionData.CanDash) // GetButtonDown, not GetAxis
            {
                var direction = new Vector3(_horizontalInput, 0, _verticalInput).normalized;
                StartCoroutine(CompanionDash(direction)); // Bug 1 fix: StartCoroutine
            }
        }

        private void GetInputs()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        void FixedUpdate()
        {
            Move();
        }

        private IEnumerator CompanionDash(Vector3 direction)
        {
            _companionData.CanDash = false;
            _companionData.IsDashing = true;

            Dash(direction);

            yield return new WaitForSeconds(_companionData.DashTiming);
            _companionData.IsDashing = false;

            yield return new WaitForSeconds(_companionData.DashCoolDown);
            _companionData.CanDash = true;
        }

        public void Dash(Vector3 direction)
        {
            if (direction == Vector3.zero)
                direction = transform.GetChild(0).forward; // use model's facing direction

            _companionBody.linearVelocity = Vector3.zero; // clear velocity before impulse
            _companionBody.AddForce(direction * _companionData.DashSpeed, ForceMode.Impulse);
        }

        public void Move()
        {
            if (_companionData.IsDashing) return; // Bug 2 fix: skip move while dashing

            var movement = new Vector3(_horizontalInput, 0, _verticalInput).normalized;
            bool isMoving = movement.magnitude > 0.1f;

            _companionBody.linearVelocity = new Vector3(
                movement.x * _companionData.MoveSpeed,
                _companionBody.linearVelocity.y,
                movement.z * _companionData.MoveSpeed
            );

            if (isMoving)
                transform.GetChild(0).transform.rotation = Quaternion.LookRotation(movement);

            if (isMoving && !_wasMoving)
            {
                _companionAnimator.ResetTrigger("idle");
                _companionAnimator.SetTrigger("run");
            }
            else if (!isMoving && _wasMoving)
            {
                _companionAnimator.ResetTrigger("run");
                _companionAnimator.SetTrigger("idle");
            }

            _wasMoving = isMoving;
        }
    }
}