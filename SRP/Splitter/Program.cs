namespace Splitter
{
    class Program
    {

        static void Main( string[] args )
        {
            // Initialise
            Application application = new Application();

            application.initialise( args );

            application.start();

            application.clear();
        }

    }

} // End namespace
