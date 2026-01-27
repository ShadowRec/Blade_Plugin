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
                parameters.NumericalParameters[ParameterType.BladeLength],
                parameters.NumericalParameters[ParameterType.PeakLenght],
                1.0 / 6.0, 1.0 / 10
            );

            parameters.SetDependencies(
                parameters.NumericalParameters[ParameterType.BladeLength],
                parameters.NumericalParameters[ParameterType.BindingLength],
                1, 0
            );

            //Установка ширины по умолчанию
            parameters.NumericalParameters[ParameterType.BladeWidth]
                .Value = 40;
            parameters.SetDependencies(
                parameters.NumericalParameters[ParameterType.BladeWidth],
                parameters.NumericalParameters[ParameterType.EdgeWidth],
                3.0 / 6.0, 1.0 / 6.0
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
                parameters.NumericalParameters[
                    ParameterType.BladeLength],
                parameters.NumericalParameters[
                    ParameterType.SerreitorLength],
                5.0 / 10.0, 3 / 10.0
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

            while (true)
            {
                countDown++;
                stopWatch.Start();
                builder.BuildBlade(parameters);
                stopWatch.Stop();
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
