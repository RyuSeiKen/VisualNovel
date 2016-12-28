using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;

namespace Feature.Narative {
    
    public class NarativeManager : MonoBehaviour, INarativeManagerrMock {

        public void Init(Story story) {
           // story.bin
        } 

        public void HandleActorClothesChange(object actor, object clothes) {
            throw new NotImplementedException();
        }

        public void HandleActorEnterScene(object actor, object entryType) {
            throw new NotImplementedException();
        }

        public void HandleActorExitScene(object actor, object extitType) {
            throw new NotImplementedException();
        }

        public void HandleActorFaceChange(object actor, object face) {
            throw new NotImplementedException();
        }

        public void HandleActorMovemnt(object actor, object movementType) {
            throw new NotImplementedException();
        }

        public void HandleActorPoseChange(object actor, object pause) {
            throw new NotImplementedException();
        }

        public void HandleActorSpeaker(object actor) {
            throw new NotImplementedException();
        }

        public void HandleBgmChange(object bgm) {
            throw new NotImplementedException();
        }

        public void HandleCgEventChange(object cg) {
            throw new NotImplementedException();
        }

        public void HandleCgEventEnd(object transitionType) {
            throw new NotImplementedException();
        }

        public void HandleCgEventStart(object cg, object transitionType) {
            throw new NotImplementedException();
        }

        public void HandleGfx(object gfx) {
            throw new NotImplementedException();
        }

        public void HandleSceneChange(object newScene, object transitionType) {
            throw new NotImplementedException();
        }

        public void HandleSfx(object sfx) {
            throw new NotImplementedException();
        }
    }
}