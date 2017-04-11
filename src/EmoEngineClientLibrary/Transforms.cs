// Copyright © 2010 James Galasyn 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace EmoEngineClientLibrary
{
    public class Transforms
    {
        ///////////////////////////////////////////////////////////////////////
        #region Implementation

        /// <summary>
        /// Calculates the real discrete Fourier transform for the specified data channel.
        /// </summary>
        /// <param name="channel">The data channel to calculate the Fourier transform for.</param>
        /// <returns>An array that contains the magnitudes of samples in the frequency domain (spectrum)
        /// of the real time-domain signal in <paramref name="channel"/>.</returns>
        /// <remarks>
        /// The <see cref="ComputeRealFourierTransform"/> method calls the <see cref="ComputeFourierTransform"/> method
        /// and extracts the real part of the transformed signal from the returned <see cref="Complex"/> array. The real 
        /// part is computed by using the <see cref="Complex.Magnitude"/> property. The <see cref="Complex.Phase"/> is 
        /// ignored. The first N / 2 samples are returned, which comprise the positive frequencies.   
        /// </remarks>
        //public double[] ComputeRealFourierTransform( EdkDll.EE_DataChannel_t channel )
        //{
        //    Complex[] complexDataArray = this.ComputeFourierTransform( channel );

        //    var magnitudes = complexDataArray.Select( c => c.Magnitude ).Take( complexDataArray.Length / 2 );

        //    return magnitudes.ToArray<double>();
        //}

        public static double[] ComputeRealFourierTransform( double[] data )
        {
            Complex[] complexFourierTransform = ComputeFourierTransform( data );

            return ComputeRealFourierTransform( complexFourierTransform );
        }


        public static double[] ComputeRealFourierTransform( Complex[] complexFourierTransform )
        {
            var magnitudes = complexFourierTransform.Select( c => c.Magnitude ).Take( complexFourierTransform.Length / 2 );

            return magnitudes.ToArray<double>();
        }


        public static Complex[] ComputeFourierTransform( double[] data )
        {
            var complexData = data.Select( d => new Complex( d, 0 ) );
            Complex[] complexDataArray = complexData.ToArray<Complex>();

            Transform.FourierForward( complexDataArray );

            return complexDataArray;
        }

        /// <summary>
        /// Calculates the real discrete inverse Fourier transform for the specified array of frequencies.
        /// </summary>
        /// <param name="complexDataArray">The frequencies to transform.</param>
        /// <returns>An array that contains the real discrete inverse Fourier transform that corresponds 
        /// with <paramref name="complexDataArray"/>. </returns>
        /// <remarks>
        /// The <paramref name="complexDataArray"/> is usually returned by a call to 
        /// the <see cref="ComputeFourierTransform"/> method.
        /// </remarks>
        public static double[] ComputeRealInverseFourierTransform( Complex[] complexDataArray )
        {
            Complex[] inverseFourierArray = ComputeInverseFourierTransform( complexDataArray );

            var doubleData = inverseFourierArray.Select( c => c.Real ).Take( inverseFourierArray.Length / 2 );

            var doubleData2 = doubleData.Reverse();

            var invFFT = doubleData.Concat( doubleData2 );

            return invFFT.ToArray<double>();
        }

        private static Complex[] ComputeInverseFourierTransform( Complex[] complexDataArray )
        {
            Complex[] inverseFourierArray = new Complex[complexDataArray.Length];
            complexDataArray.CopyTo( inverseFourierArray, 0 );

            Transform.FourierInverse( inverseFourierArray );

            return inverseFourierArray;
        }

        #endregion
    }
}