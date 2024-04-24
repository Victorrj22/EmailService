using EmailService.Models;

namespace EmailService.Repository;

public class Repository
{
    /// <summary>
    /// Busca todos os endereços de emails a serem notificados
    /// </summary>
    /// <returns></returns>
    public List<string> FindEmails()
    {
        List<string> emails;
        using (var dbContext = new PostgresContext())
        {
            emails = dbContext.Configuracaoemails.Select(e => e.Ceemail).ToList();
        }
        return emails;
    }

    /// <summary>
    /// Busca todos os produtos que precisam ser avisados
    /// </summary>
    /// <returns></returns>
    public List<Produto> FindProductToNotice()
    {
        List<Produto> produtos;
        using (var dbContext = new PostgresContext())
        {
            produtos = dbContext.Produtos.Where(p => p.Proqtdestoque < p.Proqtdavisa).ToList();
        }
        return produtos;
    }

    /// <summary>
    /// Verifica a data do ultimo aviso de um determinado produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    public Aviso? FindNoticeDate(Produto produto)
    {
        Aviso? avisoData;
        using (var dbContext = new PostgresContext())
        {
            avisoData = dbContext.Avisos.Where(a => a.Avcodigoproduto == produto.Procodigo)
                .OrderByDescending(a => a.Avdata).FirstOrDefault();
        }
        return avisoData;
    }

    /// <summary>
    /// Verifica se existem avisos
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    public bool CheckNotices(Produto produto)
    {
        bool avisoExistente;
        using (var dbContext = new PostgresContext())
        {
            avisoExistente = dbContext.Avisos.Any(a => a.Avcodigoproduto == produto.Procodigo);
        }
        return avisoExistente;
    }

    /// <summary>
    /// Adiciona um novo registro de aviso
    /// </summary>
    /// <param name="produto"></param>
    public void AddNotice(Produto produto)
    {
        using (var dbContext = new PostgresContext())
        {
            var aviso = new Aviso()
            {
                Avdata = DateTime.Now,
                Avcodigoproduto = produto.Procodigo
            };
            dbContext.Avisos.Add(aviso);
            dbContext.SaveChanges();
        }
    }
}