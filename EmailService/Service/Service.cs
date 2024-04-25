using EmailService.Repository;

namespace EmailService;

public class Service
{
    private readonly Repository.Repository _repositorio = new Repository.Repository();
    private SendEmail _sendEmail = new SendEmail();
    
    /// <summary>
    /// Inicia o processo do serviço
    /// </summary>
    public async void StartServiceEmail()
    {
        var horarioAtual = DateTime.Now;
        var produtos = _repositorio.FindProductToNotice();
        var emails = _repositorio.FindEmails();
        foreach (var produto in produtos)
        {
            //var avisoExistente = _repositorio.CheckNotices(produto);
            var avisoData = _repositorio.FindNoticeDate(produto);
            var diferencaDeTempo = avisoData != null ? horarioAtual - avisoData!.Avdata : TimeSpan.Zero;
            if (diferencaDeTempo.TotalMinutes >= 5 || avisoData == null)
            {
                await _sendEmail.SendGridEmail(produto, emails);
                _repositorio.AddNotice(produto);
            }
        }
        
        
    }
}