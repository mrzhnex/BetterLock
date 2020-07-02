using UnityEngine;

namespace BetterLock
{
    class CooldownComponent : MonoBehaviour
    {
        private float timeIsUp = 1.0f;
        private float timer = 0f;
        public float cooldown = Global.cooldown;

        public void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;

                cooldown = cooldown - timeIsUp;

            }
            if (cooldown <= 0f)
            {
                Destroy(gameObject.GetComponent<CooldownComponent>());
            }
        }

    }
}
