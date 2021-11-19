﻿using log4net.Config;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System.Xml;

namespace Trevor6.Neat;


public class Neat
{
    static NeatEvolutionAlgorithm<NeatGenome> _ea;
    const string CHAMPION_FILE = @"E:\Projects\Trevor6\Champs\trader_champion.xml";

    public static void Main()
    {
        // Initialise log4net (log to console).
        //XmlConfigurator.Configure(new FileInfo("log4net.properties"));

        // Experiment classes encapsulate much of the nuts and bolts of setting up a NEAT search.
        var experiment = new StockMarketExperiment();

        // Load config XML.
        XmlDocument xmlConfig = new XmlDocument();
        xmlConfig.Load(@"E:\Projects\Trevor6\Trevor6\Neat\stockMarket.config.xml");
        experiment.Initialize("StockMarket", xmlConfig.DocumentElement);

        // Create evolution algorithm and attach update event.
        _ea = experiment.CreateEvolutionAlgorithm();
        _ea.UpdateEvent += new EventHandler(ea_UpdateEvent);

        // Start algorithm (it will run on a background thread).
        _ea.StartContinue();

        // Hit return to quit.
        Console.ReadLine();
    }

    static void ea_UpdateEvent(object sender, EventArgs e)
    {
        Console.WriteLine(string.Format("gen={0:N0} bestFitness={1:N6}", _ea.CurrentGeneration, _ea.Statistics._maxFitness));

        // Save the best genome to file
        var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
        doc.Save(CHAMPION_FILE);
    }
}

