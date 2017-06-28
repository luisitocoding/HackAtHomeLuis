using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.Entities;
using HackAtHome.CustomAdapters;
using HackAtHome.SAL;


namespace HackAtHomeClient
{
    [Activity(Label = "Hack@Home",Icon ="@drawable/luisito")]
    public class ListaLabs : Activity
    {
        Evidence evidence = new Evidence();
        public List<Evidence> Items;
        PersistenciaLab data;
        EvidenceDetail eDetalle = new EvidenceDetail();
        EvidencesAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListaLabs);
            var user = FindViewById<TextView>(Resource.Id.tvUSER);
            user.Text = Intent.GetStringExtra("dato");
            var token = Intent.GetStringExtra("token");
            CargaLista(token);
            var ListViewDatos = FindViewById<ListView>(Resource.Id.listView1);
            ListViewDatos.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var intent = new Android.Content.Intent(this, typeof(LabInfo));
                intent.PutExtra("user", user.Text);                

                intent.PutExtra("id", data.ListaCmp[e.Position].EvidenceID);
                intent.PutExtra("token", token);

                intent.PutExtra("labNom", Items[e.Position].Title);
                intent.PutExtra("labStatus", Items[e.Position].Status);
                //
                //Koush.UrlImageViewHelper.SetUrlDrawable(Widget, ImgUrl);

                StartActivity(intent);
            };
        }

        
        bool IsLandScape(Activity activity)
        {
            var Orientacion = activity.WindowManager.DefaultDisplay.Rotation;
            return Orientacion == SurfaceOrientation.Rotation90 || Orientacion == SurfaceOrientation.Rotation270;
        }

        protected async void CargaLista(string Token)
        {
            data = (PersistenciaLab)this.FragmentManager.FindFragmentByTag("Data");
            if (data == null)
            {
                data = new PersistenciaLab();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(data, "Data");
                FragmentTransaction.Commit();
                ServiceClient Evidencias = new ServiceClient();
                data.ListaCmp = await Evidencias.GetEvidencesAsync(Token);
            }

            Items = data.ListaCmp;

            var ListViewDatos = FindViewById<ListView>(Resource.Id.listView1);
            if (!IsLandScape(this))
            {
                ListViewDatos.Adapter = new EvidencesAdapter(
                    this, Items, Resource.Layout.ListItemLabs, Resource.Id.tvLab, Resource.Id.tvAprobado);
            }
            else
            {
                ListViewDatos.Adapter = new EvidencesAdapter(
                    this, Items, Resource.Layout.ListItemLabs, Resource.Id.tvLab, Resource.Id.tvAprobado);
            }            
        }
    }
}