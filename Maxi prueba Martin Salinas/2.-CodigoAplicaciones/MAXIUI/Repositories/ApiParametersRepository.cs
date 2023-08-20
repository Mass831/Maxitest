using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MAXIUI.Repositories.MethodRepository;

namespace MAXIUI.Repositories
{
    public class ApiParametersRepository<TEntity>
    {

        public ApiParametersRepository(string urlBase, string uri, Method metodo, string tipoMedia, TEntity entidad)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            Metodo = metodo;
            TipoMedia = tipoMedia;
            Entidad = entidad;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
        }

        public ApiParametersRepository(string urlBase, string uri, Method metodo, string tipoMedia)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            Metodo = metodo;
            TipoMedia = tipoMedia;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
        }





        public NetworkCredential Credentials { get; set; }
        public Uri UrlBase { get; set; }
        public string Uri { get; set; }
        public TEntity Entidad { get; set; }
        public Method Metodo { get; set; }
        public string TipoMedia { get; set; }
        public string Token { get; set; }
        public string TokenScheme { get; set; }
        public int BeneficiaryId { get; set; }
        public string ElasticAttributes { get; set; }
        public string Language { get; set; }

        public void AddNetworkCredentials(string credentialUser, string credentialPassword)
            => Credentials = new NetworkCredential(credentialUser, credentialPassword);

        public void RemoveNetworkCredentials()
            => Credentials = null;

    }

       
}
