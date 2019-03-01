using UnityEngine;

namespace Xtaieer.Toolbox
{
    public class TriggerBoundary : AbstractBoundary
    {
        private void OnTriggerExit(Collider other)
        {
            Vector3 vector = other.transform.position - transform.position;
            if (Vector3.Dot(transform.forward, vector) > 0f)
            {
                OnEnter();
            }
            else
            {
                OnExit();
            }
        }
    }
}
