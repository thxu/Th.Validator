using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class NotEmptyModel
    {
        public int[] ArrayFields { get; set; }

        [NotEmpty("字符串不能为null且字符串长度需大于零")]
        public string StrField { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        public IList<string> StrFields { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        public List<string> StrFieldList { get; set; }
    }

    public class NotEmptyModel1
    {
        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        public int[] ArrayFields { get; set; }

        public int[] ArrayFields2 { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public Dictionary<int, string> DicFields { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public ArrayList ArrayFields { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public Queue QueueFields { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public LinkedList<string> LinkedListFields { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public Hashtable HashtableFields { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public SortedList SortedListFields { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public SortedList<int, string> SortedListFields1 { get; set; }

        //[NotEmpty("集合不能为null，且至少需要有一个元素")]
        //[InnerValid]
        //public Stack<int> StackFields { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        [InnerValid]
        public HashSet<int> HashSetFields { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        [InnerValid]
        public SortedSet<int> SortedSetFields { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        [InnerValid]
        public BitArray BitArrayFields { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        [InnerValid]
        public ListDictionary ListDictionaryFields { get; set; }

        [NotEmpty("集合不能为null，且至少需要有一个元素")]
        [InnerValid]
        public HybridDictionary HybridDictionaryFields { get; set; }
    }
}
