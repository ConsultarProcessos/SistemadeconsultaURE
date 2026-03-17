using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VibeCodeAPI.Models;

namespace VibeCodeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarRequerimento([FromBody] Requerimento req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { sucesso = false, mensagem = "Dados inválidos ou vazios." });
            }

            // Geração do Protocolo Único
            var protocolo = "REQ" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // Montagem da Mensagem
            var mensagem = $@"
Protocolo: {protocolo}

Nome: {req.Nome}
RG: {req.Rg}
Cargo: {req.Cargo}
Endereço: {req.Endereco}
Telefone: {req.Telefone}
Email: {req.Email}

Unidade Escolar: {req.Escola}

Solicitação:
Contagem de tempo para fins de {req.Tipo}
";

            var assunto = $"Confirmação de Solicitação de Contagem de Tempo - {protocolo}";

            bool emailEnviado = false;
            string erroEmail = string.Empty;

            try
            {
                // TODO: Configurar servidor SMTP real. 
                // Por enquanto simulamos o sucesso (ou falha prevista) do mail() do PHP local
                
                // Exemplo prático para o futuro:
                // using var smtpClient = new SmtpClient("smtp.servidor.com");
                // var mailMessage = new MailMessage("sistema@atendimentoprevidenciario.com.br", req.Email, assunto, mensagem);
                // await smtpClient.SendMailAsync(mailMessage);
                // emailEnviado = true;
                
                // Simulando o comportamento exato do backend atual:
                erroEmail = "A função de e-mail requer configuração SMTP (simulação .NET).";
            }
            catch (Exception)
            {
                erroEmail = "Falha ao enviar e-mail.";
            }

            return Ok(new
            {
                sucesso = true,
                protocolo = protocolo,
                emailEnviado = emailEnviado,
                alertaOpcional = erroEmail
            });
        }
    }
}
