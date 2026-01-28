using Core;
using Kompas6API5;
using KompasAPI7;
using KompasBuilder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// Класс формы главного меню.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Конструктор класса MainForm где расставляются все компоненты.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Объект Builder.
        /// </summary>
        private Builder _builder;

        /// <summary>
        /// Параметры клинка.
        /// </summary>
        private Parameters _parameters;

        /// <summary>
        /// Словарь, содержащий текущие значения параметров.
        /// </summary>
        private Dictionary<ParameterType, double> _currentParameters =
            new Dictionary<ParameterType, double>()
            {
                [ParameterType.BladeLength] = 300,
                [ParameterType.BladeWidth] = 40,
                [ParameterType.BladeThickness] = 2,
                [ParameterType.EdgeWidth] = 14,
                [ParameterType.PeakLenght] = 45,
                [ParameterType.BindingLength] = 300,
                [ParameterType.SerreitorLength] = 90,
                [ParameterType.SerreitorNumber] = 8
            };

        /// <summary>
        /// Словарь текстовых описаний параметров для сообщений об ошибках.
        /// </summary>
        private Dictionary<ParameterType, string> _parametersStrings =
            new Dictionary<ParameterType, string>()
            {
                [ParameterType.BladeLength] = "В поле 'Длина клинка'",
                [ParameterType.BladeWidth] = "В поле 'Ширина клинка'",
                [ParameterType.BladeThickness] = "В поле 'Толщина клинка'",
                [ParameterType.EdgeWidth] = "В поле 'Ширина лезвия'",
                [ParameterType.PeakLenght] = "В поле 'Длина острия'",
                [ParameterType.BindingLength] = "В поле 'Длина крепление'",
                [ParameterType.SerreitorLength] = "В поле 'Длина серрейтора'",
                [ParameterType.SerreitorNumber] = "В поле 'Количество зубьев'"
            };

        /// <summary>
        /// Словарь текстовых описаний исключений.
        /// </summary>
        private Dictionary<ExceptionType, string> _exceptionsStrings =
            new Dictionary<ExceptionType, string>()
            {
                [ExceptionType.NullException] = "не было значения!",
                [ExceptionType.InvalidException] = "было введено " +
                    "некорректное значение!",
                [ExceptionType.TooSmallException] = "было введено " +
                    "значение меньше диапазона допустимых значений!",
                [ExceptionType.TooBigException] = "было введено " +
                    "значение больше диапазона допустимых значений!",
                [ExceptionType.RatioNegativeException] = "возникла " +
                    "ошибка на уровне кода, введенное соотношение" +
                " ниже нуля!",
                [ExceptionType.MaxValueNegativeException] = "возникла " +
                    "ошибка на уровне кода, максимальное значение " +
                "ниже нуля!",
                [ExceptionType.MinValueNegativeException] = "возникла " +
                    "ошибка на уровне кода, минимальное значение" +
                " ниже нуля!",
                [ExceptionType.KompasOpenErrorException] = "Возникла " +
                    "ошибка при попытке открытия Kompas-3D",
                [ExceptionType.MinGreaterMaxException] = "возникла " +
                    "ошибка на уровне кода, минимальное значение " +
                    "больше максимального!",
                [ExceptionType.PartBuildingErrorException] = "Возникла " +
                    "ошибка при попытке построить деталь!"
            };

        /// <summary>
        /// Словарь связывающий тип параметра с TextBox.
        /// </summary>
        private Dictionary<ParameterType, Control> _parametersTextBoxes;

        /// <summary>
        /// Словарь связывающий тип параметра с Label.
        /// </summary>
        private Dictionary<ParameterType, Control> _parametersLabels;

        /// <summary>
        /// Словарь соотношений между параметрами.
        /// </summary>
        private Dictionary<(ParameterType, ParameterType), (double, double)>
            _parametersRatios = new Dictionary<(ParameterType, ParameterType),
                (double, double)>()
            {
                [(ParameterType.BladeLength, ParameterType.PeakLenght)]
                    = (1.0 / 6.0, 1.0 / 10),
                [(ParameterType.BladeLength, ParameterType.BindingLength)]
                    = (1, 0),
                [(ParameterType.BladeWidth, ParameterType.EdgeWidth)] =
                    (3.0 / 6.0, 1.0 / 6.0),
                [(ParameterType.BladeLength, 
                    ParameterType.SerreitorLength)] =
                    (5.0 / 10.0, 3 / 10.0),
            };

        /// <summary>
        /// Словарь соотношений для типов креплений.
        /// </summary>
        private Dictionary<BindingType, (double, double)> _bindingRatios =
            new Dictionary<BindingType, (double, double)>()
            {
                [BindingType.ForOverlays] = (1, 0),
                [BindingType.Insert] = (3.0 / 4.0, 0),
                [BindingType.Through] = (1, 0),
                [BindingType.None] = (1, 0)
            };

        /// <summary>
        /// Функция, выполняющая действия при запуске программы.
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            _builder = new Builder();
            _parametersTextBoxes = new Dictionary<ParameterType, Control>()
            {
                [ParameterType.BladeLength] = TextBoxLength,
                [ParameterType.BladeWidth] = TextBoxWidth,
                [ParameterType.BladeThickness] = TextBoxBladeThickness,
                [ParameterType.EdgeWidth] = TextBoxEdgeWidth,
                [ParameterType.PeakLenght] = TextBoxPeakLength,
                [ParameterType.BindingLength] = TextBoxBindingLength,
                [ParameterType.SerreitorLength] = SerreitorLengthTextBox,
                [ParameterType.SerreitorNumber] = SerreitorNumberTextBox,
            };

            _parametersLabels = new Dictionary<ParameterType, Control>()
            {
                [ParameterType.BladeLength] = BladeLengthLabel,
                [ParameterType.BladeWidth] = BladeWidthLabel,
                [ParameterType.BladeThickness] = BladeThickLabel,
                [ParameterType.EdgeWidth] = EdgeWidthLabel,
                [ParameterType.PeakLenght] = PeakLengthLabel,
                [ParameterType.BindingLength] = BindingLabel,
                [ParameterType.SerreitorLength] = SerreitorLengthLabel,
                [ParameterType.SerreitorNumber] = SerreitorNumberLabel,
            };

            _parameters = new Parameters();

            // Установка длины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeLength]
                .Value = 300;
            SetDependenciesAndUpdate(ParameterType.BladeLength,
                ParameterType.PeakLenght);
            SetDependenciesAndUpdate(ParameterType.BladeLength,
                ParameterType.BindingLength);

            _parameters.NumericalParameters[ParameterType.BladeWidth]
                .Value = 40;
            SetDependenciesAndUpdate(ParameterType.BladeWidth,
                ParameterType.EdgeWidth);

            // Установка толщины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeThickness]
                .Value = 2;

            // Установка длины острия по умолчанию
            _parameters.NumericalParameters[ParameterType.PeakLenght]
                .Value = 46;

            // Установка ширины лезвия по умолчанию
            _parameters.NumericalParameters[ParameterType.EdgeWidth]
                .Value = 14;

            // Установка длины крепления по умолчанию
            _parameters.NumericalParameters[ParameterType.BindingLength]
                .Value = 300;

            // Установка наличия острия по умолчанию (Есть)
            _parameters.BladeExistence = true;

            _parameters.NumericalParameters[
                ParameterType.SerreitorNumber].Value = 8;

            // Установка типа клинка по умолчанию (Односторонний)
            _parameters.BladeType = false;

            // Установка типа крепления по умолчанию (Накладное)
            _parameters.BindingType = BindingType.ForOverlays;
            ComboBoxTypeBlade.SelectedIndex = 0;
            ComboBoxTypeBinding.SelectedIndex = 2;
            CheckBoxPeakBlade.Checked = true;

            // Установка Длины серрейтора по умолчанию
            SetDependenciesAndUpdate(ParameterType.BladeLength,
                ParameterType.SerreitorLength);
            _parameters.NumericalParameters[ParameterType.SerreitorLength]
                .Value = 90;

            // Установка типа серрейтора по умолчанию
            _parameters.SerreitorType = SerreitorType.AlternationSerreitor;
            SerreitorTypeComboBox.SelectedIndex = 0;
            _parameters.SerreitorExistence = true;
            serreitorCheckBox.Checked = true;
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с поля "Длина клинка".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxLengthLeave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.BladeLength,
               new ParameterType[3]
               {
                   ParameterType.BindingLength,
                   ParameterType.PeakLenght,
                   ParameterType.SerreitorLength
               });
        }

        /// <summary>
        /// Обновления текста ToolTip.
        /// </summary>
        /// <param name="target">Ссылка на поле, 
        /// чей ToolTip обновляется.</param>
        /// <param name="maxvalue">Максимальное 
        /// значение допустимого диапазона.</param>
        /// <param name="minvalue">Минимальное 
        /// значение допустимого диапазона.</param>
        private void UpdateToolTip(object target, double maxvalue,
            double minvalue)
        {
            Max_Min_Value.SetToolTip((Control)target,
                $"Допустимые значения:{minvalue}..{maxvalue}");
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с поля "Длина острия".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxPeakLengthLeave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.PeakLenght,
               new ParameterType[0]);
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с поля "Длина крепления".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxBindingLengthLeave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.BindingLength,
               new ParameterType[0]);
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с поля "Ширина клинка".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxWidthLeave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.BladeWidth,
               new ParameterType[1]
               {
                   ParameterType.EdgeWidth
               });
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с поля "Ширина лезвия".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxEdgeWidthLeave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.EdgeWidth,
                new ParameterType[0]);
        }

        /// <summary>
        /// Функция, выполняющая действия при изменении индекса 
        /// в комбо-боксе "Тип Крепления".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void ComboBoxTypeSelectedIndexChanged(object sender,
            EventArgs e)
        {
            if (ComboBoxTypeBinding.SelectedIndex != -1)
            {
                _parameters.BindingType =
                    (BindingType)ComboBoxTypeBinding.SelectedIndex;

                _parametersRatios[(ParameterType.BladeLength,
                    ParameterType.BindingLength)] =
                    _bindingRatios[(BindingType)
                    ComboBoxTypeBinding.SelectedIndex];

                if ((BindingType)ComboBoxTypeBinding.SelectedIndex
                    == BindingType.None)
                {
                    TextBoxBindingLength.ReadOnly = true;
                    Max_Min_Value.SetToolTip(TextBoxBindingLength,
                        "У клинка выбрано отсутствие крепления");
                }

                if (_parameters.NumericalParameters[
                    ParameterType.BladeLength].Value != 0)
                {
                    SetDependenciesAndUpdate(ParameterType.BladeLength,
                        ParameterType.BindingLength);
                    TryParseTextBox(ParameterType.BindingLength);
                    TextBoxBindingLength.ReadOnly = false;
                }
                SetDefault();
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с комбо-бокса "Тип клинка".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void ComboBoxTypeBladeLeave(object sender, EventArgs e)
        {
            if (ComboBoxTypeBlade.SelectedIndex != -1)
            {
                _parameters.BladeType =
                    (ComboBoxTypeBlade.SelectedIndex == 1);
            }
            SetDefault();
        }

        /// <summary>
        /// Функция, выполняющая действия при изменении статуса 
        /// чек бокса "Наличие острия".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void CheckBoxEndBladeCheckedChanged(object sender,
            EventArgs e)
        {
            _parameters.BladeExistence = CheckBoxPeakBlade.Checked;
            SetDefault();
        }

        /// <summary>
        /// Сброс окна ошибок и подсвечивания неверных значений, 
        /// выставление текущих значений параметров.
        /// </summary>
        private void SetDefault()
        {
            if (_parametersLabels != null && _parametersTextBoxes != null)
            {
                foreach (var parameter in _currentParameters)
                {
                    _parametersLabels[parameter.Key].ForeColor = Color.Black;
                    _parametersTextBoxes[parameter.Key].ForeColor =
                        Color.Black;
                    _parametersTextBoxes[parameter.Key].Text =
                        _currentParameters[parameter.Key].ToString();
                }
            }
            TextBoxError.Text = "";
        }

        /// <summary>
        /// Полная проверка полноты параметров.
        /// </summary>
        /// <returns>Возвращает TRUE, если все параметры заполнены, 
        /// FALSE если есть пустой параметр.</returns>
        private bool CheckAll()
        {
            var exceptions = new List<ParameterException>();
            try
            {
                if (ComboBoxTypeBinding.SelectedIndex == -1)
                {
                    throw new ParameterException(
                        ExceptionType.NullException,
                        ParameterType.BindingType);
                }

                if (ComboBoxTypeBlade.SelectedIndex == -1)
                {
                    throw new ParameterException(
                        ExceptionType.NullException,
                        ParameterType.BladeLength);
                }

                foreach (var parameter in _parameters.NumericalParameters)
                {
                    if (parameter.Value == null ||
                        parameter.Value.Value == 0)
                    {
                        throw new ParameterException(
                            ExceptionType.NullException,
                            parameter.Key);
                    }
                    try
                    {
                        TryParseTextBox(parameter.Key);
                    }
                    catch (ParameterException ex)
                    {
                        exceptions.Add(ex);
                    }
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
                return true;
            }
            catch (ParameterException ex)
            {
                _parametersLabels[ex.ParameterType].ForeColor = Color.Red;
                TextBoxError.Text += _parametersStrings[ex.ParameterType]
                    + _exceptionsStrings[ex.ExceptionType] + "\n";
                return false;
            }
            catch (AggregateException ex)
            {
                foreach (ParameterException exception in ex.InnerExceptions)
                {
                    _parametersLabels[exception.ParameterType].ForeColor
                        = Color.Red;
                    TextBoxError.Text +=
                        _parametersStrings[exception.ParameterType]
                        + _exceptionsStrings[exception.ExceptionType] + "\n";
                }
                return false;
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе
        /// с текст бокса "Толщина клинка".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxBladeThicknessLeave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.BladeThickness,
                 new ParameterType[0]);
        }

        /// <summary>
        /// Функция, выполняющая действия при нажатии кнопки "Построить".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void ButtonBuildClick(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                SetDefault();
                _builder.StartCreating();
                _builder.BuildBlade(_parameters);
                Console.Write("Starting Building");
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при 
        /// выходе с текст бокса "Длина серрейтора".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void SerreitorLengthTextBox_Leave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.SerreitorLength,
                new ParameterType[0]);
        }

        /// <summary>
        /// Функция, выполняющая действия при изменении 
        /// состояния чек-бокса серрейтора.
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void serreitorCheckBox_CheckedChanged(object sender,
            EventArgs e)
        {
            _parameters.SerreitorExistence = serreitorCheckBox.Checked;
            SerreitorLengthTextBox.Enabled = serreitorCheckBox.Checked;
            SerreitorTypeComboBox.Enabled = serreitorCheckBox.Checked;
            SerreitorNumberTextBox.Enabled = serreitorCheckBox.Checked;
            SetDefault();
        }

        /// <summary>
        /// Парсинг значения из TextBox с обработкой зависимых параметров.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="dependentParameterType">Массив 
        /// зависимых параметров.</param>
        private void ParseTextBox(
            ParameterType parameterType,
            ParameterType[] dependentParameterType)
        {
            var exceptions = new List<ParameterException>();
            try
            {
                TryParseTextBox(parameterType);
                for (int i = 0; i < dependentParameterType.Length; i++)
                {
                    try
                    {
                        SetDependenciesAndUpdate(parameterType,
                        dependentParameterType[i]);
                        TryParseTextBox(dependentParameterType[i]);
                    }
                    catch (ParameterException ex)
                    {
                        exceptions.Add(ex);
                    }
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
                SetDefault();
            }
            catch (ParameterException ex)
            {
                _parametersTextBoxes[ex.ParameterType].ForeColor = Color.Red;
                TextBoxError.Text += _parametersStrings[ex.ParameterType] +
                    _exceptionsStrings[ex.ExceptionType] + "\n";
            }
            catch (AggregateException ex)
            {
                foreach (ParameterException exception in ex.InnerExceptions)
                {
                    _parametersTextBoxes[exception.ParameterType].ForeColor
                        = Color.Red;
                    TextBoxError.Text +=
                        _parametersStrings[exception.ParameterType] +
                        _exceptionsStrings[exception.ExceptionType] + "\n";
                }
            }
        }

        /// <summary>
        /// Попытка парсинга значения из TextBox.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        private void TryParseTextBox(ParameterType parameterType)
        {
            Control textBox = _parametersTextBoxes[parameterType];
            try
            {
                if (textBox.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(textBox.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            parameterType].Value = value;
                        _currentParameters[parameterType] = value;
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException, parameterType);
                    }
                }
                else
                {
                    if (_currentParameters[parameterType] != 0)
                    {
                        textBox.Text =
                            _currentParameters[parameterType].ToString();
                    }
                }
            }
            catch (ParameterException ex)
            {
                throw new ParameterException(ex.ExceptionType, parameterType);
            }
        }

        /// <summary>
        /// Установка зависимостей между параметрами и обновление ToolTip.
        /// </summary>
        /// <param name="parameterType">Тип основного параметра.</param>
        /// <param name="dependentParameter">Тип 
        /// зависимого параметра.</param>
        private void SetDependenciesAndUpdate(ParameterType parameterType,
            ParameterType dependentParameter)
        {
            _parameters.SetDependencies(
                _parameters.NumericalParameters[parameterType],
                _parameters.NumericalParameters[dependentParameter],
                _parametersRatios[(parameterType, dependentParameter)].Item1,
                _parametersRatios[(parameterType, dependentParameter)].Item2
            );

            UpdateToolTip(
                _parametersTextBoxes[dependentParameter],
                _parameters.NumericalParameters[dependentParameter].MaxValue,
                _parameters.NumericalParameters[dependentParameter].MinValue
            );
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с комбо-бокса 
        /// "Тип серрейтора".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, 
        /// передаваемые с событием вызова.</param>
        private void SerreitorTypeComboBox_SelectedIndexChanged(object sender,
            EventArgs e)
        {
            if (SerreitorTypeComboBox.SelectedIndex != -1)
            {
                _parameters.SerreitorType =
                    (SerreitorType)SerreitorTypeComboBox.SelectedIndex;
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с текст бокса 
        /// "Количество зубьев".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void SerreitorNumberTextBox_Leave(object sender, EventArgs e)
        {
            ParseTextBox(ParameterType.SerreitorNumber,
                new ParameterType[0]);
        }

        /// <summary>
        /// Дейстивие при закрытии формы
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _builder.CLose();
        }
    }
}