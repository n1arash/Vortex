﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using Nomad.Matrix;
using Vortex.Optimizer.Utility;

namespace Vortex.Optimizer
{
    public sealed class GradientDescent : Utility.BaseOptimizer
    {
        public GradientDescent(GradientDescentSettings settings) : base(settings)
        {
        }

        public override Matrix CalculateDelta(Matrix X, Matrix dJdX)
        {
            return (Alpha * (X.Hadamard(dJdX)));
        }

        public override EOptimizerType Type() => EOptimizerType.GradientDescent;
    }

    public sealed class GradientDescentSettings : OptimizerSettings
    {
        public override EOptimizerType Type() => EOptimizerType.GradientDescent;

        public GradientDescentSettings(double alpha) : base(alpha)
        {
        }
    }
}