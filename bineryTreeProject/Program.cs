using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;
using System.Xml.Linq;

namespace bineryTreeProject
{
    internal class Program
    {
        public static List<ThretModel>? Threats { get; set; }
        public static List<DefencModel>? DefenceStrategies { get; set; }
        public static BSTree DefenceTree { get; set; }
        static void Main(string[] args)
        {
            Threats = jsonTools.ReadFromJsonAsync<List<ThretModel>>("../../../threats.json");
            DefenceStrategies = jsonTools.ReadFromJsonAsync<List<DefencModel>>("../../../defenceStrategiesBalanced.json");

            DefenceTree = InsertDefences(DefenceStrategies, new BSTree());

            PrintTree(DefenceTree.root,"root");

            findDefencPreOrderHelper(culcThretValeu(Threats[0]), DefenceTree.root);
        }
        public static BSTree InsertDefences(List<DefencModel> defenceStrategies, BSTree tree)
        {
            foreach (DefencModel def in defenceStrategies)
            {
                tree.Insert(def);
            }
            return tree;
        }
        public static void PrintTree(TreeNode root , string LorRorRoot)
        {

            if (root != null)
            {
                string str = new string('-', root.height) +root.Value.ToString()+string.Join("," , root.Defense);
                // Print the current value
                Console.WriteLine(str);
                // Traverse left subtree
                PrintTree(root.Left,"left");
                // Traverse right subtree
                PrintTree(root.Right,"right");
            }
        }        
        public static int culcThretValeu(ThretModel thret)
        {
            int TargetValue;
            switch (thret.Target)
            {

                case "Web Server":
                    TargetValue = 10;
                    break;
                case "Database":
                    TargetValue = 15;
                    break;
                case "User Credentials":
                    TargetValue = 20;
                    break;
                default:
                    TargetValue = 5;
                    break;
            }
            int severity = (thret.Volume * thret.Sophistication) + TargetValue;
            return severity;
        }
        public static void findDefencPreOrderHelper(int threatValeu, TreeNode? root)
        {
            if (threatValeu < BSTree.FindMin(root).min)
            {
                Console.WriteLine("Atec Server is bloe the threshold. Attack is ignored");
                return;
            }
            if (threatValeu > BSTree.FindMax(root).max)
            {
                Console.WriteLine("No suitable defence was found. Brace for impact");
            }
            else
            {
                findDefencPreOrder(threatValeu, root);
            }
        }
        private static void findDefencPreOrder(int threatValeu , TreeNode? root)
        {
            // Base case: if the current node is null, return an empty list
            if (root == null) { return; }
        
            if (threatValeu >= root.Value.min && threatValeu <= root.Value.max)
            {
                for(int index =0; index < root.Defense.Count(); index++)
                {
                    Console.WriteLine(root.Defense[index]);
                    Thread.Sleep(2000);
                    return;
                };
            }

            // Recursively get the list from the left subtree
            findDefencPreOrder(threatValeu ,root.Left);

            // Recursively get the list from the right subtree
             findDefencPreOrder(threatValeu,root.Right);

        }
    }
}
