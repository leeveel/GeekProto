using MessagePack;
using System.Collections.Generic;

namespace Geek.Proto
{

    [MessagePackObject]
    public class MyClass
    {
        [Key(0)]
        public int Age { get; set; }

        [Key(1)]
        public string FirstName { get; set; }

        [Key(2)]
        public string LastName { get; set; }
    }



    [MessagePackObject]
    public class MyClass1
    {
        [Key(0)]
        public int Age { get; set; }

        [Key(1)]
        public string FirstName { get; set; }

        [Key(2)]
        public string LastName { get; set; }
        [Key(3)]
        public List<List<int>> List1 { get; set; }



        public void Ser<T>(List<List<int>> v) where T : List<List<int>>
        {
            foreach (var item in v)
            {
            }
        }

        public void SerializeNestList<T>(T val) where T : List<T> 
        {
            foreach (var item in val)
            {
                //item.Count;
                //SerializePrimitiveList(item);
            }
        }

        public void SerializePrimitiveList<T>(List<T> val) where T : struct
        {
            foreach (var item in val)
            {
            }
        }

    }
}
