using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;

namespace Firestore
{
    class Program
    {
  
         
       public  static void Main(string[] args)
        {
                   IFirebaseConfig config = new FirebaseConfig
                  {
                      AuthSecret = "RuACwgmFjFnMGMGvLPNbqAwhJkZRmI9DTZqrkaE7",
                      BasePath = "https://hip-voyager-241415.firebaseio.com"


                  };
        var people = new People { Name = "Tom", Designation = "TechLead", Age = 25 };

            IFirebaseClient client = new FirebaseClient( config);
            _ = client.Push("People/", people);
        }
      
        public class People
        {
            public string Name { get; set; }
            public string Designation { get; set; }
            public int Age { get; set; }
        }
    }
}
