using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Gameplay.Data {

    [CreateAssetMenu(fileName = "Subject", menuName = "Feature/Gameplay/Subject")]
    [System.Serializable]
    public class Subject : ScriptableObject {
        
        public new string name = "Unamed Subject";

        [Header("Ink")]
        public string inkGlobalVaraibleName = "None";

    }

}