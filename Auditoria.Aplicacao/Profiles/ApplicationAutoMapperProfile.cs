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
        
        CreateMap<BsonDocument, Dictionary<string, string>?>()
            .ConvertUsing(src =>
                (src == null
                    ? null
                    : src.Elements.ToDictionary(
                        e => e.Name,
                        e => e.Value.IsBsonNull ? null : e.Value.ToString()
                    ))!
            );
        
        CreateMap<string, BsonDocument>()
            .ConvertUsing(src => 
                (string.IsNullOrEmpty(src) 
                    ? null 
                    : BsonDocument.Parse(src))!);
        
        CreateMap<DadosInseridor, DadosInseridorView>();
        CreateMap<DadosAcao, DadosAcaoView>();
        CreateMap<LogAuditoria, LogAuditoriaView>()
            .ForMember(dest => dest.Dados, opt => opt.MapFrom(src => src.DadoDesserializado));

        CreateMap<InserirLogAuditoriaDTO, LogAuditoria>()
            .ForMember(dest => dest.DadosInseridor, opt => opt.MapFrom(src => new DadosInseridor
            {
                CodigoTenant = src.CodigoTenant,
                CodigoUnidade = src.CodigoUnidade,
                CodigoUsuario = src.CodigoUsuario
            }))
            .ForMember(dest => dest.DadosAcao, opt => opt.MapFrom(src => new DadosAcao
            {
                EntidadeAuditada = src.EntidadeAuditada,
                TipoLog = src.TipoLog
            }))
            .ForMember(dest => dest.DadoDesserializado, opt => opt.MapFrom(src => src.DadoSerializado))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataInsercao, opt => opt.Ignore());
        //todo: converter para fuso local ao mapear para entidade

    }
}