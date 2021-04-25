using System.Drawing;

namespace Go.Items
{
    public abstract class TypeItem
    {
        public string Name { get; private set; }
        public Color Color { get; protected set; }

        protected TypeItem(string name)
        {
            Name = name;
        }
    }

    public class Hydrography : TypeItem
    {
        public Hydrography(string name) : base(name)
        {
            Color = Color.DeepSkyBlue;
        }
    }
    public class Flora : TypeItem
    {
        public Flora(string name) : base(name)
        {
            Color = Color.ForestGreen;
        }
    }
    public class ArtificalObject : TypeItem
    {
        public ArtificalObject(string name) : base(name)
        {
            Color = Color.Black;
        }
    }
    public class Landform : TypeItem
    {
        public int Level { get; private set; }
        public Landform(string name, int level = 0) : base(name)
        {
            Color = Color.SaddleBrown;
            Level = level;
        }
    }
    public class Stone : TypeItem
    {
        public Stone(string name) : base(name)
        {
            Color = Color.Gray;
        }
    }
}

