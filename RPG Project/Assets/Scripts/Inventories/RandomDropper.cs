using GameDevTV.Inventories;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {
        //CONFIG DATA
        [Tooltip("How far can the pickups be scattered from the dropper.")]
        [SerializeField] float scatterDistance = 1f;
        [SerializeField] InventoryItem[] dropLibrary;
        [SerializeField] int maxItemsToDrop = 2;

        //CONSTANTS
        const int ATTEMPTS = 30;

        public void RandomDrop()
        {
            for (int i = 0; i < maxItemsToDrop; i++)
            {
                var itemToDrop = dropLibrary[Random.Range(0, dropLibrary.Length)];
                DropItem(itemToDrop);
            }
        }
        protected override Vector3 GetDropLocation()
        {
            for (int i = 0; i < ATTEMPTS; i++)
            {
                //Random Location
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }
    }
}
