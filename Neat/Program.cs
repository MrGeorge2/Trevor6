/* ***************************************************************************
 * This file is part of the NashCoding tutorial on SharpNEAT 2.
 * 
 * Copyright 2010, Wesley Tansey (wes@nashcoding.com)
 * 
 * Some code in this file may have been copied directly from SharpNEAT,
 * for learning purposes only. Any code copied from SharpNEAT 2 is 
 * copyright of Colin Green (sharpneat@gmail.com).
 *
 * Both SharpNEAT and this tutorial are free software: you can redistribute
 * it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the 
 * License, or (at your option) any later version.
 *
 * SharpNEAT is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with SharpNEAT.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using log4net.Config;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System.Threading.Tasks;
using System.Threading;
using Neat;

namespace TicTacToeCoevolution
{
    class Program
    {
        static StockMarketParallelExperiment experiment;
        static NeatEvolutionAlgorithm<NeatGenome> _ea;
        const string CHAMPION_FILE = @"E:\Projects\Trevor6\Champs\trader_champion.xml";

        static void Main(string[] args)
        {

            // Experiment classes encapsulate much of the nuts and bolts of setting up a NEAT search.
            experiment = new StockMarketParallelExperiment();

            experiment.Initialize("TicTacToe", null);

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
            // Save the best genome to file
            var champDoc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
            champDoc.Save(CHAMPION_FILE);

            // Save the best genome to file
            var populationDoc = NeatGenomeXmlIO.SaveComplete(_ea.GenomeList, false);
            populationDoc.Save(experiment.POPULATION_LOCATION);
        }
    }
}
