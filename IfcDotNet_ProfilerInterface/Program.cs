
using System;
using System.IO;
using System.Globalization;

using IfcDotNet.Schema;
using IfcDotNet.StepSerializer;

namespace IfcDotNet_ProfilerInterface
{
	class Program
	{
		public static void Main(string[] args)
		{
			string inputFile = "../../../IfcDotNet_UnitTests/sampleData/NIST_TrainingStructure_param.ifc";
			if(!File.Exists(inputFile)){
				Console.WriteLine(String.Format(CultureInfo.InvariantCulture,
				                                "File does not exist at : {0}", inputFile));
			}else{
				Console.WriteLine("Running...");
				StreamReader sr = new StreamReader(inputFile);
				StepReader reader = new StepReader( sr );
				IfcStepSerializer serializer = new IfcStepSerializer();
				
				iso_10303 iso10303 = serializer.Deserialize( reader );
				uos1 uos1 = iso10303.uos as uos1;
				Entity[] entities = uos1.Items;
				
				Console.WriteLine(String.Format(CultureInfo.InvariantCulture,
				                                "Have deserialized {0} entities", entities.Length));
			}
			Console.WriteLine("Press any key to quit");
			Console.ReadKey();
			Console.WriteLine("Exiting");
		}
	}
}