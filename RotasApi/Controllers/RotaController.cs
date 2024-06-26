﻿using Microsoft.AspNetCore.Mvc;
using RotasApi.DTOs;
using RotasApi.Models;
using RotasApi.Services;

namespace RotasApi.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class RotaController : Controller
{
#warning Update config documentation

    private readonly IRotaService _rotaService;

    public RotaController(IRotaService rotaService)
    {
        _rotaService = rotaService;
    }

    /// <summary>
    /// Obtém todas as rotas disponíveis
    /// </summary>
    /// <returns>Lista de rotas</returns>
    /// <response code="200">Retorna a lista de rotas</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rota>>> Get()
    {
        try
        {
            var rotas = await _rotaService.ObterTodas();
            return Ok(rotas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter rotas: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém uma rota específica pelo ID
    /// </summary>
    /// <param name="id">ID da rota</param>
    /// <returns>Rota correspondente ao ID</returns>
    /// <response code="200">Retorna a rota encontrada</response>
    /// <response code="404">Se a rota não for encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RotaDTO>> Get(int id)
    {
        try
        {

            var rota = await _rotaService.ObterPorId(id);
            if (rota == null)
            {
                return NotFound();
            }
            return Ok(rota);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter rota com ID: {id}. {ex.Message}");
        }
    }

    /// <summary>
    /// Adiciona uma nova rota
    /// </summary>
    /// <param name="rotaDto">Objeto com os campos necessários para criação de uma rota</param>
    /// <returns>Rota criada</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Rota>> Post(RotaDTO rotaDto)
    {
        try
        {
            var novaRota = await _rotaService.Adicionar(rotaDto);
            return CreatedAtAction(nameof(Get), new { id = novaRota.Id }, novaRota);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao adicionar rota: {ex.Message}");
        }
    }

    /// <summary>
    /// Atualiza uma rota existente
    /// </summary>
    /// <param name="id">ID da rota a ser atualizada</param>
    /// <param name="rotaDto">Objeto com os campos necessários para atualização da rota</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    /// <response code="404">Se a rota não for encontrada</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, RotaDTO rotaDto)
    {
        try
        {
            var rotaAtualizada = await _rotaService.Atualizar(id, rotaDto);
            if (rotaAtualizada == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar rota com ID: {id}. {ex.Message}");
        }
    }

    /// <summary>
    /// Remove uma rota pelo ID
    /// </summary>
    /// <param name="id">ID da rota</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Caso remoção seja feita com sucesso</response>
    /// <response code="404">Se a rota não for encontrada</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var sucesso = await _rotaService.Remover(id);
            if (!sucesso)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao remover rota com ID: {id}. {ex.Message}");
        }
    }

    /// <summary>
    /// Consulta a melhor rota entre dois pontos
    /// </summary>
    /// <param name="origem">Cidade de origem</param>
    /// <param name="destino">Cidade de destino</param>
    /// <returns>Rota com o menor custo</returns>
    /// <response code="200">Retorna a melhor rota</response>
    [HttpGet("consulta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> ConsultarMelhorRota([FromQuery] string origem, [FromQuery] string destino)
    {
        try
        {
            var resultado = await _rotaService.ConsultarMelhorRota(origem, destino);
            return Ok(new { rota = resultado });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao consultar melhor rota de {origem} para {destino}. {ex.Message}");
        }
    }
}
