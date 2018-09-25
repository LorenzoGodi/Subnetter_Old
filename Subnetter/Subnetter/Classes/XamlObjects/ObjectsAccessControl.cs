using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.Classes.XamlObjects
{
    class ObjectsAccessControl
    {
        class Obj
        {
            public string name;
            public int blockTimes;

            public Obj(string name)
            {
                this.name = name;
                blockTimes = 0;
            }
        }

        List<Obj> objects;

        public ObjectsAccessControl()
        {
            objects = new List<Obj>();
        }

        public void AddObj(string name)
        {
            objects.Add(new Obj(name));
        }

        public void AddObj(params string[] names)
        {
            foreach (string name in names)
                objects.Add(new Obj(name));
        }

        public void Block(string name)
        {
            for (int v = 0; v < objects.Count; v++)
                if (objects[v].name == name)
                    objects[v].blockTimes++;
        }

        public bool IsFree(string name)
        {
            for (int v = 0; v < objects.Count; v++)
            {
                if (objects[v].name == name)
                {
                    if (objects[v].blockTimes > 0) { objects[v].blockTimes--; return false; }
                    return true;
                }
            }
            throw new Exception("Oggetto non contenuto nella lista");
        }
    }
}
