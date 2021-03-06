﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using Nomad.Matrix;

namespace Vortex.Optimizer.Utility
{
    public abstract class BaseOptimizerKernel
    {
        public double Alpha { get; set; }
        protected BaseOptimizerKernel(BaseOptimizer settings) { Alpha = settings.Alpha; }
        public abstract Matrix CalculateDeltaW(Matrix w, Matrix dJdW);
        public abstract Matrix CalculateDeltaB(Matrix b, Matrix dJdB);
        public abstract EOptimizerType Type();
    }

    public abstract class BaseOptimizer
    {
        public double Alpha { get; set; }
        public abstract EOptimizerType Type();

        protected BaseOptimizer(double alpha)
        {
            Alpha = alpha;
        }
    }
}
