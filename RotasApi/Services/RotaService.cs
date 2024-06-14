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

        public RotaService(IRotaRepository rotaRepository, IMapper mapper)
        {
            _rotaRepository = rotaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RotaDTO>> ObterTodas()
        {
            var rotas = await _rotaRepository.ObterTodas();
            return _mapper.Map<IEnumerable<RotaDTO>>(rotas);
        }

        public async Task<RotaDTO> ObterPorId(int id)
        {
            var rota = await _rotaRepository.ObterPorId(id);
            return _mapper.Map<RotaDTO>(rota);
        }

        public async Task<Rota> Adicionar(RotaDTO rotaDto)
        {
            var rota = _mapper.Map<Rota>(rotaDto);
            var novaRota = await _rotaRepository.Adicionar(rota);
            return novaRota;
        }

        public async Task<RotaDTO> Atualizar(int id, RotaDTO rotaDto)
        {
            var rotaExistente = await _rotaRepository.ObterPorId(id);
            if (rotaExistente == null)
            {
                return null;
            }

            var rota = _mapper.Map(rotaDto, rotaExistente);
            var rotaAtualizada = await _rotaRepository.Atualizar(rota);
            return _mapper.Map<RotaDTO>(rotaAtualizada);
        }

        public async Task<bool> Remover(int id)
        {
            return await _rotaRepository.Remover(id);
        }

        public async Task<string> ConsultarMelhorRota(string origem, string destino)
        {
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
            return result;
        }

        private string BuscarMelhorRota(Dictionary<string, List<(string destino, int valor)>> graph, string origem, string destino)
        {
            var pq = new PriorityQueue<(string cidade, decimal custo, List<string> caminho), decimal>();
            pq.Enqueue((origem, 0, new List<string> { origem }), 0);

            while (pq.Count > 0)
            {
                var (cidade, custo, caminho) = pq.Dequeue();

                if (cidade == destino)
                {
                    return string.Join(" - ", caminho) + $" ao custo de ${custo}";
                }

                if (graph.ContainsKey(cidade))
                {
                    foreach (var (proxDestino, proxCusto) in graph[cidade])
                    {
                        var novoCaminho = new List<string>(caminho) { proxDestino };
                        pq.Enqueue((proxDestino, custo + proxCusto, novoCaminho), custo + proxCusto);
                    }
                }
            }

            return "Rota não encontrada";
        }
    }
}
