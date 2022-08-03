using System.Linq;
using System;
using System.Collections.Generic;
using TheOtherRoles.Players;
using static TheOtherRoles.TheOtherRoles;
using UnityEngine;

namespace TheOtherRoles {
    public class VisualAppearance {
        public float SpeedFactor { get; set; } = 1.0f;
        public Vector3 SizeFactor { get; set; } = new Vector3(0.7f, 0.7f, 1.0f);
    }
}
