using System.Net;
using Auditoria.Dominio.Historicos;
using Auditoria.Dominio.Interfaces;
using Auditoria.Infra.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using MongoDB.Bson;

namespace Auditoria.Aplicacao.Historicos;

public class AplicHistorico : IAplicHistorico
{
    private readonly IAmbienteHelper _ambienteHelper;
    private readonly IRepBaseMongoDb<Historico> _repBaseMongo;

    public AplicHistorico(IAmbienteHelper ambienteHelper, IRepBaseMongoDb<Historico> repBaseMongo)
    {
        _ambienteHelper = ambienteHelper;
        _repBaseMongo = repBaseMongo;
    }
    
    public async Task Inserir(HttpRequest request, HttpStatusCode statusCode)
    {
        var id = new ObjectId();
        var reg = new Historico
        {
            Id = id,
            DataHora = id.CreationTime,
            ChaveApi = _ambienteHelper.RecuperarUsuario() ?? string.Empty,
            CodigoTenant = _ambienteHelper.RecuperarCodigoTenant(),
            CodigoUnidade = _ambienteHelper.RecuperarCodigoUnidade(),
            Operacao = request.Method,
            Url = request.GetDisplayUrl(),
            SituacaoRetorno = statusCode
        };

        await _repBaseMongo.CreateAsync(reg);
    }
}