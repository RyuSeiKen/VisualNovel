using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Narrative {
    [CreateAssetMenu(fileName = "unamed_character", menuName = "[PROJECT]/Narrative/Character", order = 0)]
    public class Character : ScriptableObject {

        public new string name = "unamed_character";
        public string talkerName = "unamed_character";
        
        public List<ActorMovement> movementList;

        public List<ActorFacialExpretion> facialExpretionList;


         

    }
}