using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 合併AI練習
{
    internal  class AIRequest
    {
        public List<Content> contents { get; set; } = new List<Content>();
        public List<Tool> tools { get; set; } = new List<Tool>();

        public class Content
        {
            public string role { get; set; }
            public Part[] parts { get; set; }
        }

        public class Part
        {
            public string text { get; set; }
        }

        public class Tool
        {
            public List<Functiondeclaration> functionDeclarations { get; set; } = new List<Functiondeclaration>();

            public void Add(Functiondeclaration functiondeclaration)
            {
                this.functionDeclarations.Add(functiondeclaration);
            }

            public void AddRange(List<Functiondeclaration> functiondeclarations)
            {
                this.functionDeclarations.AddRange(functiondeclarations);
            }


            
        }

        public class Functiondeclaration
        {
            public string name { get; set; }
            public string description { get; set; }
            public Parameters parameters { get; set; }

           
        }

        public abstract class Parameters
        {
            public string type { get; set; }
            public abstract object properties { get;}
            public string[] required { get; set; }

          

        }


    }
}
