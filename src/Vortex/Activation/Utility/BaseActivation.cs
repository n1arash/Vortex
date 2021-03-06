﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using Nomad.Matrix;

namespace Vortex.Activation.Utility
{
    public abstract class BaseActivationKernel
    {
        public BaseActivation Settings { get; }

        protected BaseActivationKernel(BaseActivation settings)
        {
            Settings = settings;
        }

        public abstract Matrix Forward(Matrix input);

        public abstract Matrix Backward(Matrix input);

        protected abstract double Activate(double input);

        protected abstract double Derivative(double input);

        public abstract EActivationType Type();
    }

    public abstract class BaseActivation
    {
        public abstract EActivationType Type();
    }
}
