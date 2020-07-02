using UnityEngine;

namespace BetterLock
{
    class DoorState : MonoBehaviour
    {
        private float timer = 0f;
        private float timeIsUp = 1.0f;
        private float timeToUnlock = Global.timeToUnlock;
        public void Start()
        {
            gameObject.GetComponent<Door>().locked = true;
        }

        public void Update()
        {
            timer = timer + Time.deltaTime;

            if (timer >= timeIsUp)
            {
                timer = 0;

                timeToUnlock = timeToUnlock - timeIsUp;

            }
            if (timeToUnlock <= 0f)
            {
                gameObject.GetComponent<Door>().locked = false;
                Destroy(gameObject.GetComponent<DoorState>());
            }

        }
    }
}
