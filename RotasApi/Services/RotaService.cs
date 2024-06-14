using AutoMapper;
using RotasApi.DTOs;
using RotasApi.Models;
using RotasApi.Repositories;

namespace RotasApi.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RotaService> _logger;

        public RotaService(IRotaRepository rotaRepository, IMapper mapper, ILogger<RotaService> logger)
        {
            _rotaRepository = rotaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Rota>> ObterTodas()
        {
            try
            {
                _logger.LogInformation("Obtendo todas as rotas");
                var rotas = await _rotaRepository.ObterTodas();
                _logger.LogInformation("Obtenção de todas as rotas concluída com sucesso");
                return rotas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as rotas");
                throw;
            }
        }

        public async Task<RotaDTO> ObterPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Obtendo rota com ID: {id}");
                var rota = await _rotaRepository.ObterPorId(id);
                if (rota == null)
                    _logger.LogWarning($"Rota com ID: {id} não encontrada");
                else
                    _logger.LogInformation($"Rota com ID: {id} obtida com sucesso");
                return _mapper.Map<RotaDTO>(rota);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter rota com ID: {id}");
                throw;
            }
        }

        public async Task<Rota> Adicionar(RotaDTO rotaDto)
        {
            try
            {
                _logger.LogInformation($"Iniciando adição de uma nova rota com origem: {rotaDto.Origem} e destino: {rotaDto.Destino}");
                var rota = _mapper.Map<Rota>(rotaDto);
                var novaRota = await _rotaRepository.Adicionar(rota);
                _logger.LogInformation($"Nova rota adicionada com sucesso com ID: {novaRota.Id}");
                return novaRota;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar uma nova rota");
                throw;
            }
        }

        public async Task<RotaDTO> Atualizar(int id, RotaDTO rotaDto)
        {
            try
            {
                _logger.LogInformation($"Iniciando atualização da rota com ID: {id}");
                var rotaExistente = await _rotaRepository.ObterPorId(id);
                if (rotaExistente == null)
                {
                    _logger.LogWarning($"Rota com ID: {id} não encontrada para atualização");
                    return null;
                }

                var rota = _mapper.Map(rotaDto, rotaExistente);
                var rotaAtualizada = await _rotaRepository.Atualizar(rota);
                _logger.LogInformation($"Rota com ID: {id} atualizada com sucesso");
                return _mapper.Map<RotaDTO>(rotaAtualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar rota com ID: {id}");
                throw;
            }
        }

        public async Task<bool> Remover(int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando remoção da rota com ID: {id}");
                var sucesso = await _rotaRepository.Remover(id);
                if (sucesso)
                    _logger.LogInformation($"Rota com ID: {id} removida com sucesso");
                else
                    _logger.LogWarning($"Rota com ID: {id} não encontrada para remoção");
                return sucesso;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover rota com ID: {id}");
                throw;
            }
        }

        public async Task<string> ConsultarMelhorRota(string origem, string destino)
        {
            try
            {
                _logger.LogInformation($"Iniciando consulta da melhor rota de {origem} para {destino}");

                var rotas = await _rotaRepository.ObterTodas();
                var graph = new Dictionary<string, List<(string destino, int valor)>>();

                foreach (var rota in rotas)
                {
                    if (!graph.ContainsKey(rota.Origem))
                    {
                        graph[rota.Origem] = new List<(string destino, int valor)>();
                    }
                    graph[rota.Origem].Add((rota.Destino, rota.Valor));
                }

                var result = BuscarMelhorRota(graph, origem, destino);
                _logger.LogInformation($"Consulta da melhor rota de {origem} para {destino} concluída com resultado: {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao consultar melhor rota de {origem} para {destino}");
                throw;
            }
        }

        private string BuscarMelhorRota(Dictionary<string, List<(string destino, int valor)>> graph, string origem, string destino)
        {
            try
            {
                _logger.LogInformation($"Iniciando busca da melhor rota de {origem} para {destino}");
                _logger.LogInformation($"Iniciando busca da melhor rota de {origem} para {destino}");
                var filaDePrioridade = new PriorityQueue<(string cidade, decimal custo, List<string> caminho), decimal>();
                filaDePrioridade.Enqueue((origem, 0, new List<string> { origem }), 0);

                while (filaDePrioridade.Count > 0)
                {
                    var (cidade, custo, caminho) = filaDePrioridade.Dequeue();

                    if (cidade == destino)
                    {
                        var resultado = string.Join(" - ", caminho) + $" ao custo de ${custo}";
                        _logger.LogInformation($"Melhor rota encontrada: {resultado}");
                        return resultado;
                    }

                    if (graph.ContainsKey(cidade))
                    {
                        foreach (var (proxDestino, proxCusto) in graph[cidade])
                        {
                            var novoCaminho = new List<string>(caminho) { proxDestino };
                            filaDePrioridade.Enqueue((proxDestino, custo + proxCusto, novoCaminho), custo + proxCusto);
                        }
                    }
                }

                _logger.LogWarning($"Nenhuma rota encontrada de {origem} para {destino}");
                return "Rota não encontrada";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar melhor rota de {origem} para {destino}");
                throw;
            }
        }
    }
}
