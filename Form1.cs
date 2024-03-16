using SchnaeppchenJaeger.Client;

namespace SchnaeppchenJaeger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button_test_Click(object sender, EventArgs e)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var _client = new ApiClient(74564, "coca cola");

            var result = await _client.GetOffersAsync(cancellationToken);
        }
    }
}
