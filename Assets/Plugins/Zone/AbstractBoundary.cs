using UnityEngine;

namespace Xtaieer.Toolbox
{
    public class AbstractBoundary : MonoBehaviour
    {
        public event System.Action onEnter;
        public event System.Action onExit;

        protected void OnEnter()
        {
            if (onEnter != null)
            {
                onEnter();
            }
        }

        protected void OnExit()
        {
            if (onExit != null)
            {
                onExit();
            }
        }
    }
}
