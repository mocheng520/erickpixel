using System;
using UnityEngine;

namespace TargetingSystem
{
    public struct TargetInfo : IEquatable<TargetInfo>, IFormattable
    {
        private static readonly TargetInfo _none = new TargetInfo();
        /// <summary>
        /// Shorthand to represent "null" value.
        /// </summary>
        public static TargetInfo None 
        {
            get
            {
                return _none;
            }
        }

        public readonly bool isValid;
        public readonly string name;
        public readonly Vector3 position;
        public readonly Quaternion rotation;

        public TargetInfo(ITargetable target)
        {
            isValid = target != null;

            if (isValid)
            {
                name = target.aimPoint.name;
                position = target.aimPoint.position;
                rotation = target.aimPoint.rotation;
            }
            else
            {
                name = string.Empty;
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }
        }

        public TargetInfo(Transform transform)
        {
            isValid = transform != null;

            if (isValid)
            {
                name = transform.name;
                position = transform.position;
                rotation = transform.rotation;
            }
            else
            {
                name = string.Empty;
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }
        }

        public TargetInfo(bool isValid, string name, Vector3 position, Quaternion rotation)
        {
            this.isValid = isValid;
            this.name = name;
            this.position = position;
            this.rotation = rotation;
        }

        public bool Equals(TargetInfo other)
        {
            return isValid == other.isValid &&
                   name == other.name &&
                   position.Equals(other.position) &&
                   rotation.Equals(other.rotation);
        }

        public override bool Equals(object obj)
        {
            return obj is TargetInfo info && Equals(info);
        }

        public override int GetHashCode()
        {
            int hash1 = isValid.GetHashCode() << 3;
            int hash2 = name.GetHashCode() << 3;
            int hash3 = position.GetHashCode() << 3;
            int hash4 = rotation.GetHashCode() << 3;

            return (hash1 + hash2 + hash3 + hash4) >> 3;
        }

        public override string ToString()
        {
            return $"Target: {name} Valid: {isValid} Position: {position} Rotation: {rotation}";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format, formatProvider);
        }
    }
}
