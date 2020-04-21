﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using System;
using Vortex.Cost.Utility;
using Vortex.Regularization.Utility;
using Nomad.Matrix;

namespace Vortex.Cost
{
    /// <summary>
    /// "Generalized Kullback Leibler Divergence": also known as "Bregman divergence"
    /// </summary>
    public class GeneralizedKullbackLeiblerDivergence : Utility.Cost
    {
        public GeneralizedKullbackLeiblerDivergence(GeneralizedKullbackLeiblerDivergenceSettings settings) : base(settings) { }

        public override double Forward(Matrix Actual, Matrix Expected, int layerCount)
        {
            double error = 0.0;

            for (int i = 0; i < Actual.Rows; i++)
            {
                for (int j = 0; j < Actual.Columns; j++)
                {
                    error += Expected[i, j] * Math.Log(Expected[i, j] / Actual[i, j]) - Expected[i, j] + Actual[i, j];
                }
            }

            BatchCost += error;
            return error;
        }

        public override Matrix Backward(Matrix Actual, Matrix Expected, int layerCount)
        {
            Matrix gradMatrix = Actual.Duplicate();

            for (int i = 0; i < Actual.Rows; i++)
            {
                for (int j = 0; j < Actual.Columns; j++)
                {
                    gradMatrix[i, j] = (Actual[i, j] - Expected[i, j]) / Actual[i, j];
                }
            }

            return gradMatrix;
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override ECostType Type()
        {
            return ECostType.GeneralizedKullbackLeiblerDivergence;
        }
    }

    public class GeneralizedKullbackLeiblerDivergenceSettings : CostSettings { }
}