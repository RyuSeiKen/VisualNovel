using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Narative {
    public interface INarativeManagerrMock {

        /*********
         * ACTOR *
         *********/

        // Scene
        void HandleActorEnterScene(object actor, object entryType);
        void HandleActorExitScene(object actor, object extitType);
        void HandleActorMovemnt(object actor, object movementType);

        // Speaker
        void HandleActorSpeaker(object actor);

        // Body
        void HandleActorFaceChange(object actor, object face);
        void HandleActorClothesChange(object actor, object clothes);
        void HandleActorPoseChange(object actor, object pause);



        /*********
         * SCENE *
         *********/

        // Change
        void HandleSceneChange(object newScene, object transitionType);


        /************
         * CG Event *
         ************/

        // CG
        void HandleCgEventStart(object cg, object transitionType);
        void HandleCgEventChange(object cg);
        void HandleCgEventEnd(object transitionType);


        /*******
         * GFX *
         *******/
        void HandleGfx(object gfx);

        /*********
         * AUDIO *
         *********/
        void HandleBgmChange(object bgm);
        void HandleSfx(object sfx);

    }
}