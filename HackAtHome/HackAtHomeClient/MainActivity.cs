using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.SAL;
using HackAtHome.Entities;


namespace HackAtHomeClient
{
    [Activity(Label = "Hack@Home", MainLauncher = true, Icon = "@drawable/luisito")]
    public class MainActivity : Activity
    {
        ServiceClient serviceCliente = new ServiceClient();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            var Validar = FindViewById<Button>(Resource.Id.btnValidate);
            
            Validar.Click += Validar_Click;
        }

        private async void Validar_Click(object sender, System.EventArgs e)
        {
            var Email = FindViewById<EditText>(Resource.Id.txtEmail);
            var Pass = FindViewById<EditText>(Resource.Id.txtPass);

            var Result = await serviceCliente.AutenticateAsync(Email.Text,Pass.Text);            
                
            var intent = new Android.Content.Intent(this, typeof(ListaLabs));
            intent.PutExtra("dato", Result.FullName);
            intent.PutExtra("token", Result.Token);
            StartActivity(intent);

            //Evidencia del HackAtHome
            var MicrosoftEvidence = new LabItem
            {
                Email = Email.Text,
                Lab = "Hack@Home",
                DeviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId),
            };
            var MicrosoftCliente = new MicrosoftServiceClient();
            await MicrosoftCliente.SendEvidence(MicrosoftEvidence);
        }
    }
}

