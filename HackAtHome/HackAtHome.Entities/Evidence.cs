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

namespace HackAtHome.Entities
{
    public class Evidence
    {
        // Identificador de la evidencia
        public int EvidenceID { get; set; }

        // Título de la Evidencia
        public string Title { get; set; }

        // Estatus de la Evidencia
        public string Status { get; set; }
    }
}