using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraNet.Shared.Buffers
{
    public struct PlayerMoveBuffer
    {
        public char[] AnimationParameter;

        public float QuaternionX;
        public float QuaternionY;
        public float QuaternionZ;
        public float QuaternionW;

        public float PositionX;
        public float PositionY;
        public float PositionZ;
    }
}
