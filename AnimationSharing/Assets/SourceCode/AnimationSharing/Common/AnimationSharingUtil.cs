using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnimationSharing
{
    public static class AnimationSharingUtil
    {
        public static void SetBonePoseBuffByRootBone(Transform rootBone, out BonePoseBuff poseBuff)
        {
            poseBuff = new BonePoseBuff();

            poseBuff.RootBone = rootBone;
            poseBuff.PosedBones = new List<Transform>();

            var posedBones = poseBuff.PosedBones;
            
            var transforms = rootBone.GetComponentsInChildren<Transform>(false);
            foreach(var tf in transforms)
            {
                poseBuff.PosedBones.Add(tf);
            }
        }

        public static void CopyBonePosedBuff(BonePoseBuff fromBuff, BonePoseBuff toBuff)
        {
            if (fromBuff == null || toBuff == null) return;
            if (fromBuff.RootBone == toBuff.RootBone) return;

            if(fromBuff.PosedBones.Count != toBuff.PosedBones.Count)
            {
                UnityEngine.Debug.LogFormat("TODO: retargeting is not realized currently...");
                return;
            }

            for(int i = 0; i < toBuff.PosedBones.Count; i++)
            {
                toBuff.PosedBones[i].localPosition = fromBuff.PosedBones[i].localPosition;
                toBuff.PosedBones[i].localRotation = fromBuff.PosedBones[i].localRotation;

                // TODO : 暂时不考虑scale
            }
        }


    }
}
