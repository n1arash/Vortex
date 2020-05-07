﻿// Copyright © 2020 Void-Intelligence All Rights Reserved.

using System;
using Vortex.Activation.Kernels;
using Vortex.Activation.Utility;
using Vortex.Regularization;
using Vortex.Regularization.Utility;
using Nomad.Matrix;
using System.Collections.Generic;
using Vortex.Optimizer.Utility;
using Vortex.Regularization.Kernels;

namespace Vortex.Layer.Utility
{
    /// <summary>
    /// Base of all Layer classes
    /// </summary>
    public abstract class BaseLayerKernel
    {
        public float RegularizationValue { get; protected set; }
        public BaseLayer Settings { get; }
        public int NeuronCount { get; }
        public BaseActivationKernel ActivationFunction { get; }
        public BaseRegularizationKernel RegularizationFunction { get; }
        public BaseOptimizerKernel OptimizerFunction { get; }
        public Dictionary<string, Matrix> Params { get; }
        public Dictionary<string, Matrix> Grads { get; }

        protected BaseLayerKernel(BaseLayer layerSettings, BaseOptimizerKernel optimizer)
        {
            RegularizationValue = 0.0f;

            OptimizerFunction = optimizer;

            Params = new Dictionary<string, Matrix>();

            Grads = new Dictionary<string, Matrix>();

            Settings = layerSettings;

            NeuronCount = Settings.NeuronCount;

            // Activation Setup
            ActivationFunction = (Settings.ActivationFunctionSettings.Type()) switch
            {
                EActivationType.Arctan => new ArctanKernel(),
                EActivationType.BinaryStep => new BinaryStepKernel(),
                EActivationType.BipolarSigmoid => new BipolarSigmoidKernel(),
                EActivationType.Elu => new EluKernel((Elu)Settings.ActivationFunctionSettings),
                EActivationType.Exponential => new ExponentialKernel(),
                EActivationType.HardSigmoid=> new HardSigmoidKernel(),
                EActivationType.HardTanh=> new HardTanhKernel(),
                EActivationType.Identity=> new IdentityKernel(),
                EActivationType.Loggy => new LoggyKernel(),
                EActivationType.Logit => new LogitKernel(),
                EActivationType.LRelu => new LReluKernel((LRelu)Settings.ActivationFunctionSettings),
                EActivationType.Mish => new MishKernel(),
                EActivationType.Relu => new ReluKernel(),
                EActivationType.Selu => new SeluKernel(),
                EActivationType.Sigmoid => new SigmoidKernel(),
                EActivationType.Softmax => new SoftmaxKernel(),
                EActivationType.Softplus => new SoftplusKernel(),
                EActivationType.Softsign => new SoftsignKernel(),
                EActivationType.Swish => new SwishKernel(),
                EActivationType.Tanh => new TanhKernel(),
                _ => throw new ArgumentException("Activation Type Invalid."),
            };

            // Regularization Setup
            RegularizationFunction = (Settings.RegularizationFunctionSettings.Type()) switch
            {
                ERegularizationType.None => new NoneKernel((None)Settings.RegularizationFunctionSettings),
                ERegularizationType.L1 => new L1Kernel((L1)Settings.RegularizationFunctionSettings),
                ERegularizationType.L2 => new L2Kernel((L2)Settings.RegularizationFunctionSettings),
                _ => throw new ArgumentException("Regularization Type Invalid."),
            };
        }

        // All Layer Forward Calculations
        public abstract Matrix Forward(Matrix inputs);

        // All Layer Backward Calculations
        public abstract Matrix Backward(Matrix dA);

        // All Layer Optimization Calculations
        public abstract void Optimize();

        public abstract ELayerType Type();
    }

    public abstract class BaseLayer
    {
        public int NeuronCount { get; }
        public Activation.Utility.BaseActivation ActivationFunctionSettings { get; }
        public Regularization.Utility.Regularization RegularizationFunctionSettings { get; }

        protected BaseLayer(int neuronCount, Activation.Utility.BaseActivation activationSettings, Regularization.Utility.Regularization regularizationSettings) 
        {
            NeuronCount = neuronCount;
            ActivationFunctionSettings = activationSettings;
            RegularizationFunctionSettings = regularizationSettings;
        }

        public abstract ELayerType Type();
    }
}
