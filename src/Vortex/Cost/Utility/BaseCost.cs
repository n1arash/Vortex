﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using System;
using Vortex.Regularization;
using Vortex.Regularization.Utility;
using Nomad.Matrix;

namespace Vortex.Cost.Utility
{
    /// <summary>
    /// A cost function is a measure of "how good" a neural network did with respect to it's given training sample and the expected output. 
    /// It also may depend on variables such as weights and biases.
    /// A cost function is a single value, not a vector, because it rates how good the neural network did as a whole.
    /// </summary>
    public abstract class BaseCostKernel
    {
        protected BaseCostKernel(BaseCost settings) { }
        
        public abstract double Forward(Matrix actual, Matrix expected);

        public abstract Matrix Backward(Matrix actual, Matrix expected);

        public abstract ECostType Type();

        public double BatchCost { get; protected set; }

        public virtual void ResetCost() { BatchCost = 0; }
    }

    public abstract class BaseCost
    {
        public abstract ECostType Type();
    }
}
