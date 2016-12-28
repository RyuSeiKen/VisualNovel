using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Narative {


    /*********
     * ACTOR *
     *********/

    public abstract class ActorAnimation : ScriptableObject {

        public new string name;

        public Animation unityAnimation;

    }

    // Scene

    [CreateAssetMenu(fileName = "unamed_actor_entrance_animation", menuName = "Narative/Actor/Animation/Entrance", order = 0)]
    public class ActorEntance : ActorAnimation {
        public ActorEntance() {
            this.name = "unamed_actor_entrance_animation";
        }
    }

    [CreateAssetMenu(fileName = "unamed_actor_exit_animation", menuName = "Narative/Actor/Animation/Exit", order = 1)]
    public class ActorExit: ActorAnimation {
        public ActorExit() {
            this.name = "unamed_actor_exit_animation";
        }
    }

    [CreateAssetMenu(fileName = "unamed_actor_movement_animation", menuName = "Narative/Actor/Animation/Movement", order = 2)]
    public class ActorMovement : ActorAnimation {

    }

    [CreateAssetMenu(fileName = "unamed_actor_facila_expretion_animation", menuName = "Narative/Actor/Animation/Facial Expretion", order = 3)]
    public class ActorFacialExpretion : ActorAnimation {

    }
}