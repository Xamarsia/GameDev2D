using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InteractableObjects
{
    public class InteractableObject : MonoBehaviour
    {
        protected PlayerCharacter _playerCharacter;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_playerCharacter!= null)
            {
                return;
            }

            PlayerCharacter playerCharacter = other.GetComponent<PlayerCharacter>();
            if(playerCharacter != null)
            {
                Interact(playerCharacter);
            }
        }

        protected virtual void Interact(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;
        }
    }
}
