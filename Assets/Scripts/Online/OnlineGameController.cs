#if DEBUG
#define SERVER
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Online
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        private void Awake()
        {
            instance = this;
            Networking.Start();
        }

#if SERVER
        public void Entity_Create(int id, Vector3 position, object data)
        {
            // server send data to clients
        }
#endif

#if SERVER
        public void Entity_Update(int id, object data)
        {
            // server send data to clients
        }
#endif

#if SERVER
        public void Entity_Destroy(int id)
        {
            // server send data to clients
        }
#endif

    }
}