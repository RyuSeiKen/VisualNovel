using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtility {

    /// <summary>
    /// Destroy all childrens GameObjects of the given transform.
    /// </summary>
    /// <param name="transform">The transform whose child will be deleted form.</param>
    public static void DestroyAllChildrens(Transform transform) {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; --i) {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

}
