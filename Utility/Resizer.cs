#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate.


namespace SchnaeppchenJaeger.Utility
{
    /// <summary>
    /// Helper class to resize controls within a Form dynamically while maintaining their proportions.
    /// </summary>
    /// <remarks>
    /// Resizing would not work when Controls are placed in GroupBox. To fix this we need TableLayoutPanel and follow these steps:
    /// - Add a TableLayoutPanel with 4 columns and place it inside your GroupBox.
    /// - Place the Labels inside columns 1/3 and Textboxes inside columns 2/4
    /// - Set SizeType of columns 1/3 to Autosize and 2/4 to 50%
    /// - Set the Anchor-property of the TableLayoutPanel to Top, Left, Right
    /// - Set the Textboxes and Labels Dock-property to Fill
    /// - Set the Labels TextAlign-property to MiddleLeft
    /// 
    /// Credits to Stack Overflow users for original concepts:
    /// - https://stackoverflow.com/questions/50167571/make-forms-fit-to-every-size-of-screen
    /// - https://stackoverflow.com/questions/46316025/how-to-resize-controls-inside-groupbox-without-overlapping
    /// - Thanks to @Maximilian Hankele for contributions.
    /// </remarks>
    public class Resizer
    {
        private readonly List<ControlBounds> originalControlBounds = new List<ControlBounds>();
        private readonly Size originalClientSize;
        private float xRatio;
        private float yRatio;

        public Resizer() { }

        /// <summary>
        /// Initializes a new instance of the Resizer class for a given Form.
        /// </summary>
        /// <param name="form">The Form to resize controls for.</param>
        /// <exception cref="ArgumentNullException">Thrown if form is null.</exception>
        public Resizer(Form form) : this()
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            originalClientSize = form.ClientSize;
            SaveOriginalBounds(form);

            form.Load += Form_Load;
            form.Resize += Form_Resize;
        }

        /// <summary>
        /// Event handler for the Form.Load event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Form_Load(object sender, EventArgs e)
        {
            // No action needed here since initialization is handled in the constructor.
        }

        /// <summary>
        /// Event handler for the Form.Resize event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Form_Resize(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form != null)
            {
                xRatio = (float)form.ClientSize.Width / originalClientSize.Width;
                yRatio = (float)form.ClientSize.Height / originalClientSize.Height;
                ResizeControls(form);
            }
        }

        /// <summary>
        /// Saves the original bounds and font sizes of all controls within the parent control recursively.
        /// </summary>
        /// <param name="parent">The parent control to save original bounds for.</param>
        private void SaveOriginalBounds(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                originalControlBounds.Add(new ControlBounds(ctrl, ctrl.Bounds, ctrl.Font.Size));
                SaveOriginalBounds(ctrl); // Recursive call for child controls
            }
        }

        /// <summary>
        /// Resizes all controls within the parent control based on the current form size relative to its original size.
        /// </summary>
        /// <param name="parent">The parent control to resize controls for.</param>
        private void ResizeControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                // Find the original bounds entry for the control
                var original = originalControlBounds.Find(cb => cb.Control == ctrl);
                if (original.Control != null)
                {
                    int newX = (int)(original.OriginalBounds.X * xRatio);
                    int newY = (int)(original.OriginalBounds.Y * yRatio);
                    int newWidth = (int)(original.OriginalBounds.Width * xRatio);
                    int newHeight = (int)(original.OriginalBounds.Height * yRatio);

                    ctrl.Bounds = new Rectangle(newX, newY, newWidth, newHeight);
                    ctrl.Font = new Font(ctrl.Font.FontFamily, original.OriginalFontSize * yRatio, ctrl.Font.Style);

                    // Special handling for certain control types
                    switch (ctrl)
                    {
                        case ComboBox comboBox:
                            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                            comboBox.DrawItem -= ComboBox_DrawItem;
                            comboBox.DrawItem += ComboBox_DrawItem;
                            comboBox.ItemHeight = (int)(original.OriginalFontSize * yRatio * 1.5);
                            break;

                        case CheckedListBox checkedListBox:
                            checkedListBox.DrawMode = DrawMode.OwnerDrawVariable;
                            checkedListBox.MeasureItem -= CheckedListBox_MeasureItem;
                            checkedListBox.MeasureItem += CheckedListBox_MeasureItem;
                            checkedListBox.DrawItem -= CheckedListBox_DrawItem;
                            checkedListBox.DrawItem += CheckedListBox_DrawItem;
                            break;

                        case RichTextBox richTextBox:
                            ResizeRichTextBox(richTextBox, yRatio);
                            break;

                        default:
                            ResizeControls(ctrl);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for measuring items in the CheckedListBox to adjust their height.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void CheckedListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (sender is CheckedListBox checkedListBox)
            {
                float newItemHeight = checkedListBox.Font.Size * yRatio * 1.5f;
                e.ItemHeight = (int)newItemHeight; // Adjust item height as needed
            }
        }

        /// <summary>
        /// Event handler for drawing items in the CheckedListBox to adjust their font size.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void CheckedListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (sender is CheckedListBox checkedListBox)
            {
                if (e.Index >= 0)
                {
                    string text = checkedListBox.Items[e.Index].ToString();
                    float fontSize = originalControlBounds.Find(cb => cb.Control == checkedListBox).OriginalFontSize * yRatio;
                    Font itemFont = new Font(checkedListBox.Font.FontFamily, fontSize, checkedListBox.Font.Style);

                    e.DrawBackground();
                    e.Graphics.DrawString(text, itemFont, Brushes.Black, e.Bounds);
                    e.DrawFocusRectangle();
                }
            }
        }

        /// <summary>
        /// Resizes the text in a RichTextBox based on the given ratio.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox control to resize text for.</param>
        /// <param name="yRatio">The ratio to apply for resizing the text.</param>
        private void ResizeRichTextBox(RichTextBox richTextBox, float yRatio)
        {
            richTextBox.SuspendLayout();
            int start = richTextBox.SelectionStart;
            int length = richTextBox.SelectionLength;
            richTextBox.SelectAll();
            richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.Font.Size * yRatio, richTextBox.Font.Style);
            richTextBox.Select(start, length);
            richTextBox.ResumeLayout();
        }

        /// <summary>
        /// Event handler for drawing items in the ComboBox to adjust their font size.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;

                    string text = comboBox.Items[e.Index].ToString();
                    float fontSize = originalControlBounds.Find(cb => cb.Control == comboBox).OriginalFontSize * yRatio;
                    Font itemFont = new Font(comboBox.Font.FontFamily, fontSize, comboBox.Font.Style);

                    e.DrawBackground();
                    e.Graphics.DrawString(text, itemFont, Brushes.Black, e.Bounds, sf);
                    e.DrawFocusRectangle();
                }
            }
        }

        /// <summary>
        /// Structure to store original bounds and font size of a control.
        /// </summary>
        private struct ControlBounds
        {
            public Control Control;
            public Rectangle OriginalBounds;
            public float OriginalFontSize;

            public ControlBounds(Control control, Rectangle bounds, float fontSize)
            {
                Control = control;
                OriginalBounds = bounds;
                OriginalFontSize = fontSize;
            }
        }
    }
}
