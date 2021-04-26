using System.Drawing;

namespace Go.Items
{
    public abstract class TypeItem
    {
        public string Name { get; protected set; }
        public Color Color { get; protected set; }
        public int Overcome { get; protected set; }

        protected TypeItem(string name, int overcome)
        {
            Name = name;
            Overcome = overcome;
        }
    }

    public class Hydrography : TypeItem
    {
        public Hydrography(string name, int overcome) : base(name, overcome)
        {
            Color = Color.DeepSkyBlue;
        }
    }
    public class Flora : TypeItem
    {
        public Flora(string name, int overcome) : base(name, overcome)
        {
            Color = Color.ForestGreen;
        }
    }
    public class ArtificalObject : TypeItem
    {
        public ArtificalObject(string name, int overcome) : base(name, overcome)
        {
            Color = Color.Black;
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
    }
    public class Stone : TypeItem
    {
        public Stone(string name, int overcome) : base(name, overcome)
        {
            Color = Color.Gray;
        }
    }
}

