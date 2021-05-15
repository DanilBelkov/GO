using System;
using System.Drawing;

namespace Go.Items
{
    public abstract class TypeItem
    {
        public string Name { get; protected set; }
        public Color Color { get; protected set; }
        public int Overcome { get; protected set; }
        public bool IsImpossible { get; protected set; }
        public Sequence Sequence;
        abstract public TypeItem GetCopy();
        protected TypeItem(string name, int overcome)
        {
            Name = name;
            Overcome = overcome;
        }
        public override bool Equals(Object type)
        {
            if ((type == null) || !this.GetType().Equals(type.GetType()))
                return false;
            else
            {
                TypeItem p = (TypeItem)type;
                return (this.Name == p.Name && this.Sequence.Equals(p.Sequence));
            }
        }
        public override int GetHashCode()
        {
            return Overcome;
        }
    }

    public class Hydrography : TypeItem
    {
        public Hydrography(string name, int overcome) : base(name, overcome)
        {
            Color = Color.DeepSkyBlue;
            IsImpossible = false;
        }
        override public TypeItem GetCopy()
        {
            return new Hydrography(Name, Overcome);
        }
    }
    public class Lake : Hydrography
    {
        public Lake() : base("Lake", 0)
        {
            Sequence = new Items.Area();
            IsImpossible = true;
        }
        override public TypeItem GetCopy()
        {
            return new Lake();
        }
    }
    public class Pond : Hydrography // пруд
    {
        public Pond() : base("Pond", 80)
        {
            Sequence = new Items.Area();
        }
        override public TypeItem GetCopy()
        {
            return new Pond();
        }
    }
    public class ImpassableSwamp : Hydrography
    {
        public ImpassableSwamp() : base("ImpassableSwamp", 0)
        {
            Sequence = new Items.Area();
            IsImpossible = true;
        }
        override public TypeItem GetCopy()
        {
            return new ImpassableSwamp();
        }
    }
    public class Swamp : Hydrography
    {
        public Swamp() : base("Swamp",90)
        {
            Sequence = new Items.Area();
        }
        override public TypeItem GetCopy()
        {
            return new Swamp();
        }
    }
    public class Waterlogging : Hydrography // заболоченность
    {
        public Waterlogging() : base("Waterlogging", 60)
        {
            Sequence = new Items.Area();
        }
        override public TypeItem GetCopy()
        {
            return new Waterlogging();
        }
    }
    public class ImpassableRiver : Hydrography
    {
        public ImpassableRiver() : base("ImpassableRiver", 0)
        {
            Sequence = new Items.Line();
            IsImpossible = true;
        }
        override public TypeItem GetCopy()
        {
            return new ImpassableRiver();
        }
    }
    public class DisappearRiver : Hydrography
    {
        public DisappearRiver() : base("DisappearRiver", 20)
        {
            Sequence = new Items.Line();
        }
        override public TypeItem GetCopy()
        {
            return new DisappearRiver();
        }
    }
    public class SlimSwamp : Hydrography
    {
        public SlimSwamp() : base("SlimSwamp", 50)
        {
            Sequence = new Items.Line();
        }
        override public TypeItem GetCopy()
        {
            return new SlimSwamp();
        }
    }
    public class WaterLittlePit : Hydrography
    {
        public WaterLittlePit() : base("WaterLittlePit", 100)
        {
            Sequence = new Items.Single();
        }
        override public TypeItem GetCopy()
        {
            return new WaterLittlePit();
        }
    }
    public class WaterPit : Hydrography
    {
        public WaterPit() : base("WaterPit", 100)
        {
            Sequence = new Items.Single();
        }
        override public TypeItem GetCopy()
        {
            return new WaterPit();
        }
    }
    public class WaterSource : Hydrography
    {
        public WaterSource() : base("WaterSource", 100)
        {
            Sequence = new Items.Single();
        }
        override public TypeItem GetCopy()
        {
            return new WaterSource();
        }
    }
    public class WaterObject : Hydrography
    {
        public WaterObject() : base("WaterObject", 100)
        {
            Sequence = new Items.Single();
        }
        override public TypeItem GetCopy()
        {
            return new WaterObject();
        }
    }

    public class Flora : TypeItem
    {
        public Flora(string name, int overcome) : base(name, overcome)
        {
            Color = Color.ForestGreen;
        }
        override public TypeItem GetCopy()
        {
            return new Flora(Name, Overcome);
        }
    }
    public class ArtificalObject : TypeItem
    {
        public ArtificalObject(string name, int overcome) : base(name, overcome)
        {
            Color = Color.Black;
        }
        override public TypeItem GetCopy()
        {
            return new ArtificalObject(Name, Overcome);
        }
    }
    public class Landform : TypeItem
    {
        public int Level { get; private set; }
        public Landform(string name, int overcome, int level = 0) : base(name, overcome)
        {
            Color = Color.SaddleBrown;
            Level = level;
        }
        override public TypeItem GetCopy()
        {
            return new Landform(Name, Overcome);
        }
    }
    public class Stone : TypeItem
    {
        public Stone(string name,int overcome) : base(name,overcome)
        {
            Color = Color.Gray;
        }
        override public TypeItem GetCopy()
        {
            return new Stone(Name, Overcome);
        }
    }
}

