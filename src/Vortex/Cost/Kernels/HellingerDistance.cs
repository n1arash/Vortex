﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using System;
using Vortex.Cost.Utility;
using Nomad.Matrix;

namespace Vortex.Cost.Kernels
{
    /// <summary>
    /// "Hellinger Distance": needs to have positive values, and ideally values between 0 and 1. The same is true for the following divergences.
    /// </summary>
    public class HellingerDistanceKernel : BaseCostKernel
    {
        public HellingerDistanceKernel(HellingerDistance settings = null) : base(settings) { }
        
        public override double Forward(Matrix actual, Matrix expected)
        {
            double error = 0.0;

            for (int i = 0; i < actual.Rows; i++)
            {
                for (int j = 0; j < actual.Columns; j++)
                {
                    error += Math.Pow((Math.Sqrt(actual[i, j]) - Math.Sqrt(expected[i, j])), 2);
                }
            }

            error *= (1 / Math.Sqrt(2));
            
            BatchCost += error;

            return error;
        }

        public override Matrix Backward(Matrix actual, Matrix expected)
        {
            if (actual.Rows != expected.Rows || actual.Columns != expected.Columns)
            {
                throw new ArgumentException("Actual Matrix does not have the same size as The Expected Matrix");
            }

            Matrix gradMatrix = actual.Duplicate();

            for (int i = 0; i < actual.Rows; i++)
            {
                for (int j = 0; j < actual.Columns; j++)
                {
                    gradMatrix[i, j] = (Math.Sqrt(actual[i, j]) - Math.Sqrt(expected[i, j])) / (Math.Sqrt(2) * Math.Sqrt(actual[i, j]));
                }
            }

            return gradMatrix;
        }

        public override ECostType Type() => ECostType.HellingerDistance;
    }

    public class HellingerDistance : BaseCost 
    {
        public override ECostType Type() => ECostType.HellingerDistance;
    }
}
