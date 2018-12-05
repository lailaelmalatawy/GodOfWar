using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malatawy
{
    public class PlayerFPS : MonoBehaviour
    {
    
        void Start() {

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Slash)) {
                Debug.Log("Attack!");
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<ChasePlayer>().isHit();
            }
        }
    }
}