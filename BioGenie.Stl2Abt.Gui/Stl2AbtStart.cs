using System.Windows.Forms;

namespace BioGenie.Stl2Abt.Gui
{
    public static class Stl2AbtStart
    {
        public static void Stl2AbtMainFormStart(string stlFileName, string abtFileName = null)
        {
            if (abtFileName == null)
            {
                var pos = stlFileName.LastIndexOf('.');
                abtFileName = (pos == -1 ? stlFileName : stlFileName.Substring(0, pos)) + ".ABT";
            }
            var mainForm = new Stl2AbtMainForm(stlFileName, abtFileName);
            mainForm.Show();
            Application.Run(mainForm);
        }
    }
}