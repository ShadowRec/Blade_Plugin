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
using CORE;
using KompasBuilder;

namespace BladePlugin
{
    public partial class MainForm : Form
    {
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

        /// <summary>
        /// Текущее  значение длины клинка
        /// </summary>
        private double _bladeLengthCurr;
        /// <summary>
        /// // Текущее значение длины крепления
        /// </summary>
        private double _bindingLengthCurr; 
        /// <summary>
        /// Текущее значение длины острия
        /// </summary>
        private double _peakLengthCurr; 
        /// <summary>
        /// Текущее значения ширины клинка
        /// </summary>
        private double _bladeWidthCurr; 
        /// <summary>
        /// Текущее значение толщины клинка
        /// </summary>
        private double _bladeThickCurr; 
        /// <summary>
        /// Текущее значение ширины лезвия
        /// </summary>
        private double _edgeWidthCurr;
        private void MainFormLoad(object sender, EventArgs e)
        {
            _parameters = new Parameters();

            //Установка длины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeLength].Value = 300;
            _bladeLengthCurr = 300;

            _parameters.SetDependencies(
                             _parameters.NumericalParameters[ParameterType.BladeLength],
                             _parameters.NumericalParameters[ParameterType.PeakLenght], 1.0 / 6.0, 0
                            );
            UpdateToolTip(
                TextBoxPeakLength,
                _parameters.NumericalParameters[ParameterType.PeakLenght].MaxValue,
                _parameters.NumericalParameters[ParameterType.PeakLenght].MinValue
                );

            _parameters.SetDependencies(
                            _parameters.NumericalParameters[ParameterType.BladeLength],
                            _parameters.NumericalParameters[ParameterType.BindingLength],
                            1, 0
                            );

            UpdateToolTip(
            TextBoxBindingLength,
            _parameters.NumericalParameters[ParameterType.BindingLength].MaxValue,
            _parameters.NumericalParameters[ParameterType.BindingLength].MinValue
            );

            //Установка ширины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeWidth].Value = 40;
            _bladeWidthCurr = 40;

            _parameters.SetDependencies(
                            _parameters.NumericalParameters[ParameterType.BladeWidth],
                            _parameters.NumericalParameters[ParameterType.EdgeWidth], 3.0 / 6.0, 1.0 / 6.0
                            );
            UpdateToolTip(
                TextBoxEdgeWidth,
                _parameters.NumericalParameters[ParameterType.EdgeWidth].MaxValue,
                _parameters.NumericalParameters[ParameterType.EdgeWidth].MinValue
                );

            //Установка толщины по умолчанию
            _parameters.NumericalParameters[ParameterType.BladeThickness].Value = 2;
            _bladeThickCurr = 2;

            //Установка длины острия по умолчанию
            _parameters.NumericalParameters[ParameterType.PeakLenght].Value = 40;
            _peakLengthCurr = 40;

            //Установка ширины лезвия по умолчанию
            _parameters.NumericalParameters[ParameterType.EdgeWidth].Value = 14;
            _edgeWidthCurr = 14;

            //Установка длины крепления по умолчанию
            _parameters.NumericalParameters[ParameterType.BindingLength].Value = 300;
            _bindingLengthCurr = 300;

            //Установка наличия острия по умолчанию (Есть)
            _parameters.BladeExistence = true;

            //Установка типа клинка по умолчанию (Односторонний)
            _parameters.BladeType = false;
            
            //Установка типа крепления по умолчанию (Накладное)
            _parameters.BindingType=BindingType.ForOverlays;

            ComboBoxTypeBlade.SelectedIndex = 0;
            ComboBoxTypeBinding.SelectedIndex = 2;
            CheckBoxPeakBlade.Checked = true;

            _builder = new Builder();
        }

        private void TextBoxLengthLeave(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxLength.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxLength.Text, out value)|| value!=0)
                    {
                        _parameters.NumericalParameters[ParameterType.BladeLength].Value = value;
                        _bladeLengthCurr= value;  
                        _parameters.SetDependencies(
                             _parameters.NumericalParameters[ParameterType.BladeLength],
                             _parameters.NumericalParameters[ParameterType.PeakLenght], 1.0 / 6.0, 0
                            );
                        UpdateToolTip(
                            TextBoxPeakLength,
                            _parameters.NumericalParameters[ParameterType.PeakLenght].MaxValue,
                            _parameters.NumericalParameters[ParameterType.PeakLenght].MinValue
                            );
                        CheckDepended(TextBoxPeakLength, ParameterType.PeakLenght, "Длина острия");

                        if ((BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.Insert)
                        {
                            _parameters.SetDependencies(
                            _parameters.NumericalParameters[ParameterType.BladeLength],
                            _parameters.NumericalParameters[ParameterType.BindingLength], 3.0 / 4.0, 0
                            );

                            UpdateToolTip(
                            TextBoxBindingLength,
                            _parameters.NumericalParameters[ParameterType.BindingLength].MaxValue,
                            _parameters.NumericalParameters[ParameterType.BindingLength].MinValue
                            );
                            CheckDepended(TextBoxBindingLength, ParameterType.BindingLength, "Длина Крепления");
                        }
                        if ((BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.Through ||
                            (BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.ForOverlays)
                        {
                            _parameters.SetDependencies(
                            _parameters.NumericalParameters[ParameterType.BladeLength],
                            _parameters.NumericalParameters[ParameterType.BindingLength],
                            1, 0
                            );

                            UpdateToolTip(
                            TextBoxBindingLength,
                            _parameters.NumericalParameters[ParameterType.BindingLength].MaxValue,
                            _parameters.NumericalParameters[ParameterType.BindingLength].MinValue
                            );
                            CheckDepended(TextBoxBindingLength, ParameterType.BindingLength, "Длина Крепления");
                        }

                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
                else
                {
                    if (_bladeLengthCurr != 0)
                    {
                        TextBoxLength.Text = _bladeLengthCurr.ToString();
                    }
                }
                SetDefault();
            }
            catch (Exception ex)
            {
                TextBoxLength.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Длина Клинка' было введено некорректное значение /n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += " В поле 'Длина Клинка' было введено  значение, что меньше диапазона допустимых значений!/n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += " В поле 'Длина Клинка' было введено  значение, что больше диапазона допустимых значений!/n";
                }

            }

        }
        /// <summary>
        /// Обновления текста ToolTip
        /// </summary>
        /// <param name="target">Ссылка на поле, чей ToolTip обновляется</param>
        /// <param name="maxvalue"> Максимальное значение допустимого диапазона</param>
        /// <param name="minvalue">Минимальное значение допустимого диапазона</param>
        private void UpdateToolTip(object target, double maxvalue, double minvalue)
        {
            Max_Min_Value.SetToolTip((Control)target, $"Допустимые значения:{minvalue}..{maxvalue}");
        }

        private void TextBoxPeakLengthLeave(object sender, EventArgs e)
        {
            
            try
            {
                if (TextBoxPeakLength.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxPeakLength.Text, out value) || value != 0)
                    {
                        _parameters.NumericalParameters[ParameterType.PeakLenght].Value = value;
                        _peakLengthCurr=value;
                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
                else
                {
                    if (_peakLengthCurr != 0)
                    {
                        TextBoxPeakLength.Text = _peakLengthCurr.ToString();
                    }
                }
                SetDefault();
            }
            catch (Exception ex)
            {
                TextBoxPeakLength.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Длина острия' было введено некорректное значение /n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += " В поле 'Длина острия' было введено  значение, что меньше диапазона допустимых значений!/n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += " В поле 'Длина острия' было введено  значение, что больше диапазона допустимых значений!/n";
                }
            }
        }

        private void TextBoxBindingLengthLeave(object sender, EventArgs e)
        {
           
            try
            {
                if (TextBoxBindingLength.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxBindingLength.Text, out value) || value != 0)
                    {
                        _parameters.NumericalParameters[ParameterType.BindingLength].Value = value;
                        _bindingLengthCurr = value;
                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
                else
                {
                    if (_bindingLengthCurr != 0)
                    {
                        TextBoxBindingLength.Text = _bindingLengthCurr.ToString();
                    }
                }
                SetDefault();
            }
            catch (Exception ex)
            {
                TextBoxBindingLength.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Длина крепления' было введено некорректное значение \n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += " В поле 'Длина крепления' было введено  значение, что меньше диапазона допустимых значений!\n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += " В поле 'Длина крепления' было введено  значение, что больше диапазона допустимых значений!\n";
                }
            }
        }

        private void TextBoxWidthLeave(object sender, EventArgs e)
        {
            
            try
            {
                if (TextBoxWidth.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxWidth.Text, out value) || value != 0)
                    {
                        _parameters.NumericalParameters[ParameterType.BladeWidth].Value = value;
                        _bladeWidthCurr=value;

                        _parameters.SetDependencies(
                            _parameters.NumericalParameters[ParameterType.BladeWidth],
                            _parameters.NumericalParameters[ParameterType.EdgeWidth], 3.0 / 6.0, 1.0 / 6.0
                            );
                        UpdateToolTip(
                            TextBoxEdgeWidth,
                            _parameters.NumericalParameters[ParameterType.EdgeWidth].MaxValue,
                            _parameters.NumericalParameters[ParameterType.EdgeWidth].MinValue
                            );

                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
                else
                {
                    if (_bladeWidthCurr != 0)
                    {
                        TextBoxWidth.Text = _bladeWidthCurr.ToString();
                    }
                }
                SetDefault();

            }
            catch (Exception ex)
            {
                TextBoxWidth.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Ширина клинка' было введено некорректное значение /n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += " В поле 'Ширина клинка' было введено  значение, что меньше диапазона допустимых значений!/n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += " В поле 'Ширина клинка' было введено  значение, что больше диапазона допустимых значений!/n";
                }

            }
        }

        private void TextBoxEdgeWidthLeave(object sender, EventArgs e)
        {
            
            try
            {
                if (TextBoxEdgeWidth.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxEdgeWidth.Text, out value) || value != 0)
                    {
                        _parameters.NumericalParameters[ParameterType.EdgeWidth].Value = value;
                        _edgeWidthCurr=value;
                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
                else
                {
                    if (_edgeWidthCurr!= 0)
                    {
                        TextBoxEdgeWidth.Text = _edgeWidthCurr.ToString();
                    }
                }
                SetDefault();
            }
            catch (Exception ex)
            {
                TextBoxEdgeWidth.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Ширина лезвия' было введено некорректное значение \n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += " В поле 'Ширина лезвия' было введено  значение, что меньше диапазона допустимых значений!\n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += " В поле 'Ширина лезвия' было введено  значение, что больше диапазона допустимых значений!\n";
                }
            }
        }

        private void ComboBoxTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ComboBoxTypeBinding.SelectedIndex != -1)
            {
                _parameters.BindingType = (BindingType)ComboBoxTypeBinding.SelectedIndex;
                if((BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.None)
                {
                    TextBoxBindingLength.ReadOnly = true;
                    Max_Min_Value.SetToolTip(TextBoxBindingLength,"У клинка выбрано отсутствие крепления");
                }

                if (_parameters.NumericalParameters[ParameterType.BladeLength].Value != 0)
                {
                    if ((BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.Insert)
                    {
                        _parameters.SetDependencies(
                        _parameters.NumericalParameters[ParameterType.BladeLength],
                        _parameters.NumericalParameters[ParameterType.BindingLength], 3.0 / 4.0, 0
                        );

                        UpdateToolTip(
                        TextBoxBindingLength,
                        _parameters.NumericalParameters[ParameterType.BindingLength].MaxValue,
                        _parameters.NumericalParameters[ParameterType.BindingLength].MinValue
                        );
                        CheckDepended(TextBoxBindingLength, ParameterType.BindingLength, "Длина Крепления");
                        TextBoxBindingLength.ReadOnly = false;
                    }
                    if ((BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.Through ||
                        (BindingType)ComboBoxTypeBinding.SelectedIndex == BindingType.ForOverlays)
                    {
                        _parameters.SetDependencies(
                        _parameters.NumericalParameters[ParameterType.BladeLength],
                        _parameters.NumericalParameters[ParameterType.BindingLength],
                        1, 0
                        );

                        UpdateToolTip(
                        TextBoxBindingLength,
                        _parameters.NumericalParameters[ParameterType.BindingLength].MaxValue,
                        _parameters.NumericalParameters[ParameterType.BindingLength].MinValue
                        );
                        CheckDepended(TextBoxBindingLength, ParameterType.BindingLength, "Длина Крепления");
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
        /// <param name="target_label">Имя параметра для отображения в окне ошибок</param>
        private void CheckDepended(object target, ParameterType parametertype, string target_label)
        {

            Control target_control = (Control)target;
            try
            {
                if (target_control.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(target_control.Text, out value))
                    {
                        _parameters.NumericalParameters[parametertype].Value = value;
                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                target_control.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += $"В поле '{target_label}' было введено некорректное значение /n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += $" В поле '{target_label}' было введено  значение, что меньше диапазона допустимых значений!/n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += $" В поле '{target_label}' было введено  значение, что больше диапазона допустимых значений!/n";
                }
            }
        }

        private void ComboBoxTypeBladeLeave(object sender, EventArgs e)
        {
           
            if (ComboBoxTypeBlade.SelectedIndex != -1)
            {
                _parameters.BladeType = (ComboBoxTypeBlade.SelectedIndex == 1);
            }
            SetDefault();
        }

        private void CheckBoxEndBladeCheckedChanged(object sender, EventArgs e)
        {
           
            _parameters.BladeExistence = CheckBoxPeakBlade.Checked;
            SetDefault();
        }

        /// <summary>
        /// Сброс окна ошибок и подсвечивания неверных значений, выставление текущих значений параметров
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

            BindingLabel.ForeColor= Color.Black;
            BladeTypeLabel.ForeColor=Color.Black;
            BladeLengthLabel.ForeColor = Color.Black;
            PeakLengthLabel.ForeColor = Color.Black;
            BindingLengthLabel.ForeColor = Color.Black;
            BladeWidthLabel.ForeColor = Color.Black;
            BladeThickLabel.ForeColor = Color.Black;
            EdgeWidthLabel.ForeColor = Color.Black;

            if (_bladeLengthCurr != 0)
            {
                TextBoxLength.Text = _bladeLengthCurr.ToString();
            }

            if (_peakLengthCurr != 0)
            {
                TextBoxPeakLength.Text = _peakLengthCurr.ToString();
            }

            if (_bindingLengthCurr != 0)
            {
                TextBoxBindingLength.Text = _bindingLengthCurr.ToString();
            }

            if (_bladeWidthCurr != 0)
            {
                TextBoxWidth.Text = _bladeWidthCurr.ToString();
            }

            if (_edgeWidthCurr != 0)
            {
                TextBoxEdgeWidth.Text = _edgeWidthCurr.ToString();
            }

            if (_bladeThickCurr != 0)
            {
                TextBoxBladeThickness.Text = _bladeThickCurr.ToString();
            }

        }

        /// <summary>
        /// Полная проверка полноты параметров
        /// </summary>
        /// <returns>Возвращает TRUE, если все параметры заполнены, FALSE если есть пустой параметр</returns>
        private bool CheckAll()
        {
            try
            {
                if (ComboBoxTypeBinding.SelectedIndex == -1)
                {
                    throw new Exception("binding_null");
                }
                if (ComboBoxTypeBlade.SelectedIndex == -1)
                {
                    throw new Exception("bladetype_null");
                }
                foreach(var parameter in _parameters.NumericalParameters)
                { 
                    if(parameter.Value==null|| parameter.Value.Value == 0)
                    {
                        throw new Exception(parameter.Key.ToString() + "_null");
                    }
                    
                }
                return true;
            }
            catch (Exception ex) 
            {
                if (ex.Message == "binding_null")
                {
                    BindingLabel.ForeColor= Color.Red;
                    TextBoxError.Text+= "Не выбран тип крепления!\n";
                }

                if(ex.Message== "bladetype_null")
                {
                    BladeTypeLabel.ForeColor = Color.Red;
                    TextBoxError.Text += "Не выбран тип клинка!\n";
                }

                if(ex.Message== "BladeLength_null")
                {
                    BladeLengthLabel.ForeColor = Color.Red;
                    TextBoxError.Text += "Не введено значение в поле 'Длина клинка'!\n";
                }

                if(ex.Message== "PeakLenght_null")
                {
                    PeakLengthLabel.ForeColor=Color.Red;
                    TextBoxError.Text += "Не введено значение в поле 'Длина острия'!\n";
                }

                if( ex.Message== "BindingLength_null")
                {
                    BindingLengthLabel.ForeColor = Color.Red;
                    TextBoxError.Text += "Не введено значение в поле 'Длина крепления'!\n";
                }
                
                if(ex.Message== "BladeWidth_null")
                {
                    BladeWidthLabel.ForeColor = Color.Red;
                    TextBoxError.Text += "Не введено значение в поле 'Ширина клинка'!\n";
                }

                if(ex.Message== "BladeThickness_null")
                {
                    BladeThickLabel.ForeColor = Color.Red;
                    TextBoxError.Text += "Не введено значение в поле 'Толщина клинка'!\n";
                }

                if(ex.Message== "EdgeWidth_null")
                {
                    EdgeWidthLabel.ForeColor= Color.Red;
                    TextBoxError.Text += "Не введено значение в поле 'Ширина Лезвия'!\n";
                }
                return false;
            }
        }

        private void TextBoxBladeThicknessLeave(object sender, EventArgs e)
        {
           
            try
            {
                if (TextBoxBladeThickness.Text != "")
                {
                    double value = 0;
                    if (double.TryParse(TextBoxBladeThickness.Text, out value) || value != 0)
                    {
                        _parameters.NumericalParameters[ParameterType.BladeThickness].Value = value;
                        _bladeThickCurr= value;
                    }
                    else
                    {
                        throw new Exception("value_is_invalid");
                    }
                }
                else
                {
                    if (_bladeThickCurr != 0)
                    {
                        TextBoxBladeThickness.Text = _bladeThickCurr.ToString();
                    }
                }
                SetDefault();
            }
            catch (Exception ex)
            {
                TextBoxWidth.ForeColor = Color.Red;
                if (ex.Message == "value_is_invalid")
                {
                    TextBoxError.Text += "В поле 'Толщина клинка' было введено некорректное значение /n";
                }

                if (ex.Message == "Value_small")
                {
                    TextBoxError.Text += " В поле 'Толщина клинка' было введено  значение, что меньше диапазона допустимых значений!/n";
                }

                if (ex.Message == "Value_TooBig")
                {
                    TextBoxError.Text += " В поле 'Толщина клинка' было введено  значение, что больше диапазона допустимых значений!/n";
                }

            }
        }

        private void ButtonBuildClick(object sender, EventArgs e)
        {
           if(CheckAll())
            {
                SetDefault();
                _builder.BuildBlade(_parameters);
                Console.Write("Starting Building");
            }  
        }
    }
}
