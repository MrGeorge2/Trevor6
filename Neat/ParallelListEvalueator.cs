using SharpNeat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neat
{

    /// <summary>
    /// A concrete implementation of IGenomeListEvaluator that evaulates genomes 
    /// in parallel (on multiple execution threads). Genomes are pitted against
    /// each other in a head-to-head matchup.
    /// 
    /// Genome decoding is performed by a provided IGenomeDecoder.
    /// Phenome evaluation is performed by a provided IPhenomeEvaluator.
    /// </summary>
    public class ParallelListEvaluator<TGenome, TPhenome> : IGenomeListEvaluator<TGenome>
        where TGenome : class, global::SharpNeat.Core.IGenome<TGenome>
        where TPhenome : class
    {
        readonly IGenomeDecoder<TGenome, TPhenome> _genomeDecoder;
        readonly IParallelEvaluator<TPhenome> _phenomeEvaluator;
        readonly ParallelOptions _parallelOptions;

        #region Constructors

        /// <summary>
        /// Construct with the provided IGenomeDecoder and ICoevolutionPhenomeEvaluator. 
        /// The number of parallel threads defaults to Environment.ProcessorCount.
        /// </summary>
        public ParallelListEvaluator(IGenomeDecoder<TGenome, TPhenome> genomeDecoder,
                                           IParallelEvaluator<TPhenome> phenomeEvaluator)
            : this(genomeDecoder, phenomeEvaluator, new ParallelOptions())
        {
        }

        /// <summary>
        /// Construct with the provided IGenomeDecoder, ICoevolutionPhenomeEvaluator and ParalleOptions.
        /// The number of parallel threads defaults to Environment.ProcessorCount.
        /// </summary>
        public ParallelListEvaluator(IGenomeDecoder<TGenome, TPhenome> genomeDecoder,
                                           IParallelEvaluator<TPhenome> phenomeEvaluator,
                                           ParallelOptions options)
        {
            _genomeDecoder = genomeDecoder;
            _phenomeEvaluator = phenomeEvaluator;
            _parallelOptions = options;
        }
        #endregion

        #region IGenomeListEvaluator<TGenome> Members

        /// <summary>
        /// Gets the total number of individual genome evaluations that have been performed by this evaluator.
        /// </summary>
        public ulong EvaluationCount
        {
            get { return _phenomeEvaluator.EvaluationCount; }
        }

        /// <summary>
        /// Gets a value indicating whether some goal fitness has been achieved and that
        /// the the evolutionary algorithm/search should stop. This property's value can remain false
        /// to allow the algorithm to run indefinitely.
        /// </summary>
        public bool StopConditionSatisfied
        {
            get { return _phenomeEvaluator.StopConditionSatisfied; }
        }

        /// <summary>
        /// Reset the internal state of the evaluation scheme if any exists.
        /// </summary>
        public void Reset()
        {
            _phenomeEvaluator.Reset();
        }

        /// <summary>
        /// Main genome evaluation loop with no phenome caching (decode on each evaluation).
        /// Individuals are competed pairwise against every other individual in the population.
        /// Evaluations are summed to get the final genome fitness.
        /// </summary>
        public void Evaluate(IList<TGenome> genomeList)
        {
            List<BrainWithFitness<TPhenome>> brainsWithFitness = new List<BrainWithFitness<TPhenome>>();

            foreach(var genome in genomeList)
            {
                TPhenome phenome = _genomeDecoder.Decode(genome);
                
                if (phenome == null)
                    continue;

                brainsWithFitness.Add(new BrainWithFitness<TPhenome>(phenome, FitnessInfo.Zero));
            }

            var brainsWithFitnessArray = brainsWithFitness.ToArray();
            _phenomeEvaluator.Evaluate(ref brainsWithFitnessArray);

            // Update every genome in the population with its new fitness score.
            for (int i = 0; i < genomeList.Count; i++)
            {
                TPhenome phenome = _genomeDecoder.Decode(genomeList[i]);

                if (phenome == null)
                    continue;

                genomeList[i].EvaluationInfo.SetFitness(brainsWithFitnessArray[i].Fitness._fitness);
            }
        }

        #endregion
    }
}
