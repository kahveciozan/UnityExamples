using System;
using UnityEngine;

namespace Assets.VariablesAndInspector.Typess
{
    [Serializable]
    public class Player
    {
        [field: SerializeField]
        public int Id { get; set; }

        [field: SerializeField]
        [Tooltip("Player Email")]
        public string Email;

        [field: SerializeField]
        public string FirstName;

        [field: SerializeField]
        public string LastName;

        [field: SerializeField]
        [TextArea(5,10)]
        [Tooltip("Player Notes")]
        public string notes;

        [Space]
        [Header("Min Player Requirements")]

        [field: SerializeField]
        [Range(18,120)]
        public int MinAge = 18;


        [field: SerializeField]
        [Tooltip("Where players are allowed to play from")]
        public CountryAllowed CountryAllowed = CountryAllowed.None;
    }
}
