using Auditoria.Aplicacao.Historicos.DTOs;
using Auditoria.Dominio.Historicos;
using AutoMapper;

namespace Auditoria.Aplicacao.Profiles;

public class ApplicationAutoMapperProfile : Profile
{
    public ApplicationAutoMapperProfile()
    {
        // Criação do mapeamento de objetos - Deve-se dividir em classes menores se esta ficar grande demais
        // Criar um mapa da Entidade > View quando for operação de listar 
        // Criar um mapa da DTO > Entidade quando for operação de inserir/atualizar, um mapa para cada DTO
        
        CreateMap<HistoricoDTO, Historico>();
    }
}