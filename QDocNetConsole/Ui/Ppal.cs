// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetConsole.Ui {
    using System;
    using System.IO;
    using QDocNetLib;
    using QDocNetLib.Persistence;

    class Ppal {
        static void Main(String[] args)
        {
            try {
                Console.WriteLine( "\nQDocNetConsole - Docs generator" );
                Console.WriteLine( LibInfo.Header + "\n" );
                
                if ( args.Length == 1 ) {
                    Unit unit = Helper.LoadDocs( args[ 0 ] );
                    
                    Console.WriteLine( "Read... {0}", unit.Name );
                    Console.WriteLine( "Creating..." );

                    new HtmlExporter( unit, "." ).SaveTo();
                }
            } catch(IOException exc)
            {
                Console.Error.WriteLine( "\nFile error: " + exc.Message );
            }
            catch(ApplicationException exc)
            {
                Console.Error.WriteLine( "\nError: " + exc.Message );
            }
            catch(Exception exc)
            {
                Console.Error.WriteLine( "\nCritical error: " + exc.Message );
            }
            finally {
                Console.WriteLine( "\nEnd." );
            }
        }
    }
}
