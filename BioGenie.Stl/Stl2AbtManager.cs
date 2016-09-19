using System;
using System.Collections.Generic;
using System.IO;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl
{
    public class Stl2AbtManager
    {
        public string StlFileName { get; set; }
        public string AbtFileName { get; set; }
        public Dictionary<float, List<Vertex>> AbtBoundary { get; set; }
        public Dictionary<float, List<Vertex>> Geratrizes { get; set; }
        public StlDocument StlDocument { get; set; }
        public StlAbutment StlAbutment { get; set; }

        public Stl2AbtManager()
        {
            AbtBoundary = new Dictionary<float, List<Vertex>>();
            Geratrizes = new Dictionary<float, List<Vertex>>();
            ReadStlFile();
        }

        public Stl2AbtManager(string stlFileName, string abtFileName)
        {
            StlFileName = stlFileName;
            AbtFileName = abtFileName;

            AbtBoundary = new Dictionary<float, List<Vertex>>();
            Geratrizes = new Dictionary<float, List<Vertex>>();
            ReadStlFile();
        }

        private void ReadStlFile()
        {
            try
            {
                using (var reader = new StreamReader(StlFileName))
                {
                    StlDocument = StlDocument.Read(reader);
                }
            }
            catch (FormatException)
            {
                using (var reader = new BinaryReader(File.Open(StlFileName, FileMode.Open)))
                {
                    StlDocument = StlDocument.Read(reader);
                }
            }
            StlAbutment = new StlAbutment(StlDocument);
            StlAbutment.AlignAndCenterAbutment();
        }

        public void GenerateModel(int resAngular)
        {
            Geratrizes = new AngularBoundaryPacker(StlAbutment, resAngular).GetBoundaries(true);
            AbtBoundary = new Abt(Geratrizes).GetPoints();
        }
    }
}