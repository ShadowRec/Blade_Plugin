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

    
        //TODO: XML DONE
        /// <summary>
        /// Словарь содержащий списки зависимых параметров
        /// для независимых параметров
        /// </summary>
        private Dictionary<ParameterType, List<ParameterType>>
            _parameterTypeDependencies =
            new Dictionary<ParameterType, List<ParameterType>>()
            {
                [ParameterType.BladeLength] = 
                    new List<ParameterType>
                        {
                            ParameterType.BindingLength,
                            ParameterType.PeakLenght,
                            ParameterType.SerreitorLength
                        },
                [ParameterType.BladeWidth] = 
                    new List<ParameterType>
                        {
                            ParameterType.EdgeWidth
                        },
                [ParameterType.BladeThickness] = null,
                [ParameterType.EdgeWidth] = null,
                [ParameterType.PeakLenght] = null,
                [ParameterType.BindingLength] = null,
                [ParameterType.SerreitorLength] = null,
                [ParameterType.SerreitorNumber] = null
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

            UpdateToolTip(ParameterType.PeakLenght);
            UpdateToolTip(ParameterType.BindingLength);
            UpdateToolTip(ParameterType.EdgeWidth);
            UpdateToolTip(ParameterType.SerreitorLength);

            ComboBoxTypeBlade.SelectedIndex = 0;
            ComboBoxTypeBinding.SelectedIndex = 2;
            CheckBoxPeakBlade.Checked = true;

            SerreitorTypeComboBox.SelectedIndex = 0;
            serreitorCheckBox.Checked = true;
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе с поля "Длина клинка".
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию.</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова.</param>
        private void TextBoxLeave(object sender, EventArgs e)
        {
            var parameterType = _parametersTextBoxes.FirstOrDefault(
                x => x.Value == (Control)sender).Key;
            var dependencies = _parameterTypeDependencies[parameterType];
            if (dependencies != null) 
            {
                ParseTextBox(parameterType,
               dependencies.ToArray());
            }
            else
            {
                ParseTextBox(parameterType, null);
            }

            
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
        private void UpdateToolTip(ParameterType paramType)
        {
            var minvalue = 
                _parameters.NumericalParameters[paramType].MinValue;
            var maxvalue =
                _parameters.NumericalParameters[paramType].MaxValue;
            Max_Min_Value.SetToolTip(_parametersTextBoxes[paramType],
                $"Допустимые значения:{minvalue}..{maxvalue}");
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

                _parameters.SetBindingRatios((BindingType)
                    ComboBoxTypeBinding.SelectedIndex);

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
                foreach (var parameter in _parameters.NumericalParameters)
                {
                    _parametersLabels[parameter.Key].ForeColor = Color.Black;
                    _parametersTextBoxes[parameter.Key].ForeColor =
                        Color.Black;
                    _parametersTextBoxes[parameter.Key].Text =
                        parameter.Value.Value.ToString();
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
                if (dependentParameterType != null)
                {
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
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException, parameterType);
                    }
                }
                else
                {
                    if (_parameters.NumericalParameters[
                        parameterType].Value != 0)
                    {
                        textBox.Text =
                            _parameters.NumericalParameters[
                        parameterType].Value.ToString();
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
                parameterType, dependentParameter
            );

            UpdateToolTip(
               dependentParameter
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

    }
}