﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VRCSDKHelper
{
    public static class ResetToBasePose
    {
        public static void DoResetToBasePose(Animator animator, GameObject model)
        {
            var dummyModel = GameObject.Instantiate(model);
            dummyModel.name = "DummyModel(VRCSDKHelper - Reset To Base Pose)";
            var baseAnimator = dummyModel.GetComponent<Animator>();

            List<Transform> baseBoneList = new List<Transform>();
            List<Transform> targetBoneList = new List<Transform>();
            int boneCount = (int)HumanBodyBones.LastBone + 1;
            for (int i = 0; i < boneCount; i++)
            {
                var bone = (HumanBodyBones)i;
                var targetTrf = animator.GetBoneTransform(bone);
                var baseTrf = baseAnimator.GetBoneTransform(bone);

                if (targetTrf == null || baseTrf == null)
                    continue;

                Undo.RecordObject(targetTrf, "Reset To T Pose");

                targetTrf.localPosition = baseTrf.localPosition;
                targetTrf.localRotation = baseTrf.localRotation;
                targetTrf.localScale = baseTrf.localScale;
            }

            GameObject.DestroyImmediate(dummyModel);
        }
    }
}