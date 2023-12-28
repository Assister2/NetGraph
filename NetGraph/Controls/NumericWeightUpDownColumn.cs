namespace CyConex.Controls
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    public class NumericWeightUpDownColumn : DataGridViewColumn
	{

		public NumericWeightUpDownColumn() : base(new NumericWeightUpDownCell())
		{
		}

		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				// Ensure that the cell used for the template is a NumericUpDownCell
				if (value != null && !value.GetType().IsAssignableFrom(typeof(NumericWeightUpDownCell)))
				{
					throw new InvalidCastException("Must be a NumericUpDownCell");
				}
				base.CellTemplate = value;
			}
		}
	}

	public class NumericWeightUpDownCell : DataGridViewTextBoxCell
	{
		public NumericWeightUpDownCell() : base()
		{
			this.Style.Format = "N1";
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			// Set the value of the editing control to the current cell value.
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			NumericWeightUpDownEditingControl ctl = DataGridView.EditingControl as NumericWeightUpDownEditingControl;
			// Use the default row value when Value property is null.
			if (this.RowIndex != -1)
			{
				if (this.Value == null)
				{
					ctl.Value = Convert.ToDecimal(this.DefaultNewRowValue);
				}
				else
				{
					ctl.Value = Convert.ToDecimal(this.Value);
				}
			}
		}

		public override Type EditType
		{
			get
			{
				// Return the type of the editing control that NumericUpDownCell uses.
				return typeof(NumericWeightUpDownEditingControl);
			}
		}

		public override Type ValueType
		{
			get
			{
				// Return the type of the value that NumericUpDownCell contains.
				return typeof(Decimal);
			}
		}

		public override object DefaultNewRowValue
		{
			get
			{
				// Use the 0.5 as default value.
				return 0.5M;
			}
		}
	}

	class NumericWeightUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
	{
		
		public event EventHandler<WeightValueChangedEventArgs> WeightValueChanged;

		DataGridView dataGridView;
		private bool valueChanged = false;
		int rowIndex;
		private bool _thisConstructorCall;

		public NumericWeightUpDownEditingControl() : base()
		{
			this.DecimalPlaces = 1;
			this.TextAlign = HorizontalAlignment.Right;
			this.Increment = 0.1M;
			this.Maximum = 10.0M;
			_thisConstructorCall = true;
			this.Minimum = 0.0M;
			this.ValueChanged += NumericUpDownEditingControl_ValueChanged;
			this.BorderStyle = BorderStyle.FixedSingle;
			_thisConstructorCall = false;
		}

		private void NumericUpDownEditingControl_ValueChanged(object sender, EventArgs e)
		{
			WeightValueChanged?.Invoke(sender, new WeightValueChangedEventArgs(this.rowIndex, this.Value));
		}

		// Implements the IDataGridViewEditingControl.EditingControlFormattedValue
		// property.
		public object EditingControlFormattedValue
		{
			get
			{
				return this.Value.ToString();
			}
			set
			{
				if (value is String)
				{
					try
					{
						// This will throw an exception of the string is null, empty, or not in the format of a decimal.
						this.Value = Decimal.Parse((String)value);
					}
					catch
					{
						// In the case of an exception, just use the default value so we're not left with a null value.
						this.Value = 0.5M;
					}
				}
			}
		}

		// Implements the
		// IDataGridViewEditingControl.GetEditingControlFormattedValue method.
		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return EditingControlFormattedValue;
		}

		// Implements the
		// IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			this.ForeColor = dataGridViewCellStyle.ForeColor;
			this.BackColor = dataGridViewCellStyle.BackColor;
		}

		// Implements the IDataGridViewEditingControl.EditingControlRowIndex property.
		public int EditingControlRowIndex
		{
			get
			{
				return rowIndex;
			}
			set
			{
				rowIndex = value;
			}
		}

		// Implements the IDataGridViewEditingControl.EditingControlWantsInputKey
		// method.
		public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
		{
			// Let the numeric handle the keys listed.
			switch (key & Keys.KeyCode)
			{
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
				case Keys.Right:
					return true;
				default:
					return !dataGridViewWantsInputKey;
			}
		}

		// Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit method.
		public void PrepareEditingControlForEdit(bool selectAll)
		{
			// No preparation needs to be done.
		}

		// Implements the IDataGridViewEditingControl.RepositionEditingControlOnValueChange property.
		public bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		// Implements the IDataGridViewEditingControl.EditingControlDataGridView property.
		public DataGridView EditingControlDataGridView
		{
			get
			{
				return dataGridView;
			}
			set
			{
				dataGridView = value;
			}
		}

		// Implements the IDataGridViewEditingControl.EditingControlValueChanged property.
		public bool EditingControlValueChanged
		{
			get
			{
				return valueChanged;
			}
			set
			{
				valueChanged = value;
			}
		}

		// Implements the IDataGridViewEditingControl.EditingPanelCursor property.
		public Cursor EditingPanelCursor
		{
			get
			{
				return base.Cursor;
			}
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			if (!_thisConstructorCall)
			{
				// Notify the DataGridView that the contents of the cell have changed.
				this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
				base.OnValueChanged(eventargs);
				valueChanged = true;
			}
			else
			{
				Debug.WriteLine("This is constructor call");
			}
		}
	}

	public class WeightValueChangedEventArgs : EventArgs
	{
		public int Row { get; set; }
		public object Value { get; set; }

		public WeightValueChangedEventArgs(int row, object value)
		{
			Row = row;
			Value = value;	
		}
	}
}
