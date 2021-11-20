using SharpNeat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neat
{
    public record BrainWithFitness<TPhenome>(TPhenome Phenome, FitnessInfo Fitness);

    /// <summary>
    /// Represents an evaluator that competes two phenomes against each other.
    /// </summary>
    public interface IParallelEvaluator<TPhenome>
    {
        /// <summary>
        /// Gets the total number of individual genome evaluations that have been performed by this evaluator.
        /// </summary>
        ulong EvaluationCount { get; }

        /// <summary>
        /// Gets a value indicating whether some goal fitness has been achieved and that
        /// the the evolutionary algorithm search should stop. This property's value can remain false
        /// to allow the algorithm to run indefinitely.
        /// </summary>
        bool StopConditionSatisfied { get; }

        /// <summary>
        /// Evaluate the provided phenomes and return their fitness scores.
        /// </summary>
        void Evaluate(ref BrainWithFitness<TPhenome>[] brainsWithFitness);

        /// <summary>
        /// Reset the internal state of the evaluation scheme if any exists.
        /// </summary>
        void Reset();
    }
}
