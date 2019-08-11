using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Threading;
using DataStruct.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DataStruct.Utility
{
    public static class Utility
    {

        [ThreadStatic] private static Random Local;
        static Random random = new Random();

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }

        //Yates Shuffle Algorithm
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Utility.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //Generate price for products
        public static decimal GenerateRandomPrice()
        {
            decimal price = 0m;

            price = Decimal.Parse((random.Next(20, 101)).ToString());

            return price;
        }


        public static List<string> GetLectureNames()
        {
            var lectureNames = new List<string>()
            {
                  "Array",
                  "BinaryTree",
                  "Graph",
                  "HashTable",
                  "Heap",
                  "LinkedList",
                  "Queue",
                  "Set",
                  "Stack",
                  "Tree"
            };

            return lectureNames;
        }

        public static List<string> GetLecturePhotoAddresses()
        {
            var lecturePhotoAddresses = new List<string>()
            {
              "/Photo/Array.png",
              "/Photo/BinaryTree.png",
              "/Photo/Graph.png",
              "/Photo/HashTable.png",
              "/Photo/Heap.png",
              "/Photo/LinkedList.png",
              "/Photo/Queue.png",
              "/Photo/Set.png",
              "/Photo/Stack.png",
              "/Photo/Tree.jpg"
            };

            return lecturePhotoAddresses;
        }

        public static string GetFileData(string fileName)
        {
            string data = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + fileName);

            return data;
        }

        public static Dictionary<string, string> GetJSON(string jFileName)
        {
            
            var jsonString = @GetFileData(jFileName);

            var JSONObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
           

            return JSONObj;

        }

            
    }
}











