using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.OrdenCompra;
using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Presentacion.Agente
{
    public class AgenteServicio
    {
        private static string UrlBase;
        private string ApiLogin;
        private string ApiCatalgos;
        private string ApiRequisicion;
        private string ApiOrdenCompra;

        public RespuestaDTO _respuestaDTO;

        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public RespuestaRequisicionDTO _respuestaRequisicion;
        public OrdenCompraDTO _ordeCompraDTO;
        public RequisicionRevisionDTO _requisicionRevisionDTO;
        public RequisicionAutorizacionDTO _requsicionAutorizacion;
        public CatalogoRespuestaDTO _respuestaCatalogos;
        public RequisicionDTO _requisicion;
        public RequisicionOCDTO _requisicionOrdenCompra;

        public List<RequisicionDTO> _listaRequisicion;
        public List<EmpresaDTO> _listaEmpresas;
        public List<EmpresaConfiguracion> _EmpresasConfiguracion;
        public List<PaisModel> _listaPaises;
        public List<EstadosRepModel> _listaEstados;
        public List<RolDto> _lstaAllRoles;
        public List<RolCat> _lstaRolesCat;
        public List<RolMovilCompra> _lstaRolesMovilCom;
        public List<RolCompras> _lstaRolesCom;
        public List<RolRequsicion> _lstaRolesReq;
        public List<TipoPersonaModel> _lstaTipoPersona;
        public List<RegimenFiscalModel> _lstaRegimenFiscal;
        public List<ClientesDto> _lstaClientes;

        public List<RequisicionEstatusDTO> _listaRequisicionEstatus;
        public List<UsuarioDTO> _listaUsuarios;
        public List<UsuariosModel> _lstUserEmp;
        public List<CentroCostoDTO> _listaCentroCosto;
        public List<ProductoDTO> _listProductos;
        public List<OrdenCompraDTO> _listaOrdenCompra;
        public List<OrdenCompraEstatusDTO> _listaOrdenCompraEstatus;
        public List<ProveedorDTO> _listaProveedores;
        public List<CuentaContableDTO> _listaCuentasContables;

        public AgenteServicio()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];
        }

        #region Paises
        public void BuscarPaises(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPaises"];
            ListaPaises(this.ApiCatalgos, tkn).Wait();
        }

        public List<PaisModel> AgregaritemP()
        {
            PaisModel rol = new PaisModel();
            rol.Pais = "Seleccione";
            List<PaisModel> Paises = new List<PaisModel>();
            Paises.Add(rol);

            return Paises;

        }
        private async Task ListaPaises(string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<PaisModel> emp = new List<PaisModel>();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<PaisModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<PaisModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                var item = AgregaritemP();
                item.AddRange(emp);
                _listaPaises = item;
            }
        }

        #endregion

        #region Estados

        public List<EstadosRepModel> AgregaritemE()
        {
            EstadosRepModel rol = new EstadosRepModel();
            rol.Estado = "Seleccione";
            List<EstadosRepModel> Edos = new List<EstadosRepModel>();
            Edos.Add(rol);

            return Edos;

        }
        public void BuscarEstados(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEstadosR"];
            ListaEstados(this.ApiCatalgos, tkn).Wait();
        }

        private async Task ListaEstados(string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<EstadosRepModel> emp = new List<EstadosRepModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    //HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<EstadosRepModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<EstadosRepModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                var item = AgregaritemE();
                item.AddRange(emp);
                _listaEstados = item;

            }
        }

        #endregion

        #region Catalogos 

        #region Empresa
        public void ListaEmpresasLogin()
        {
            this.ApiLogin = ConfigurationManager.AppSettings["GetListaEmpresasLogin"];
            ListaEmp(this.ApiLogin).Wait();
        }
        public void ListaEmpresasLogin(string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEmpresas"];
            ListaEmp(ApiCatalgos, token).Wait();
        }
        //private async Task ListaEmp(string api, string token = null)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        //this.ApiLogin = ConfigurationManager.AppSettings["GetListaEmpresasLogin"];
        //        //ListaEmp(this.ApiLogin).Wait();
        //    }
        //public void ListaEmpresasLogin(string token)
        //{
        //    this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEmpresas"];
        //    ListaEmp(ApiCatalgos, token).Wait();
        //}
        private async Task ListaEmp(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<EmpresaDTO> emp = new List<EmpresaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<EmpresaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<EmpresaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaEmpresas = emp;
            }
        }
        //}
        public void GuardarEmpresaNueva(EmpresaModel dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraEmpresas"];
            GuardarEmpresa(dto, tkn).Wait();
        }
        //private async Task GuardarEmpresa(EmpresaModel _pcDTO, string token)
        //{

        //    using (var client = new HttpClient())
        //    {
        //        this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraEmpresas"];
        //        GuardarEmpresa(dto, tkn).Wait();
        //    }
        private async Task GuardarEmpresa(EmpresaModel _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        //  #endregion
        //#region Usuarios
        //public void BuscarListaUsuarios(short idEmpresa, string tkn)
        //{
        //    this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
        //    GetListaUsuarios(idEmpresa, tkn).Wait();
        //}
        //private async Task GetListaUsuarios(short IdEmpresa, string Token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        List<UsuarioDTO> lus = new List<UsuarioDTO>();
        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.GetAsync(ApiCatalgos + IdEmpresa.ToString()).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                lus = await response.Content.ReadAsAsync<List<UsuarioDTO>>();
        //            else
        //            {
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            lus = new List<UsuarioDTO>();
        //            client.CancelPendingRequests();
        //            client.Dispose(); ;
        //        }
        //        _listaUsuarios = lus;
        //    }
        //}
        //#endregion

        //#region Centros de costos
        //public void BuscarCentrosCostos(string tkn)
        //     }

        public void EliminarEmpresa(short dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarEmpresa"];
            EliminarEmpresaSeleccionada(dto, tkn).Wait();
        }

        private async Task EliminarEmpresaSeleccionada(short _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _pcDTO.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        public void GuardarEmpresaConfiguracion(EmpresaConfiguracion dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEmpresaConfig"];
            GuardarEmpresaConfig(dto, tkn).Wait();
        }
        private async Task GuardarEmpresaConfig(EmpresaConfiguracion _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {//PostAsJsonAsync
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        public void GuardarEmpresaEdicion(EmpresaDTO dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaEmpresas"];
            GuardarEmpresaEditada(dto, tkn).Wait();
        }

        private async Task GuardarEmpresaEditada(EmpresaDTO _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {//PostAsJsonAsync
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        #endregion

        #region Usuarios
        public void BuscarListaUsuarios(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaUsuarios(idEmpresa, tkn).Wait();
        }
        private async Task GetListaUsuarios(short IdEmpresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuarioDTO> lus = new List<UsuarioDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + IdEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuarioDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuarioDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaUsuarios = lus;
            }
        }
        public void BuscarUsuarioId(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaIdUsuario(id, tkn).Wait();
        }

        private async Task GetListaIdUsuario(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuariosModel> lus = new List<UsuariosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuariosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuariosModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstUserEmp = (from x in lus where x.IdUsuario == id select x).ToList();
            }
        }
        public void BuscarTodosUsuarios(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaTodosUsuarios(tkn).Wait();
        }

        private async Task GetListaTodosUsuarios(string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuariosModel> lus = new List<UsuariosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuariosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuariosModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstUserEmp = lus;
            }
        }

        public void FiltrarUsuarios(int idEmpresa, int idUser, string mail, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaFiltroUsuarios(idEmpresa, idUser, mail, tkn).Wait();
        }

        private async Task GetListaFiltroUsuarios(int idEmpresa, int idUser, string mail, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuariosModel> lus = new List<UsuariosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuariosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuariosModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _lstUserEmp = lus;

                if (idEmpresa != 0)
                {
                    _lstUserEmp = (from x in lus where x.IdEmpresa == idEmpresa select x).ToList();
                }
                if (idUser != 0)
                {
                    _lstUserEmp = (from x in lus where x.IdUsuario == idUser select x).ToList();
                }


                if (!String.IsNullOrEmpty(mail))
                {
                    _lstUserEmp = (from x in lus where x.Email1 == mail select x).ToList();
                }
            }
        }

        public void GuardarNuevoUsuario(UsuarioDTO dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraUsuarios"];
            GuardarUsuario(dto, tkn).Wait();
        }
        private async Task GuardarUsuario(UsuarioDTO _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        public void GuardarCredenciales(UsuarioDTO dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaCredencial"];
            GuardarCredencial(dto, tkn).Wait();
        }

        private async Task GuardarCredencial(UsuarioDTO _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        public void GuardarRolesAsig(UsuariosModel dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostAsignarRol"];
            GuardarRolUser(dto, tkn).Wait();
        }
        private async Task GuardarRolUser(UsuariosModel _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        public void GuardarUsuarioEdicion(UsuarioDTO dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaUsuarios"];
            GuardarUsuarioEditado(dto, tkn).Wait();
        }

        private async Task GuardarUsuarioEditado(UsuarioDTO _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        public void EliminarUsuario(short dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarUsuario"];
            EliminarUsuarioSeleccionado(dto, tkn).Wait();
        }

        private async Task EliminarUsuarioSeleccionado(short _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _pcDTO.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        //public void BuscarListaUsuarios(short idEmpresa, string tkn)
        //{
        //    this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
        //    GetListaUsuarios(idEmpresa, tkn).Wait();
        //}
        //private async Task GetListaUsuarios(short IdEmpresa, string Token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        this.ApiCatalgos = ConfigurationManager.AppSettings["GetCentrosCostos"];
        //        ListaCentrosCosto(tkn).Wait();
        //    }
        //private async Task ListaCentrosCosto(string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        List<CentroCostoDTO> emp = new List<CentroCostoDTO>();
        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                emp = await response.Content.ReadAsAsync<List<CentroCostoDTO>>();
        //            else
        //            {
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            emp = new List<CentroCostoDTO>();
        //            client.CancelPendingRequests();
        //            client.Dispose(); ;
        //        }
        //        _listaCentroCosto = emp;
        //    }
        //}
        #endregion

        #region Productos
        public void BuscarProductos(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaProductos"];
            ListaProductosPorIdEmpresa(tkn).Wait();
        }
        private async Task ListaProductosPorIdEmpresa(string token)
        {
            using (var client = new HttpClient())
            {
                List<ProductoDTO> emp = new List<ProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<ProductoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<ProductoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listProductos = emp;
            }
        }
        // }


        #endregion

        #region roles

        public void BuscarRolesRequisicion(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesReq(tkn).Wait();
        }

        private async Task GetListaRolesReq(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolRequsicion> lus = new List<RolRequsicion>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolRequsicion>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolRequsicion>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesReq = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }

        public void BuscarRolesMovilCompras(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesMovilCom(tkn).Wait();
        }

        private async Task GetListaRolesMovilCom(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolMovilCompra> lus = new List<RolMovilCompra>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolMovilCompra>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolMovilCompra>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesMovilCom = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarRolesCompras(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesCom(tkn).Wait();
        }

        private async Task GetListaRolesCom(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolCompras> lus = new List<RolCompras>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolCompras>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolCompras>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesCom = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarRolesCat(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesCat(tkn).Wait();
        }

        private async Task GetListaRolesCat(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolCat> lus = new List<RolCat>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolCat>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolCat>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesCat = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarTodosRoles(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaTodosRoles(tkn).Wait();
        }

        private async Task GetListaTodosRoles(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolDto> lus = new List<RolDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaAllRoles = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }

        public void BuscarRolId(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaIdRol(id, tkn).Wait();
        }
        private async Task GetListaIdRol(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolDto> lus = new List<RolDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaAllRoles = (from x in lus where x.IdRol == id select x).ToList();
            }
        }
        public void GuardarNuevoRol(RolDto dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraRol"];
            GuardarRol(dto, tkn).Wait();
        }
        private async Task GuardarRol(RolDto _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        public void GuardarModificacionRol(RolDto dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaRoles"];
            GuardarEdicionRol(dto, tkn).Wait();
        }

        private async Task GuardarEdicionRol(RolDto _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        public void GuardarPermisos(RolDto dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaPermisos"];
            ActualizaPermisos(dto, tkn).Wait();
        }

        private async Task ActualizaPermisos(RolDto _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        public void EliminarRol(short dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarRol"];
            EliminarRolSeleccionado(dto, tkn).Wait();
        }

        private async Task EliminarRolSeleccionado(short _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _pcDTO.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }

        #endregion

        #region Clientes

        public void BuscarTiposPersona(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetTiposPersona"];
            GetTiposPersona(tkn).Wait();
        }

        private async Task GetTiposPersona(string Token)
        {
            using (var client = new HttpClient())
            {
                List<TipoPersonaModel> lus = new List<TipoPersonaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<TipoPersonaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<TipoPersonaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaTipoPersona = lus;
            }
        }

        public void BuscarRegimenFiscal(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetRegimenFiscal"];
            GetRegimen(tkn).Wait();
        }

        private async Task GetRegimen(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RegimenFiscalModel> lus = new List<RegimenFiscalModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RegimenFiscalModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RegimenFiscalModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRegimenFiscal = lus;
            }
        }

        public void BuscarListaClientes(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaClientes"];
            GetListaClientes(idEmpresa, tkn).Wait();
        }
        private async Task GetListaClientes(short IdEmpresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<ClientesDto> lus = new List<ClientesDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + IdEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<ClientesDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<ClientesDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaClientes = lus;
            }
        }
        public void GuardarNuevoCliente(ClientesDto dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraClientes"];
            GuardarClientes(dto, tkn).Wait();
        }
        private async Task GuardarClientes(ClientesDto _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        #endregion

        public void BuscarCentrosCostos(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCentrosCostos"];
            ListaCentrosCosto(tkn).Wait();
        }
        private async Task ListaCentrosCosto(string token)
        {
            using (var client = new HttpClient())
            {
                List<CentroCostoDTO> emp = new List<CentroCostoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<CentroCostoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<CentroCostoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaCentroCosto = emp;
            }
        }

        public void BuscarProveedores(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaProveedores"];
            ListaProveedores(tkn).Wait();
        }
        private async Task ListaProveedores(string token)
        {
            using (var client = new HttpClient())
            {
                List<ProveedorDTO> emp = new List<ProveedorDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<ProveedorDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    emp = new List<ProveedorDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaProveedores = emp;
            }
        }
        //private async Task ListaCentrosCosto(string token)
        //    {
        //        using (var client = new HttpClient())


        #endregion

        #region Cuentas Contables
        public void BuscarCuentasContables(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCuentasContables"];
            ListaCuentaContable(idEmpresa, tkn).Wait();
        }
        private async Task ListaCuentaContable(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<CuentaContableDTO> emp = new List<CuentaContableDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos, idEmpresa)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<CuentaContableDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<CuentaContableDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaCuentasContables = emp;
            }
        }
        #endregion
        //  #endregion

        #region Login
        public void Acceder(AutenticacionDTO autDto)
        {
            this.ApiLogin = ConfigurationManager.AppSettings["PostLogin"];
            Login(autDto).Wait();
        }
        private async Task Login(AutenticacionDTO autDto)
        {
            using (var client = new HttpClient())
            {
                RespuestaAutenticacionDto respuesta = new RespuestaAutenticacionDto();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiLogin, autDto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        respuesta = await response.Content.ReadAsAsync<RespuestaAutenticacionDto>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message.ToString();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaAutenticacion = respuesta;
            }
        }
        #endregion

        #region Requisicion
        public void BuscarRequisicionEstatus(string Tkn)
        {
            ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionEstatus"];
            ListaRequisicionesEstatus(Tkn).Wait();
        }
        private async Task ListaRequisicionesEstatus(string token)
        {
            using (var client = new HttpClient())
            {
                List<RequisicionEstatusDTO> emp = new List<RequisicionEstatusDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<RequisicionEstatusDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<RequisicionEstatusDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaRequisicionEstatus = emp;
            }
        }
        public void BuscarRequisiciones(short idEmpresa, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionesByIdEmpresa"];
            ListaRequisiciones(idEmpresa, tkn).Wait();
        }
        private async Task ListaRequisiciones(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<RequisicionDTO> emp = new List<RequisicionDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion + idEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<RequisicionDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<RequisicionDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaRequisicion = emp;
            }
        }
        public void GuardarRequisicon(RequisicionEDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PostRequisicion"];
            SaveRequisicon(_requi, token).Wait();
        }
        private async Task SaveRequisicon(RequisicionEDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        public void ActualizarRequisicionRevision(RequisicionRevPutDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutActulizarRevision"];
            UpdateRequisicon(_requi, token).Wait();
        }
        public void ActualizarRequisicionAutorizacion(RequisicionAutPutDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutActulizarAutorizacion"];
            UpdateRequisicon(_requi, token).Wait();
        }
        public void ActualizarRequisicionCancelar(RequisicionCancelaDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutCancelarRequisicion"];
            UpdateRequisicon(_requi, token).Wait();
        }
        private async Task UpdateRequisicon(RequisicionCancelaDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDTO resp = new RespuestaRequisicionDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaRequisicion = resp;
            }
        }
        private async Task UpdateRequisicon(RequisicionAutPutDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDTO resp = new RespuestaRequisicionDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaRequisicion = resp;
            }
        }
        private async Task UpdateRequisicon(RequisicionRevPutDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        public void RequisicionRevision(int IdRequisicion, string tkn)
        {
            ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionByNumRequisicion"];
            BuscarRequisicioRevision(IdRequisicion, tkn).Wait();
        }
        private async Task BuscarRequisicioRevision(int IdRequisicion, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionRevisionDTO emp = new RequisicionRevisionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiRequisicion, IdRequisicion)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionRevisionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionRevisionDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requisicionRevisionDTO = emp;
            }
        }
        public void BuscarRequisicioAuto(int IdRequisicion, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionByNumRequisicionAut"];
            RequisicionAuto(IdRequisicion, tkn).Wait();
        }
        private async Task RequisicionAuto(int IdReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionAutorizacionDTO emp = new RequisicionAutorizacionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiRequisicion, IdReq.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionAutorizacionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionAutorizacionDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requsicionAutorizacion = emp;
            }
        }


        #endregion

        #region Orden de Compra
        public void BuscarRequisicionOC(int idReq, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetBuscarReq"];
            RequisicionPorIdReqOC(idReq, tkn).Wait();
        }
        private async Task RequisicionPorIdReqOC(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionOCDTO emp = new RequisicionOCDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiOrdenCompra + numReq).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionOCDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionOCDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requisicionOrdenCompra = emp;
            }
        }
        public void GuardarOrdenesCompra(OrdenCompraCrearDTO ocDTO, string token)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["PostGenerarOrdenesCompra"];
            SaveOrdenCompra(ocDTO, token).Wait();
        }
        private async Task SaveOrdenCompra(OrdenCompraCrearDTO _oc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    resp.Exito = false;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        public void BuscarOrdenesCompra(short idEmpresa, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetOrdenesCompra"];
            ListaOrdenCompra(idEmpresa, tkn).Wait();
        }
        private async Task ListaOrdenCompra(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<OrdenCompraDTO> emp = new List<OrdenCompraDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idEmpresa.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<OrdenCompraDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<OrdenCompraDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaOrdenCompra = emp;
            }
        }
        public void CancelarOrdenCompra(OrdenCompraDTO _oc, string token)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["PutCancelarOrdenCompra"];
            CancelarOC(_oc, token).Wait();
        }
        private async Task CancelarOC(OrdenCompraDTO _oc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        _respuestaDTO = resp;
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        //public void AutorizarOrdenCompra(OrdenCompraAutorizacionDTO _oc, string token)
        //{
        //    this.ApiOrdenCompra = ConfigurationManager.AppSettings["PutAutorizarCompra"];
        //    AutorizarOC(_oc, token).Wait();
        //}
        //private async Task AutorizarOC(OrdenCompraAutorizacionDTO _oc, string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        RespuestaDto resp = new RespuestaDto();

        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.PutAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                resp = await response.Content.ReadAsAsync<RespuestaDto>();
        //            else
        //            {
        //                _respuestaDTO = resp;
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            resp.Mensaje = ex.Message;
        //            client.CancelPendingRequests();
        //            client.Dispose();
        //        }
        //        _respuestaDTO = resp;
        //    }
        //}
        //public void BuscarOrdenCompra(int idOC, string tkn)
        //{
        //    this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetBuscarOrdenCompra"];
        //    OrdenCompra(idOC, tkn).Wait();
        //}
        //private async Task OrdenCompra(int idOrdenCompra, string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        OrdenCompraCrearDTO emp = new OrdenCompraCrearDTO();
        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idOrdenCompra.ToString())).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                emp = await response.Content.ReadAsAsync<OrdenCompraCrearDTO>();
        //            else
        //            {
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            emp = new OrdenCompraCrearDTO();
        //            client.CancelPendingRequests();
        //            client.Dispose(); ;
        //        }
        //        _ordenCompraCrearDTO = emp;
        //    }
        //}
        public void BuscarOrdenCompraEstatus(string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetOrdenCompraEstatus"];
            ListaOrdenCompraEstatus(tkn).Wait();
        }
        private async Task ListaOrdenCompraEstatus(string token)
        {
            using (var client = new HttpClient())
            {
                List<OrdenCompraEstatusDTO> emp = new List<OrdenCompraEstatusDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiOrdenCompra).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<OrdenCompraEstatusDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<OrdenCompraEstatusDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaOrdenCompraEstatus = emp;
            }
        }
        #endregion
    }
}