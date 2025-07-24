using Auditoria.Dominio.LogsAuditoria;
using Auditoria.Dominio.Views;
using AutoMapper;
using MongoDB.Bson;

namespace Auditoria.Aplicacao.Profiles;

public class ApplicationAutoMapperProfile : Profile
{
    public ApplicationAutoMapperProfile()
    {
        // Criação do mapeamento de objetos - Deve-se dividir em classes menores se esta ficar grande demais
        // Criar um mapa da Entidade > View quando for operação de listar 
        // Criar um mapa da DTO > Entidade quando for operação de inserir/atualizar, um mapa para cada DTO
        
        CreateMap<BsonDocument, Dictionary<string, string>>()
            .ConvertUsing(src =>
                src.Elements.ToDictionary(
                    e => e.Name,
                    e => e.Value.IsBsonNull ? null : e.Value.ToString()
                )!
            );
        
        CreateMap<LogAuditoria, LogAuditoriaView>()
            .ForMember(view => view.Dados, opt => opt.MapFrom(src => src.Dados));
    }
}