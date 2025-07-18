using System;
using Data;
using UnityEngine;

namespace World
{
    /// <summary>
    /// This gameObject is only activated after the required quest is completed
    /// They are all deactivated on Awake
    /// </summary>
    public class LockedByQuest : MonoBehaviour
    {
        [SerializeField] private Quest requiredQuest;

        public Quest RequiredQuest => requiredQuest;


        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}