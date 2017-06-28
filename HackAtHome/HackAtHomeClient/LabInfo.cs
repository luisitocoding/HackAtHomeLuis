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
using HackAtHome.SAL;
using HackAtHome.CustomAdapters;
using HackAtHome.Entities;
using Android.Webkit;
namespace HackAtHomeClient
{
    [Activity(Label = "Hack@Home", Icon = "@drawable/luisito")]
    public class LabInfo : Activity
    {
        ServiceClient client = new ServiceClient();
        EvidencesAdapter adapter;
        EvidenceDetail detalle = new EvidenceDetail();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LabInfo);
            var user = FindViewById<TextView>(Resource.Id.txtUserNomLab);
            var labNombre = FindViewById<TextView>(Resource.Id.txtNomLab);
            var labStatus = FindViewById<TextView>(Resource.Id.txtLabStatus);           

            user.Text = Intent.GetStringExtra("user");

            var id = Intent.GetIntExtra("id",0);
            var token = Intent.GetStringExtra("token");

            labNombre.Text = Intent.GetStringExtra("labNom");
            labStatus.Text = Intent.GetStringExtra("labStatus");

            EvidenceDetail(token, id);           
        }

        public async void EvidenceDetail(string token, int IDEvidence)
        {
            

            var web_view = FindViewById<WebView>(Resource.Id.webView1);
            var img = FindViewById<ImageView>(Resource.Id.imageView1);
            detalle = await client.GetEvidenceByIDAsync(token, IDEvidence);
            string WebViewContent = $"<div style='color:#C1C1C1'>{detalle.Description}</div>";
            web_view.LoadDataWithBaseURL(null, WebViewContent, "text/html", "utf-8", null);
            web_view.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Koush.UrlImageViewHelper.SetUrlDrawable(img, detalle.Url);            
        }
    }
}