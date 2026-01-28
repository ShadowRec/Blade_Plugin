using Core;
using KompasBuilder;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StresTests
{
    internal class Program
    {
        static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
            var builder = new Builder();
            var parameters = new Parameters();
            var stopWatch = new Stopwatch();
            var currentProcess = Process.GetCurrentProcess();
            var computerInfo = new ComputerInfo();

            parameters.NumericalParameters[ParameterType.BladeLength]
                .Value = 300;

            parameters.SetDependencies(
     ParameterType.BladeLength,
     ParameterType.PeakLenght
 );

            parameters.SetDependencies(
                ParameterType.BladeLength,
                ParameterType.BindingLength
            );

            // Установка ширины по умолчанию
            parameters.NumericalParameters[ParameterType.BladeWidth].Value = 40;

            parameters.SetDependencies(
                ParameterType.BladeWidth,
                ParameterType.EdgeWidth
            );
            //Установка толщины по умолчанию
            parameters.NumericalParameters[ParameterType.BladeThickness]
                .Value = 2;
            //Установка длины острия по умолчанию
            parameters.NumericalParameters[ParameterType.PeakLenght]
                .Value = 45;
            //Установка ширины лезвия по умолчанию
            parameters.NumericalParameters[ParameterType.EdgeWidth]
                .Value = 14;


            //Установка длины крепления по умолчанию
            parameters.NumericalParameters[ParameterType.BindingLength]
                .Value = 300;

            //Установка наличия острия по умолчанию (Есть)
            parameters.BladeExistence = true;

            //Установка типа клинка по умолчанию (Односторонний)
            parameters.BladeType = false;

            //Установка типа крепления по умолчанию (Накладное)
            parameters.BindingType = BindingType.ForOverlays;

            //Установка Длины серрейтора по умолчанию

            parameters.SetDependencies(
             ParameterType.BladeLength,
                ParameterType.SerreitorLength
            );

            parameters.NumericalParameters[
                ParameterType.SerreitorLength]
                .Value = 90;

            parameters.NumericalParameters[
                ParameterType.SerreitorNumber]
                .Value = 10;

            ///Установка типа серрейтора по умолчанию
            parameters.SerreitorType = SerreitorType.AlternationSerreitor;
            parameters.SerreitorExistence = true;

           var streamWriter = new StreamWriter("log.txt");

            int count = 0;
            int countDown = 0;
            const double gigabyteInByte = 1.0 / 1073741824.0;

            // Для расчёта времени 
            TimeSpan lastTotalProcessorTime = currentProcess.TotalProcessorTime;
            DateTime lastTime = DateTime.Now;
            builder.StartCreating();

            while (true)
            {
                //countDown++;
                stopWatch.Start();
                builder.BuildBlade(parameters);
                stopWatch.Stop();
                builder.CLose();
                // ОЗУ
                double usedMemory =
                    (computerInfo.TotalPhysicalMemory -
                    computerInfo.AvailablePhysicalMemory) *
                    gigabyteInByte;

                // Время
                TimeSpan currentTotalProcessorTime =
                    currentProcess.TotalProcessorTime;
                DateTime currentTime = DateTime.Now;

                lastTotalProcessorTime = currentTotalProcessorTime;
                lastTime = currentTime;

                streamWriter.WriteLine(
                    $"{++count}\t" +
                    $"{stopWatch.Elapsed:mm\\:ss\\.fff}\t" +
                    $"{usedMemory:F3}"
                );

                streamWriter.Flush();
                stopWatch.Reset();
            }
        }
    }
}
