using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KompasAPI7;
using Kompas6API5;
using System.Runtime.InteropServices;
using Core;
using KompasBuilder;

namespace GUI
{
    //TODO: rsdn
    public partial class MainForm : Form
    {
        //TODO: rsdn
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Объект Builder
        /// </summary>
        private Builder _builder;

        /// <summary>
        /// Параметры клинка
        /// </summary>
        private Parameters _parameters;

        //TODO: refactor
        /// <summary>
        /// Текущее значение длины клинка
        /// </summary>
        private double _bladeLengthCurrent;
        /// <summary>
        /// Текущее значение длины крепления
        /// </summary>
        private double _bindingLengthCurrent;
        /// <summary>
        /// Текущее значение длины острия
        /// </summary>
        private double _peakLengthCurrent;
        /// <summary>
        /// Текущее значение ширины клинка
        /// </summary>
        private double _bladeWidthCurrent;
        /// <summary>
        /// Текущее значение толщины клинка
        /// </summary>
        private double _bladeThickCurrent;
        /// <summary>
        /// Текущее значение ширины лезвия
        /// </summary>
        private double _edgeWidthCurrent;

        /// <summary>
        /// Функция, выполняющая действия при запуске программы
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            _parameters = new Parameters();

            //Установка длины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeLength]
                .Value = 300;
            _bladeLengthCurrent = 300;

            _parameters.SetDependencies(
                _parameters.NumericalParameters[ParameterType.BladeLength],
                _parameters.NumericalParameters[ParameterType.PeakLenght],
                1.0 / 6.0, 0
            );

            UpdateToolTip(
                TextBoxPeakLength,
                _parameters.NumericalParameters[ParameterType.PeakLenght]
                    .MaxValue,
                _parameters.NumericalParameters[ParameterType.PeakLenght]
                    .MinValue
            );

            _parameters.SetDependencies(
                _parameters.NumericalParameters[ParameterType.BladeLength],
                _parameters.NumericalParameters[ParameterType.BindingLength],
                1, 0
            );

            UpdateToolTip(
                TextBoxBindingLength,
                _parameters.NumericalParameters[ParameterType.BindingLength]
                    .MaxValue,
                _parameters.NumericalParameters[ParameterType.BindingLength]
                    .MinValue
            );

            //Установка ширины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeWidth]
                .Value = 40;
            _bladeWidthCurrent = 40;

            _parameters.SetDependencies(
                _parameters.NumericalParameters[ParameterType.BladeWidth],
                _parameters.NumericalParameters[ParameterType.EdgeWidth],
                3.0 / 6.0, 1.0 / 6.0
            );

            UpdateToolTip(
                TextBoxEdgeWidth,
                _parameters.NumericalParameters[ParameterType.EdgeWidth]
                    .MaxValue,
                _parameters.NumericalParameters[ParameterType.EdgeWidth]
                    .MinValue
            );

            //Установка толщины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeThickness]
                .Value = 2;
            _bladeThickCurrent = 2;

            //Установка длины острия по умолчанию
            _parameters.NumericalParameters[ParameterType.PeakLenght]
                .Value = 40;
            _peakLengthCurrent = 40;

            //Установка ширины лезвия по умолчанию
            _parameters.NumericalParameters[ParameterType.EdgeWidth]
                .Value = 14;
            _edgeWidthCurrent = 14;

            //Установка длины крепления по умолчанию
            _parameters.NumericalParameters[ParameterType.BindingLength]
                .Value = 300;
            _bindingLengthCurrent = 300;

            //Установка наличия острия по умолчанию (Есть)
            _parameters.BladeExistence = true;

            //Установка типа клинка по умолчанию (Односторонний)
            _parameters.BladeType = false;

            //Установка типа крепления по умолчанию (Накладное)
            _parameters.BindingType = BindingType.ForOverlays;

            ComboBoxTypeBlade.SelectedIndex = 0;
            ComboBoxTypeBinding.SelectedIndex = 2;
            CheckBoxPeakBlade.Checked = true;

            _builder = new Builder();
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе 
        /// с поля "Длина клинка"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void TextBoxLengthLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxLength.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxLength.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BladeLength].Value = value;
                        _bladeLengthCurrent = value;

                        _parameters.SetDependencies(
                            _parameters.NumericalParameters[
                                ParameterType.BladeLength],
                            _parameters.NumericalParameters[
                                ParameterType.PeakLenght],
                            1.0 / 6.0, 0
                        );

                        UpdateToolTip(
                            TextBoxPeakLength,
                            _parameters.NumericalParameters[
                                ParameterType.PeakLenght].MaxValue,
                            _parameters.NumericalParameters[
                                ParameterType.PeakLenght].MinValue
                        );

                        CheckDepended(TextBoxPeakLength,
                            ParameterType.PeakLenght, "Длина острия");

                        if ((BindingType)ComboBoxTypeBinding.SelectedIndex
                            == BindingType.Insert)
                        {
                            _parameters.SetDependencies(
                                _parameters.NumericalParameters[
                                    ParameterType.BladeLength],
                                _parameters.NumericalParameters[
                                    ParameterType.BindingLength],
                                3.0 / 4.0, 0
                            );

                            UpdateToolTip(
                                TextBoxBindingLength,
                                _parameters.NumericalParameters[
                                    ParameterType.BindingLength].MaxValue,
                                _parameters.NumericalParameters[
                                    ParameterType.BindingLength].MinValue
                            );

                            CheckDepended(TextBoxBindingLength,
                                ParameterType.BindingLength,
                                "Длина Крепления");
                        }

                        if ((BindingType)ComboBoxTypeBinding.SelectedIndex
                            == BindingType.Through ||
                            (BindingType)ComboBoxTypeBinding.SelectedIndex
                            == BindingType.ForOverlays)
                        {
                            _parameters.SetDependencies(
                                _parameters.NumericalParameters[
                                    ParameterType.BladeLength],
                                _parameters.NumericalParameters[
                                    ParameterType.BindingLength],
                                1, 0
                            );

                            UpdateToolTip(
                                TextBoxBindingLength,
                                _parameters.NumericalParameters[
                                    ParameterType.BindingLength].MaxValue,
                                _parameters.NumericalParameters[
                                    ParameterType.BindingLength].MinValue
                            );

                            CheckDepended(TextBoxBindingLength,
                                ParameterType.BindingLength,
                                "Длина Крепления");
                        }
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
                else
                {
                    if (_bladeLengthCurrent != 0)
                    {
                        TextBoxLength.Text = _bladeLengthCurrent.ToString();
                    }
                }
                SetDefault();
            }
            //TODO: refactor DONE
            catch (ParameterException ex)
            {
                TextBoxLength.ForeColor = Color.Red;
                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += "В поле 'Длина Клинка' было " +
                        "введено некорректное значение /n";
                }

                if (ex.ExceptionType == ExceptionType.TooSmallException)
                {
                    TextBoxError.Text += "В поле 'Длина Клинка' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!/n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += "В поле 'Длина Клинка' было " +
                        "введено значение, что больше диапазона " +
                        "допустимых значений!/n";
                }
            }
        }

        /// <summary>
        /// Обновления текста ToolTip
        /// </summary>
        /// <param name="target">Ссылка на поле, чей ToolTip обновляется</param>
        /// <param name="maxvalue">
        /// Максимальное значение допустимого диапазона</param>
        /// <param name="minvalue">
        /// Минимальное значение допустимого диапазона</param>
        private void UpdateToolTip(object target, double maxvalue,
            double minvalue)
        {
            Max_Min_Value.SetToolTip((Control)target,
                $"Допустимые значения:{minvalue}..{maxvalue}");
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе 
        /// с поля "Длина острия"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void TextBoxPeakLengthLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxPeakLength.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxPeakLength.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            ParameterType.PeakLenght].Value = value;
                        _peakLengthCurrent = value;
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
                else
                {
                    if (_peakLengthCurrent != 0)
                    {
                        TextBoxPeakLength.Text = _peakLengthCurrent.ToString();
                    }
                }
                SetDefault();
            }
            catch (ParameterException ex)
            {
                TextBoxPeakLength.ForeColor = Color.Red;
                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += "В поле 'Длина острия' было " +
                        "введено некорректное значение /n";
                }

                if (ex.ExceptionType == ExceptionType.TooSmallException)
                {
                    TextBoxError.Text += "В поле 'Длина острия' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!/n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += "В поле 'Длина острия' было " +
                        "введено значение, что больше диапазона " +
                        "допустимых значений!/n";
                }
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе 
        /// с поля "Длина крепления"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void TextBoxBindingLengthLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxBindingLength.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxBindingLength.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BindingLength].Value = value;
                        _bindingLengthCurrent = value;
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
                else
                {
                    if (_bindingLengthCurrent != 0)
                    {
                        TextBoxBindingLength.Text =
                            _bindingLengthCurrent.ToString();
                    }
                }
                SetDefault();
            }
            catch (ParameterException ex)
            {
                TextBoxBindingLength.ForeColor = Color.Red;
                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += "В поле 'Длина крепления' было " +
                        "введено некорректное значение \n";
                }

                if (ex.ExceptionType == ExceptionType.TooSmallException)
                {
                    TextBoxError.Text += "В поле 'Длина крепления' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!\n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += "В поле 'Длина крепления' было " +
                        "введено значение, что больше диапазона " +
                        "допустимых значений!\n";
                }
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе 
        /// с поля "Ширина клинка"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void TextBoxWidthLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxWidth.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxWidth.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BladeWidth].Value = value;
                        _bladeWidthCurrent = value;

                        _parameters.SetDependencies(
                            _parameters.NumericalParameters[
                                ParameterType.BladeWidth],
                            _parameters.NumericalParameters[
                                ParameterType.EdgeWidth],
                            3.0 / 6.0, 1.0 / 6.0
                        );

                        UpdateToolTip(
                            TextBoxEdgeWidth,
                            _parameters.NumericalParameters[
                                ParameterType.EdgeWidth].MaxValue,
                            _parameters.NumericalParameters[
                                ParameterType.EdgeWidth].MinValue
                        );
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
                else
                {
                    if (_bladeWidthCurrent != 0)
                    {
                        TextBoxWidth.Text = _bladeWidthCurrent.ToString();
                    }
                }
                SetDefault();
            }
            catch (ParameterException ex)
            {
                TextBoxWidth.ForeColor = Color.Red;
                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += "В поле 'Ширина клинка' было " +
                        "введено некорректное значение /n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += "В поле 'Ширина клинка' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!/n";
                }

                if (ex.ExceptionType == ExceptionType.TooSmallException)
                {
                    TextBoxError.Text += "В поле 'Ширина клинка' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!/n";
                }
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе 
        /// с поля "Ширина лезвия"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void TextBoxEdgeWidthLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxEdgeWidth.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxEdgeWidth.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            ParameterType.EdgeWidth].Value = value;
                        _edgeWidthCurrent = value;
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
                else
                {
                    if (_edgeWidthCurrent != 0)
                    {
                        TextBoxEdgeWidth.Text = _edgeWidthCurrent.ToString();
                    }
                }
                SetDefault();
            }
            catch (ParameterException ex)
            {
                TextBoxEdgeWidth.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Ширина лезвия' было " +
                        "введено некорректное значение \n";
                }

                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += "В поле 'Ширина лезвия' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!\n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += "В поле 'Ширина лезвия' было " +
                        "введено значение, что больше диапазона " +
                        "допустимых значений!\n";
                }
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при изменении индекса
        /// в комбо-боксе "Тип Крепления"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void ComboBoxTypeSelectedIndexChanged(object sender,
            EventArgs e)
        {
            if (ComboBoxTypeBinding.SelectedIndex != -1)
            {
                _parameters.BindingType =
                    (BindingType)ComboBoxTypeBinding.SelectedIndex;

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
                    if ((BindingType)ComboBoxTypeBinding.SelectedIndex
                        == BindingType.Insert)
                    {
                        _parameters.SetDependencies(
                            _parameters.NumericalParameters[
                                ParameterType.BladeLength],
                            _parameters.NumericalParameters[
                                ParameterType.BindingLength],
                            3.0 / 4.0, 0
                        );

                        UpdateToolTip(
                            TextBoxBindingLength,
                            _parameters.NumericalParameters[
                                ParameterType.BindingLength].MaxValue,
                            _parameters.NumericalParameters[
                                ParameterType.BindingLength].MinValue
                        );

                        CheckDepended(TextBoxBindingLength,
                            ParameterType.BindingLength, "Длина Крепления");
                        TextBoxBindingLength.ReadOnly = false;
                    }

                    if ((BindingType)ComboBoxTypeBinding.SelectedIndex
                        == BindingType.Through ||
                        (BindingType)ComboBoxTypeBinding.SelectedIndex
                        == BindingType.ForOverlays)
                    {
                        _parameters.SetDependencies(
                            _parameters.NumericalParameters[
                                ParameterType.BladeLength],
                            _parameters.NumericalParameters[
                                ParameterType.BindingLength],
                            1, 0
                        );

                        UpdateToolTip(
                            TextBoxBindingLength,
                            _parameters.NumericalParameters[
                                ParameterType.BindingLength].MaxValue,
                            _parameters.NumericalParameters[
                                ParameterType.BindingLength].MinValue
                        );

                        CheckDepended(TextBoxBindingLength,
                            ParameterType.BindingLength, "Длина Крепления");
                        TextBoxBindingLength.ReadOnly = false;
                    }
                }
                SetDefault();
            }
        }

        /// <summary>
        /// Проверка зависимых параметров
        /// </summary>
        /// <param name="target">Ссылка на текстовое поле для парсинга</param>
        /// <param name="parametertype">Тип зависимого параметра</param>
        /// <param name="target_label">Имя параметра для отображения 
        /// в окне ошибок</param>
        private void CheckDepended(object target, ParameterType parametertype,
            string target_label)
        {
            Control target_control = (Control)target;
            try
            {
                if (target_control.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(target_control.Text, out value))
                    {
                        _parameters.NumericalParameters[parametertype].Value
                            = value;
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
            }
            catch (ParameterException ex)
            {
                target_control.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += $"В поле '{target_label}' было " +
                        "введено некорректное значение /n";
                }

                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += $"В поле '{target_label}' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!/n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += $"В поле '{target_label}' было " +
                        "введено значение, что больше диапазона " +
                        "допустимых значений!/n";
                }
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе
        /// с комбо-бокса "Тип клинка"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
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
        /// чек бокса "Наличие острия"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void CheckBoxEndBladeCheckedChanged(object sender, EventArgs e)
        {
            _parameters.BladeExistence = CheckBoxPeakBlade.Checked;
            SetDefault();
        }

        /// <summary>
        /// Сброс окна ошибок и подсвечивания неверных значений, 
        /// выставление текущих значений параметров
        /// </summary>
        private void SetDefault()
        {
            TextBoxLength.ForeColor = Color.Black;
            TextBoxPeakLength.ForeColor = Color.Black;
            TextBoxBindingLength.ForeColor = Color.Black;
            TextBoxWidth.ForeColor = Color.Black;
            TextBoxEdgeWidth.ForeColor = Color.Black;
            TextBoxBladeThickness.ForeColor = Color.Black;
            TextBoxError.Text = "";

            BindingLabel.ForeColor = Color.Black;
            BladeTypeLabel.ForeColor = Color.Black;
            BladeLengthLabel.ForeColor = Color.Black;
            PeakLengthLabel.ForeColor = Color.Black;
            BindingLengthLabel.ForeColor = Color.Black;
            BladeWidthLabel.ForeColor = Color.Black;
            BladeThickLabel.ForeColor = Color.Black;
            EdgeWidthLabel.ForeColor = Color.Black;

            if (_bladeLengthCurrent != 0)
            {
                TextBoxLength.Text = _bladeLengthCurrent.ToString();
            }

            if (_peakLengthCurrent != 0)
            {
                TextBoxPeakLength.Text = _peakLengthCurrent.ToString();
            }

            if (_bindingLengthCurrent != 0)
            {
                TextBoxBindingLength.Text = _bindingLengthCurrent.ToString();
            }

            if (_bladeWidthCurrent != 0)
            {
                TextBoxWidth.Text = _bladeWidthCurrent.ToString();
            }

            if (_edgeWidthCurrent != 0)
            {
                TextBoxEdgeWidth.Text = _edgeWidthCurrent.ToString();
            }

            if (_bladeThickCurrent != 0)
            {
                TextBoxBladeThickness.Text = _bladeThickCurrent.ToString();
            }
        }

        /// <summary>
        /// Полная проверка полноты параметров
        /// </summary>
        /// <returns>Возвращает TRUE, если все параметры заполнены, 
        /// FALSE если есть пустой параметр</returns>
        private bool CheckAll()
        {
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
                }
                return true;
            }
            catch (ParameterException ex)
            {
                if (ex.ExceptionType == ExceptionType.NullException)
                {
                    switch (ex.ParameterType)
                    {
                        case ParameterType.BindingType:
                            BindingLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не выбран тип крепления!\n";
                            break;
                        case ParameterType.BladeType:
                            BladeTypeLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не выбран тип клинка!\n";
                            break;
                        case ParameterType.BladeLength:
                            BladeLengthLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не введено значение в поле " +
                                "'Длина клинка'!\n";
                            break;
                        case ParameterType.PeakLenght:
                            PeakLengthLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не введено значение в поле " +
                                "'Длина острия'!\n";
                            break;
                        case ParameterType.BindingLength:
                            BindingLengthLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не введено значение в поле " +
                                "'Длина крепления'!\n";
                            break;
                        case ParameterType.BladeWidth:
                            BladeWidthLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не введено значение в поле " +
                                "'Ширина клинка'!\n";
                            break;
                        case ParameterType.BladeThickness:
                            BladeThickLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не введено значение в поле " +
                                "'Толщина клинка'!\n";
                            break;
                        case ParameterType.EdgeWidth:
                            EdgeWidthLabel.ForeColor = Color.Red;
                            TextBoxError.Text += "Не введено значение в поле " +
                                "'Ширина Лезвия'!\n";
                            break;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при выходе
        /// с текст бокса "Толщина клинка"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void TextBoxBladeThicknessLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxBladeThickness.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxBladeThickness.Text, out value)
                        || value != 0)
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BladeThickness].Value = value;
                        _bladeThickCurrent = value;
                    }
                    else
                    {
                        throw new ParameterException(
                            ExceptionType.InvalidException);
                    }
                }
                else
                {
                    if (_bladeThickCurrent != 0)
                    {
                        TextBoxBladeThickness.Text =
                            _bladeThickCurrent.ToString();
                    }
                }
                SetDefault();
            }
            catch (ParameterException ex)
            {
                //TODO: refactor
                TextBoxBladeThickness.ForeColor = Color.Red;
                if (ex.ExceptionType == ExceptionType.InvalidException)
                {
                    TextBoxError.Text += "В поле 'Толщина клинка' было " +
                        "введено некорректное значение /n";
                }

                if (ex.ExceptionType == ExceptionType.TooSmallException)
                {
                    TextBoxError.Text += "В поле 'Толщина клинка' было " +
                        "введено значение, что меньше диапазона " +
                        "допустимых значений!/n";
                }

                if (ex.ExceptionType == ExceptionType.TooBigException)
                {
                    TextBoxError.Text += "В поле 'Толщина клинка' было " +
                        "введено значение, что больше диапазона " +
                        "допустимых значений!/n";
                }
            }
        }

        /// <summary>
        /// Функция, выполняющая действия при нажатии
        /// кнопки "Построить"
        /// </summary>
        /// <param name="sender">Объект, вызвавший данную функцию</param>
        /// <param name="e">Аргументы, передаваемые с событием вызова</param>
        private void ButtonBuildClick(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                SetDefault();
                _builder.BuildBlade(_parameters);
                Console.Write("Starting Building");
            }
        }
    }
}