using UnityEngine;

namespace Xtaieer.Toolbox
{
    public abstract class AbstractZone : MonoBehaviour
    {
        [SerializeField]
        private AbstractBoundary[] _areaBoundaries;

        protected virtual void Awake()
        {
            for(int i = 0; i < _areaBoundaries.Length; i ++)
            {
                _areaBoundaries[i].onEnter += OnEnter;
                _areaBoundaries[i].onExit += OnExit;
            }
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            CollectBoundary();
        }

        [ContextMenu("CollectBoundary")]
        private void CollectBoundary()
        {
            _areaBoundaries = GetComponentsInChildren<AbstractBoundary>();
        }
#endif
    }
}
