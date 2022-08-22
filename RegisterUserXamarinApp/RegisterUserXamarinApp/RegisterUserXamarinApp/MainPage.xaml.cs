using RegisterUserXamarinApp.Models;
using RegisterUserXamarinApp.Services;
using System;
using Xamarin.Forms;

namespace RegisterUserXamarinApp
{
    public partial class MainPage : ContentPage
    {
        private UserService userService;
        public MainPage()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private async void PostUserButton(object sender, EventArgs e)
        {
            try
            {
                if (entFirstName.Text == null)
                {
                    await DisplayAlert("Erro de validação", "Nome inválido", "Confirmar");
                }
                else if (entAge.Text == null)
                {
                    await DisplayAlert("Erro de validação", "Idade inválida", "Confirmar");
                }
                else
                {
                    User user = new User
                    {
                        firstName = entFirstName.Text,
                        surname = entSurname.Text,
                        age = Convert.ToInt32(entAge.Text)
                    };

                    await userService.CreateUser(user);
                    LimparCampos();
                    await DisplayAlert("Sucesso", "Usuário criado com sucesso!", "Confirmar");

                }


            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private void LimparCampos()
        {
            entFirstName.Text = "";
            entSurname.Text = "";
            entAge.Text = "";
        }
    }
}
