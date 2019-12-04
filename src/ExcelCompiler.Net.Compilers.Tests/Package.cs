using System;

namespace ExcelCompiler.Net.Compilers.Tests
{
    public class Package : IEquatable<Package>
    {
        public readonly string Name;

        public Package(string name)
        {
            Name = name;
        }

        public bool Equals(Package other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Package) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}