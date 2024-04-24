using EmailService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailService;

public class SendEmail
{
    /// <summary>
    /// Envia o email
    /// </summary>
    /// <param name="produto"></param>
    /// <param name="emails"></param>
    public async Task SendGridEmail(Produto produto, List<string> emails)
    {
        var client = new SendGridClient("...");
        var from = new EmailAddress("...", "AnotherJohn");
        var subject = "Matricula:10287, Nome:João Victor Soares Jordão";
        
        var produtoAtual = new Produto()
        {
            Procodigo = produto.Procodigo,
            Proavisaressup = produto.Proavisaressup,
            Pronome = produto.Pronome,
            Proqtdestoque = produto.Proqtdestoque,
            Proqtdavisa = produto.Proqtdavisa
        };

        foreach (var email in emails)
        {
            var to = new EmailAddress(email, "Grupo de aviso");
            var plainTextContent =
                $"O produto {produtoAtual.Pronome} de código {produtoAtual.Procodigo} precisa ser reabastecido. Quantidade em estoque: {produto.Proqtdestoque} Quantidade para aviso: {produto.Proqtdavisa}";
            var htmlContent = $"<strong>{plainTextContent}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine((response.StatusCode));
        }
    }
}