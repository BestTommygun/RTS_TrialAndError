using UnityEngine;

namespace SelectionSystem
{
    /// <summary>
    /// The <see cref="SelectionHighlight"/> is a Component to handle the visual part of the Selection System. <br/> 
    /// It requires a <seealso cref="ParticleSystem"/> Component. 
    /// <para> This class cannot be inherited. </para>
    /// <para> Make sure this game object is active. </para>
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class SelectionHighlight : MonoBehaviour
    {
        public const float MIN_SIZE = 2f;
        public const float MAX_SIZE = 20f;

        public static readonly Color defaultColor = Color.white;
        public static readonly Color ownerColor = Color.green;
        public static readonly Color neutralColor = Color.yellow;
        public static readonly Color hostileColor = Color.red;

        [SerializeField]
        [Range(MIN_SIZE, MAX_SIZE)]
        private float highlightSize = 4f;

        private ParticleSystem _particles;

        public Color color
        {
            get
            {
                return _particles.main.startColor.color;
            }
        }

        public float size
        {
            get
            {
                return _particles.main.startSize.constant;
            }
        }

        private void Awake()
        {
            _particles = GetComponent<ParticleSystem>();

            ValidateParticleSize();
        }

        private void ValidateParticleSize()
        {
            var particleMain = _particles.main;

            particleMain.startSize = highlightSize;

            if (particleMain.startSize.constant < MIN_SIZE)
                particleMain.startSize = MIN_SIZE;
            else if (particleMain.startSize.constant > MAX_SIZE)
                particleMain.startSize = MAX_SIZE;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _particles.Play();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _particles.Stop();
        }

        public void SetHighlightColor(Color color)
        {
            var main = _particles.main;
            main.startColor = color;
        }
    }
}