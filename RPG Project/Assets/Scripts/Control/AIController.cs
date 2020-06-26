using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        Health health;
        GameObject player;

        Vector3 guardLocation;
        float timeSinceLastSightOfPlayer;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                GetComponent<Mover>().StartMoveAction(guardLocation);
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance;
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
