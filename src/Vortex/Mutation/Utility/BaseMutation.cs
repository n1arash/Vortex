﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using System;

namespace Vortex.Mutation.Utility
{
    public abstract class BaseMutationKernel
    {
        public Random RNG { get; }

        public BaseMutationKernel()
        {
            RNG = new Random();
        }

        public abstract double Mutate(double value);

        public abstract EMutationType Type();
    }

    public abstract class BaseMutation
    {
        public abstract EMutationType Type();
    }
}
