using SaDick.Forms;

namespace SaDick
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new StartingForm());

            switch (UserInformation.role)
            {
                //Admin
                case 0:
                    Application.Run(new AdminForm());
                    break;
                //Educator
                case 1:
                    Application.Run(new EducatorForm());
                    break;
                //Parent
                case 2:
                    Application.Run(new ParentForm());
                    break;
                default:
                    Application.Exit();
                    break;

            }
        }
    }
}