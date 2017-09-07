using System;
using QDocNetLib;

namespace QDocNetConsole.Ui {
    class Ppal {
        static void Main(String[] args)
        {
            Entity rootEntity = Helper.LoadFromFile( "QDocNetLib.xml" );
            Console.WriteLine( rootEntity );
        }
    }
}
