using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnimationSharing
{
    public class AnimationSharingComponent : MonoBehaviour
    {
        private int mCurrentState;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void SetCurrentState(int state)
        {
            mCurrentState = state;
        }

        public int GetCurrentState()
        {
            return mCurrentState;
        }
    }
}
