using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Gameplay {
    public class SubjectInstance {

        private Data.Subject subjectData;

        public string name { get { return subjectData.name; } }
        public string inkGlobalVaraibleName { get { return subjectData.inkGlobalVaraibleName; } }

        public int level = 1;

        private SubjectInstance(Data.Subject subjectData) {
            this.subjectData = subjectData;
        }

        public static SubjectInstance Create(Data.Subject subjectData) {
            return new SubjectInstance(subjectData);
        }

        public bool IsInstanceOf(Data.Subject subjectData) {
            return this.subjectData == subjectData;
        }

    }
}