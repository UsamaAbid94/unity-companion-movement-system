using UnityEngine;

namespace Companion.Gameplay
{
    [CreateAssetMenu(fileName = "New CompanionData", menuName = "CompanionSystem/CompanionData")]
    public class CompanionData : ScriptableObject
    {
        [SerializeField]
        private string _companionName;
        [TextArea(3, 5)]
        [SerializeField]
        private string _companionDescription;

        [SerializeField]
        private GameObject _companionPrefab;

        [SerializeField]
        private float _companionMaxHealth = 1.0f;
        [SerializeField]
        private float _moveSpeed = 1.0f;
        [SerializeField]
        private float _dashSpeed = 1.0f;
        [SerializeField]
        private float _dashCoolDown = 1.0f;
        [SerializeField]
        private float _dashingTime = 0.2f;
        [SerializeField]
        private bool _canDash = true;
        [SerializeField]
        private bool _isDashing;

        [Header("Attack Stats")]
        [SerializeField]
        private float _damagePerSecond;

        [Header("=== LEVEL SYSTEM ===")]
        [SerializeField]
        private int _currentLevel = 1;
        [SerializeField]
        private float _currentExp = 0f;


        [Header("=== TIER SYSTEM ===")]
        [SerializeField]
        private int _maxLevel = 99;
        [SerializeField]
        private int _currentTier;    // T0 = no bonus
        [SerializeField]
        private int _maxTier = 10;       // or whatever
        [SerializeField]
        private float _tierStep = 0.2f;  // 20% per tier

        [SerializeField]
        private float _detectionRange = 10f;

        public string CompanionName => _companionName;

        public string CompanionDescription => _companionDescription;

        public int CurrentLevel => _currentLevel;

        public float CurrentExp => _currentExp;

        public int MaxLevel => _maxLevel;

        public int CurrentTier => _currentTier;
      


        public float CompanionMaxHealth
        {
            get => _companionMaxHealth;
            set => _companionMaxHealth = value;
        }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }


        public float DashSpeed
        {
            get => _dashSpeed;
            set => _dashSpeed = value;
        }

        public float DashCoolDown
        {
            get => _dashCoolDown;
            set => _dashCoolDown = value;
        }

        public float DashTiming
        {
            get => _dashingTime;
            set => _dashingTime = value;
        }
        
        public bool CanDash
        {
            get => _canDash;
            set => _canDash = value;
        }

        public bool IsDashing
        {
            get => _isDashing;
            set => _isDashing = value;
        }

        public float DamagePerSecond
        {
            get => _damagePerSecond;
            set => _damagePerSecond = value;
        }

        public float DetectionRange
        {
            get => _detectionRange;
            set => _detectionRange = value;
        }
      

        #region Methods

        public GameObject GetPrefab()
        {
            return _companionPrefab;
        }

        #endregion
    }
}
